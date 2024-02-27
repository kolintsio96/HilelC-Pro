namespace AccessToDB
{
    public interface IUser
    {
        public int Id { get; set; }
        string Email { get; set; }
        string Password { get; set; }
    }
}
