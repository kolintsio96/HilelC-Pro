using AccessToDB;
using System.Text.RegularExpressions;
using AccessToDB;
using System.Linq;

namespace Library
{
    public class Auth
    {
        public Auth(LibraryContext ctx)
        {
            Ctx = ctx;
            Init();
        }

        public LibraryContext Ctx { get; }

        private Options ReadOperation()
        {
            Console.Write("""
                    1. Login 
                    2. Registration
                    3. Exit
                    Choose an option:
                    """);
            string? input = Console.ReadLine();
            bool successfulParsing = Enum.TryParse<Options>(input, out Options result);
            if (!successfulParsing)
            {
                Console.Clear();
                Console.WriteLine("You enter wrong number");
                Console.WriteLine();
                return ReadOperation();
            }
            else if (result < Options.Login || result > Options.Exit)
            {
                Console.Clear();
                Console.WriteLine($"Please enter number from 1 to 3");
                Console.WriteLine();
                return ReadOperation();
            }
            return result;
        }

        private string ReadString(string message, bool isEmail = false)
        {
            Console.Write(message);
            string? input = Console.ReadLine();
            string regex = @"^[^@\s]+@[^@\s]+\.(com|net|org|gov)$";

            if (String.IsNullOrEmpty(input))
            {
                Console.WriteLine("Field can not be null or empty");
                return ReadString(message);
            }
            else if (isEmail && !Regex.IsMatch(input, regex, RegexOptions.IgnoreCase))
            {
                Console.WriteLine("Email is invalid!");
                return ReadString(message);
            }
            return input;

        }

        private void Login()
        {
            string? email = ReadString("Enter email: ", true);
            string? password = ReadString("Enter password: ");
            bool loginSuccess = Ctx.Librarians.FirstOrDefault(l => l.Email == email && l.Password == password) != null;
            Console.WriteLine($"Login {(loginSuccess ? "success" : "unsuccess")}");
            Console.ReadKey();
        }

        private void Registration()
        {
            string? login = ReadString("Enter login: ");
            string? email = ReadString("Enter email: ", true);
            string? password = ReadString("Enter password: ");
            Ctx.Librarians.Add(new Librarian() { Login = login, Email = email, Password = password });
            Ctx.SaveChanges();
            Console.WriteLine("Librarian registration was successful!");
            Console.ReadKey();
        }

        public void Init()
        {
            while (true)
            {
                Console.Clear();
                Options input = ReadOperation();
                switch (input)
                {
                    case Options.Login:
                        Login();
                        break;
                    case Options.Registration:
                        Registration();
                        break;
                    case Options.Exit:
                        Console.WriteLine("Exiting program");
                        return;
                    default:
                        Console.WriteLine("Invalid option");
                        Console.ReadLine();
                        break;
                }
                Console.Clear();
            }
        }
    }
}
