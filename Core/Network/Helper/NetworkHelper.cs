using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;

namespace Core.Network.Helper
{
    public static class NetworkHelper
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
                        if (ip.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                        {
                            dict.Add(item.Description, ip.Address.ToString());
                        }
                    }
                }
            }

            return dict;
        }
    }
}
