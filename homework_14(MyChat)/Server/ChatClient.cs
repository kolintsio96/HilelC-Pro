using System.Net.Sockets;
using System.Net;
using System.Text.Json;

namespace Server
{
    public class ChatClient
    {
        private readonly TcpClient client;
        private readonly NetworkStream stream;
        private readonly List<User> userList;
        private readonly List<string> history;

        public EndPoint? EndPoint => client?.Client?.RemoteEndPoint;

        public ChatClient(TcpClient client)
        {
            this.client = client;
            this.stream = client.GetStream();
            this.userList = new List<User>();
            this.history = new List<string>();
        }

        public Task StartReadAsync()
        {
            return Task.Run(async () =>
            {
                var reader = new StreamReader(stream);
                while (true)
                {
                    var msg = await reader.ReadLineAsync();
                    history.Add(new History($"[{EndPoint}]: {msg}").Log);
                    SaveToFile();
                    ParseMessage(msg);
                    MessageReceived?.Invoke(this, msg);
                }
            });
        }

        private async void ParseMessage(string msg)
        {
            var splittedMsg = msg.Split("#");
            switch (splittedMsg[0])
            {
                case "User":
                    User user = ParseUser(splittedMsg[1]);
                    userList.Add(user);
                    break;
                case "Login":
                    User loginData = ParseUser(splittedMsg[1]);
                    User loginnedUser = userList.Find(user => user.Email == loginData.Email && user.Password == loginData.Password);
                    bool loginSuccess = loginnedUser.Email != null;
                    await SendMessage($"{EndPoint}#Auth={loginSuccess}&{loginnedUser}");
                    break;
                default:
                    Console.WriteLine($"[{EndPoint}] Message: {msg}");
                    break;
            }
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

        public void SaveToFile(string path = "")
        {
            string fileName = "History.json";
            string filePath = Path.Combine(path == "" ? Directory.GetCurrentDirectory() : path, fileName);
            string jsonData = JsonSerializer.Serialize(history, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(filePath, jsonData);
        }
    }
}
