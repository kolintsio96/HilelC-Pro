namespace Server
{
    public struct User
    {
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }

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