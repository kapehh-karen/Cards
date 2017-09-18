using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;

namespace Core
{
    public static class Helper
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
                            dict.Add(item.Description, ip.Address.ToString());
                        }
                    }
                }
            }

            return dict;
        }

        public static bool IsEstablished(this TcpClient tcpClient)
        {
            foreach (var state in tcpClient.GetState())
            {
                if (state == TcpState.Established)
                    return true;
            }
            return false;
        }

        public static IEnumerable<TcpState> GetState(this TcpClient tcpClient)
        {
            if (tcpClient == null || tcpClient.Client == null || !tcpClient.Client.Connected)
                return new TcpState[0];

            return IPGlobalProperties.GetIPGlobalProperties()
                .GetActiveTcpConnections()
                .Where(x => x.LocalEndPoint.Equals(tcpClient.Client.LocalEndPoint))
                .Select(x => x.State);
        }
    }
}
