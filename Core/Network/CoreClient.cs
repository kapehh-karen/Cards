using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using Core.Network.Extensions;

namespace Core.Network
{
    public class CoreClient
    {
        public delegate void EventHandler(CoreClient sender, TcpClient client);
        public delegate void DataEventHandler(CoreClient sender, TcpClient client, byte[] data, int length);

        public event EventHandler Connected = (sender, client) => { };
        public event EventHandler Disconnected = (sender, client) => { };
        public event DataEventHandler DataRetrieve = (sender, client, data, length) => { };

        private SynchronizationContext mainContext;
        private Thread threadClient;
        private TcpClient client;

        public CoreClient()
        {
            mainContext = SynchronizationContext.Current;
        }

        public bool IsConnected
        {
            get
            {
                return client != null;
            }
        }

        public void SendData(byte[] data)
        {
            var stream = client.GetStream();
            stream.Write(data, 0, data.Length);
            stream.Flush();
        }

        public void Start(string hostname, int port)
        {
            CleanUp();

            client = new TcpClient();
            client.Connect(hostname, port);

            if (!client.Connected)
            {
                client = null;
                return;
            }
            
            threadClient = new Thread((state1) =>
            {
                mainContext.Send((state2) =>
                {
                    this.Connected(this, state2 as TcpClient);
                }, client);

                var socket = client.Client;
                var buffer = new byte[1024 * 1024];
                int readed;
                int readedAllData;

                while (client.IsEstablished())
                {
                    if (socket.Available > 0)
                    {
                        readedAllData = 0;

                        using (MemoryStream ms = new MemoryStream())
                        {
                            while (socket.Available > 0)
                            {
                                readed = socket.Receive(buffer, buffer.Length, SocketFlags.None);
                                ms.Write(buffer, 0, readed);
                                readedAllData += readed;
                            }

                            mainContext.Send((state2) =>
                            {
                                this.DataRetrieve(this, state2 as TcpClient, ms.ToArray(), readedAllData);
                            }, client);
                        }
                    }
                }

                try
                {
                    mainContext.Send((state2) =>
                    {
                        this.Disconnected(this, state2 as TcpClient);
                    }, client);
                }
                catch (InvalidAsynchronousStateException)
                {
                    // If form closed
                }

                client = null;
            });
            threadClient.Start(client);
        }

        public void Stop()
        {
            CleanUp();
        }

        ~CoreClient()
        {
            CleanUp(true);
        }

        private void CleanUp(bool force = false)
        {
            if (client != null)
            {
                client.Close();
            }

            if (force)
            {
                if (threadClient != null)
                {
                    threadClient.Abort();
                    threadClient = null;
                }
            }
        }
    }
}
