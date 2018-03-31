using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using Core.Network.Extensions;

namespace Core.Network
{
    public class CoreServer
    {
        public delegate void EventHandler(CoreServer sender, TcpClient client);
        public delegate void DataEventHandler(CoreServer sender, TcpClient client, byte[] data, int length);

        public event EventHandler ClientConnected = (sender, client) => { };
        public event EventHandler ClientDisconnected = (sender, client) => { };
        public event DataEventHandler ClientDataRetrieve = (sender, client, data, length) => { };

        private SynchronizationContext mainContext;
        private Thread threadListener;
        private TcpListener listener;
        private Dictionary<Thread, TcpClient> clients = new Dictionary<Thread, TcpClient>();

        public CoreServer()
        {
            mainContext = SynchronizationContext.Current;
        }

        public bool IsStarted
        {
            get;
            private set;
        }

        public void SendData(TcpClient client, byte[] data)
        {
            var stream = client.GetStream();
            stream.Write(data, 0, data.Length);
            stream.Flush();
        }

        public void Start(int port)
        {
            CleanUp();

            threadListener = new Thread(() =>
            {
                listener = new TcpListener(IPAddress.Any, port);
                listener.Start();

                this.IsStarted = true;

                while (true)
                {
                    TcpClient client;

                    try
                    {
                        client = listener.AcceptTcpClient();
                    }
                    catch (SocketException)
                    {
                        break;
                    }
                    

                    Thread threadClient = new Thread((state1) =>
                    {
                        mainContext.Send((state2) =>
                        {
                            this.ClientConnected(this, state2 as TcpClient);
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
                                        this.ClientDataRetrieve(this, state2 as TcpClient, ms.ToArray(), readedAllData);
                                    }, client);
                                }
                            }
                        }

                        try
                        { 
                            mainContext.Send((state2) =>
                            {
                                this.ClientDisconnected(this, state2 as TcpClient);
                            }, client);
                        }
                        catch (InvalidAsynchronousStateException)
                        {
                            // If form closed
                        }

                        clients.Remove(state1 as Thread);
                    });

                    clients.Add(threadClient, client);

                    threadClient.Start(threadClient);
                }

                this.IsStarted = false;

                listener = null;
            });
            threadListener.Start();
        }

        public void Stop()
        {
            CleanUp();
        }

        ~CoreServer()
        {
            CleanUp(true);
        }

        private void CleanUp(bool force = false)
        {
            this.IsStarted = false;

            // Clients
            foreach (var client in clients)
            {
                if (client.Value.Connected)
                {
                    client.Value.Close();
                }

                if (force)
                    client.Key.Abort();
            }
            clients.Clear();

            // Server listener
            if (listener != null)
            {
                listener.Stop();
            }

            if (force)
            {
                // Server thread loop for accept clients
                if (threadListener != null)
                {
                    threadListener.Abort();
                    threadListener = null;
                }
            }
        }
    }
}
