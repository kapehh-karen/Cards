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
        public static IDictionary<string, string> GetAllLocalIPv4(NetworkInterfaceType _type)
        {
            var dict = new Dictionary<string, string>();

            foreach (NetworkInterface item in NetworkInterface.GetAllNetworkInterfaces())
            {
                if (item.NetworkInterfaceType == _type && item.OperationalStatus == OperationalStatus.Up)
                {
                    foreach (UnicastIPAddressInformation ip in item.GetIPProperties().UnicastAddresses)
                    {
                        if (ip.Address.AddressFamily == AddressFamily.InterNetwork)
                        {
                            //ipAddrList.Add(ip.Address.ToString());
                            dict.Add(item.Description, ip.Address.ToString());
                        }
                    }
                }
            }

            return dict;
        }

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            var strHostName = Dns.GetHostName();
            lblMachineName.Text = strHostName;

            var strList = string.Empty;
            foreach (var ip in GetAllLocalIPv4(NetworkInterfaceType.Ethernet))
            {
                strList += $"{ip.Value} ({ip.Key}){Environment.NewLine}";
            }
            lblIPList.Text = strList;
        }
    }
}
