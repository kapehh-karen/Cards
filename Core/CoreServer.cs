using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace Core
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
                            this.ClientConnected(this, client);
                        }, client);

                        var socket = client.Client;
                        var buffer = new byte[2];
                        int readed;
                        int readedAllData;

                        while (client.Connected)
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

                                        Thread.Sleep(5);
                                    }

                                    mainContext.Send((state2) =>
                                    {
                                        this.ClientDataRetrieve(this, client, ms.ToArray(), readedAllData);
                                    }, client);
                                }
                            }

                            Thread.Sleep(50);
                        }

                        mainContext.Send((state2) =>
                        {
                            this.ClientDisconnected(this, client);
                        }, client);
                    });

                    clients.Add(threadClient, client);

                    threadClient.Start(client);
                }

                this.IsStarted = false;
            });
            threadListener.Start();
        }

        public void Stop()
        {
            CleanUp();
        }

        ~CoreServer()
        {
            CleanUp();
        }

        private void CleanUp()
        {
            this.IsStarted = false;

            // Clients
            foreach (var client in clients)
            {
                if (client.Value.Connected)
                {
                    client.Value.Close();
                }
                client.Key.Abort();
            }
            clients.Clear();

            // Server listener
            if (listener != null)
            {
                listener.Stop();
                listener = null;
            }

            // Server thread loop for accept clients
            if (threadListener != null && threadListener.IsAlive)
            {
                threadListener.Abort();
                threadListener = null;
            }
        }
    }
}
