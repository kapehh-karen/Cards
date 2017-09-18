using Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;

namespace CardsServer
{
    public partial class MainForm : Form
    {
        private CoreServer server;

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            var strHostName = Dns.GetHostName();
            lblMachineName.Text = strHostName;

            var strList = string.Empty;
            foreach (var ip in Helper.GetAllLocalIPv4(NetworkInterfaceType.Ethernet))
            {
                strList += $"{ip.Value} ({ip.Key}){Environment.NewLine}";
            }
            lblIPList.Text = strList;
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            server = new CoreServer();
            server.ClientConnected += Server_ClientConnected;
            server.ClientDisconnected += Server_ClientDisconnected;
            server.ClientDataRetrieve += Server_ClientDataRetrieve;
            server.Start(8080);
        }

        private void Server_ClientDataRetrieve(CoreServer sender, TcpClient client, byte[] data, int length)
        {
            var strBuilder = new StringBuilder();

            strBuilder.Append("Retrieved data: ");
            foreach (var b in data)
            {
                strBuilder.Append("[").Append(b).Append("] ");
            }

            MessageBox.Show($"DATA FROM CLIENT: <{strBuilder}>");

            server.SendData(client, new byte[] { 228, 118, 54, 13, 7 });
        }

        private void Server_ClientDisconnected(CoreServer sender, TcpClient client)
        {
            MessageBox.Show($"DISCONNECTED: From client {client.Client} !");
        }

        private void Server_ClientConnected(CoreServer sender, TcpClient client)
        {
            MessageBox.Show($"CONNECTED: Client endpoint {client.Client.RemoteEndPoint.ToString()} !");
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            if (server != null)
                server.Stop();
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (server != null)
                server.Stop();
        }
    }
}
