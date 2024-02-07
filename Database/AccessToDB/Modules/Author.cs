using System;
using System.Collections.Generic;

namespace AccessToDB;

public partial class Author
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Surname { get; set; }

    public string? SecondName { get; set; }

    public DateTime? Birthday { get; set; }

    public virtual ICollection<Book> Books { get; set; } = new List<Book>();
}
