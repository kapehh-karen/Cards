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
        private Dictionary<Thread, TcpClient> clients;

        public CoreServer()
        {
            mainContext = SynchronizationContext.Current;
            clients = new Dictionary<Thread, TcpClient>();
        }

        public void Start(int port)
        {
            CleanUp();

            threadListener = new Thread(() =>
            {
                listener = new TcpListener(IPAddress.Any, port);
                listener.Start();

                while (true)
                {
                    TcpClient client = listener.AcceptTcpClient();

                    Thread threadClient = new Thread((state1) =>
                    {
                        mainContext.Send((state2) =>
                        {
                            this.ClientConnected(this, client);
                        }, client);

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
                                    }

                                    mainContext.Send((state2) =>
                                    {
                                        this.ClientDataRetrieve(this, client, ms.ToArray(), readedAllData);
                                    }, client);
                                }
                            }
                        }

                        mainContext.Send((state2) =>
                        {
                            this.ClientDisconnected(this, client);
                        }, client);
                    });

                    clients.Add(threadClient, client);

                    threadClient.Start(client);
                }
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
            clients = null;

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
