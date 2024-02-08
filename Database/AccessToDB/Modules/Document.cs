using System;
using System.Collections.Generic;

namespace AccessToDB;

public partial class Document
{
    public int Id { get; set; }

    public string? Type { get; set; }

    public virtual ICollection<Reader> Readers { get; set; } = new List<Reader>();
}
