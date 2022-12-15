using System.Net;
using System.Net.Sockets;

namespace Common.Basic.Common.Basic.Net
{
    public class NetSocketExtensions
    {
        public static string GetLocalIPAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                    return ip.ToString();

            return string.Empty;
        }
    }
}
