namespace Client
{
    public class User
    {
        public string? Name { get; }
        string? Email { get; }
        string? Password { get; }

        public User(string? name, string? email, string? password)
        {
            Name = name;
            Email = email;
            Password = password;
        }

        public override string ToString()
        {
            return $"User#Name={Name}&Email={Email}&Password={Password}";
        }
    }
}