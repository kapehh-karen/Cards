using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Core;

namespace CardsClient
{
    public partial class MainForm : Form
    {
        private CoreClient client;

        public MainForm()
        {
            InitializeComponent();
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            client = new CoreClient();
            client.Connected += Client_Connected;
            client.Disconnected += Client_Disconnected;
            client.DataRetrieve += Client_DataRetrieve;
            client.Start(txtIPAddress.Text, 8080);
        }

        private void Client_DataRetrieve(CoreClient sender, System.Net.Sockets.TcpClient client, byte[] data, int length)
        {
            var strBuilder = new StringBuilder();

            strBuilder.Append("Retrieved data: ");
            foreach (var b in data)
            {
                strBuilder.Append("[").Append(b).Append("] ");
            }

            MessageBox.Show($"DATA FROM SERVER: <{strBuilder}>");
        }

        private void Client_Disconnected(CoreClient sender, System.Net.Sockets.TcpClient client)
        {
            MessageBox.Show($"DISCONNECTED: Server endpoint {client.Client.RemoteEndPoint.ToString()} !");
        }

        private void Client_Connected(CoreClient sender, System.Net.Sockets.TcpClient client)
        {
            MessageBox.Show($"CONNECTED: Server endpoint {client.Client.RemoteEndPoint.ToString()} !");
        }

        private void btnDisconnect_Click(object sender, EventArgs e)
        {
            client.Stop();
        }

        private void btnSendData_Click(object sender, EventArgs e)
        {
            if (client.IsConnected)
            {
                client.SendData(new byte[] { 7, 71, 13 });
            }
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            client.Stop();
        }
    }
}
