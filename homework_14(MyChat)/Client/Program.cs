using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Client
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            using TcpClient tcpClient = new TcpClient(AddressFamily.InterNetwork);

            try
            {
                UdpClient scanClient = new UdpClient(AddressFamily.InterNetwork);
                scanClient.Client.ReceiveTimeout = 2000;

                UdpReceiveResult result = default;
                string message = string.Empty;

                try
                {
                    for (int i = 1; i <= 5; i++)
                    {
                        Console.WriteLine("Scan network for the chat server. Try " + i + ".");
                        await scanClient.SendAsync(Encoding.UTF8.GetBytes("SCAN BY COOL CHAT SERVER"), new IPEndPoint(IPAddress.Broadcast, 7701));
                        try
                        {
                            IPEndPoint? remoteEndPoint = null;
                            //result = await scanClient.ReceiveAsync();
                            var data = scanClient.Receive(ref remoteEndPoint);
                            result = new UdpReceiveResult(data, remoteEndPoint);
                            message = Encoding.UTF8.GetString(result.Buffer);

                            Console.WriteLine($"Scan receive message [{message}] from {remoteEndPoint}");

                            if (!message.StartsWith("YES"))
                            {
                                Console.WriteLine("Servers not found!");
                                return;
                            }
                            else
                            {
                                break;
                            }
                        }
                        catch
                        {
                            Console.WriteLine("Servers not found!");
                        }
                    }
                }
                catch
                {
                    Console.WriteLine("Servers not found!");
                    return;
                }

                var port = int.Parse(message.Split(':')[1]);
                var ip = result.RemoteEndPoint.Address;
                Console.WriteLine($"Server found at {ip}:{port}");

                tcpClient.Connect(ip, port/*"192.168.1.253", 7700*/);

                Console.WriteLine("Connection established!");

                using var stream = tcpClient.GetStream();

                await Authorization.Process(stream);
            }
            catch
            {
                Console.WriteLine("Client was disconnected");
            }
        }
    }
}
