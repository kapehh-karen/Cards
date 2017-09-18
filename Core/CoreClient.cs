using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace Core
{
    public class CoreClient
    {
        private SynchronizationContext mainContext;
        private TcpClient client;

        public CoreClient()
        {
            mainContext = SynchronizationContext.Current;
        }

        public void Start(string hostname, int port)
        {
            CleanUp();

            client = new TcpClient();
            client.Connect(hostname, port);
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
        }
    }
}
