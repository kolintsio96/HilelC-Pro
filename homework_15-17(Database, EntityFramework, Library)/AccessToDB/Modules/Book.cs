﻿namespace AccessToDB;

public partial class Book
{
    public int Id { get; set; }

    public string Name { get; set; }

    public string? PublishKey { get; set; }

    public int PublishingHousesType { get; set; }

    public int? Year { get; set; }

    public string? Country { get; set; }

    public string? City { get; set; }

    public int BookingTime { get; set; }

    public virtual PublishingHouse PublishingHousesTypeNavigation { get; set; } = null!;

    public virtual ICollection<Author> Authors { get; set; } = new List<Author>();

    public virtual ICollection<History> Histories { get; set; } = new List<History>();
}
