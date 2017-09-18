using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace Core
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

        public void SendData(byte[] data)
        {
            client.Client.Send(data);
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

            this.Connected(this, client);

            threadClient = new Thread((state) =>
            {
                var stream = client.GetStream();
                var buffer = new byte[1024 * 1024];
                int readed;
                int readedAllData;

                while (client.Connected)
                {
                    if (stream.DataAvailable)
                    {
                        readedAllData = 0;

                        using (MemoryStream ms = new MemoryStream())
                        {
                            while ((readed = stream.Read(buffer, 0, buffer.Length)) > 0)
                            {
                                ms.Write(buffer, 0, readed);
                                readedAllData += readed;

                                Thread.Sleep(5);
                            }

                            mainContext.Send((state2) =>
                            {
                                this.DataRetrieve(this, client, ms.ToArray(), readedAllData);
                            }, client);
                        }
                    }

                    Thread.Sleep(50);
                }

                mainContext.Send((state2) =>
                {
                    this.Disconnected(this, client);
                }, client);
            });
            threadClient.Start(client);
        }

        public void Stop()
        {
            CleanUp();
        }

        ~CoreClient()
        {
            CleanUp();
        }

        private void CleanUp()
        {
            if (client != null)
            {
                client.Close();
                client = null;
            }

            if (threadClient != null)
            {
                threadClient.Abort();
                threadClient = null;
            }
        }
    }
}
