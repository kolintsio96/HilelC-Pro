using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Server
{
    class ChatClient
    {
        private readonly TcpClient client;
        private readonly NetworkStream stream;
        private readonly List<User> userList;

        public EndPoint? EndPoint => client?.Client?.RemoteEndPoint;

        public ChatClient(TcpClient client)
        {
            this.client = client;
            this.stream = client.GetStream();
            this.userList = new List<User>();
        }

        public Task StartReadAsync()
        {
            return Task.Run(async () =>
            {
                var reader = new StreamReader(stream);
                while (true)
                {
                    var msg = await reader.ReadLineAsync();
                    var splittedMsg = msg.Split("#");
                    switch (splittedMsg[0])
                    {
                        case "User":
                            User user = ParseUser(splittedMsg[1]);
                            userList.Add(user);
                            break;
                        case "Login":
                            User loginData = ParseUser(splittedMsg[1]);
                            bool loginSuccess = userList.FindAll(user => user.Email == loginData.Email && user.Password == loginData.Password).Count() > 0;
                            await SendMessage($"{EndPoint}@Auth={loginSuccess}");
                            break;
                        default:
                            Console.WriteLine($"[{EndPoint}] Message: {msg}");
                            break;
                    }
                    MessageReceived?.Invoke(this, msg);
                }
            });
        }

        public event EventHandler<string?> MessageReceived;

        public async Task SendMessage(string? message)
        {
            var writer = new StreamWriter(stream);
            await writer.WriteLineAsync(message);
            await writer.FlushAsync();
        }

        private User ParseUser(string msg)
        {
            User user = new User();
            var splittedMsg = msg.Split("&");
            foreach (var item in splittedMsg)
            {
                var pair = item.Split("=");
                
                switch (pair[0])
                {
                    case "Name":
                        user.Name = pair[1];
                        break;
                    case "Email":
                        user.Email = pair[1];
                        break;
                    case "Password":
                        user.Password = pair[1];
                        break;
                }
            }

            return user;
        }
    }

    internal class Program
    {

        static List<ChatClient> clients = new List<ChatClient>();

        static async Task Main(string[] args)
        {
            var task = Task.Run(async () =>
            {
                UdpClient udpClient = new UdpClient(7701, AddressFamily.InterNetwork);

                while (true)
                {
                    var result = await udpClient.ReceiveAsync();
                    var message = Encoding.UTF8.GetString(result.Buffer);
                    if (message == "SCAN BY COOL CHAT SERVER")
                    {
                        message = $"YES PORT:{7700}";
                        var sendTask = udpClient.SendAsync(Encoding.UTF8.GetBytes(message), result.RemoteEndPoint);
                    }
                }
            });

            var server = new TcpListener(System.Net.IPAddress.Any, 7700);
            server.Start();
            try
            {
                Console.WriteLine($"Server started on: {server.LocalEndpoint}");

                while (true)
                {
                    TcpClient client = await server.AcceptTcpClientAsync();

                    Console.WriteLine($"Client {client.Client.RemoteEndPoint} was connected");

                    var chatClient = new ChatClient(client);
                    chatClient.MessageReceived += ChatClient_MessageReceived;
                    clients.Add(chatClient);

                    await chatClient.StartReadAsync();
                }
            }
            finally
            {
                server.Stop();
                Console.WriteLine("Server is stopped");
            }
        }

        private static async void ChatClient_MessageReceived(object? sender, string? message)
        {
            if (message.IndexOf("Login#") != -1 || message.IndexOf("User#") != -1) return;
            var chatClient = (ChatClient?)sender;
            foreach (var client in clients)
            {
                await client.SendMessage($"{chatClient?.EndPoint}@{message}");
            }
        }
    }
}