namespace AccessToDB;

public class Reader : IUser
{
    public int Id { get; set; }

    public string? Login { get; set; }

    public string? Password { get; set; }

    public string? Email { get; set; }

    public string? Name { get; set; }

    public string? Surname { get; set; }

    public int DocumentTypeId { get; set; }

    public string? DocumentNumber { get; set; }

    public int LibrarianId { get; set; }

    public virtual Document DocumentType { get; set; } = null!;

    public virtual Librarian Librarian { get; set; } = null!;

    public virtual ICollection<History> Histories { get; set; } = new List<History>();
}
