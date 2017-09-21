using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;

namespace Core.Network.Extensions
{
    public static class Helper
    {
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
