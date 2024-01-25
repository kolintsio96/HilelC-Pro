namespace Client
{
    public struct User
    {
        string? Name { get; }
        string? Email { get; }
        string? Password { get; }

        public User(string? name, string? email, string? password)
        {
            Name = name;
            Email = email;
            Password = password;
        }
    }
}