using System;
using System.Collections.Generic;

namespace AccessToDB;

public partial class PublishingHouse
{
    public int Id { get; set; }

    public string? Type { get; set; }

    public virtual ICollection<Book> Books { get; set; } = new List<Book>();
}
