namespace AccessToDB;

public partial class Librarian
{
    public int Id { get; set; }

    public string? Login { get; set; }

    public string? Password { get; set; }

    public string? Email { get; set; }

    public virtual ICollection<Reader> Readers { get; set; } = new List<Reader>();
}
