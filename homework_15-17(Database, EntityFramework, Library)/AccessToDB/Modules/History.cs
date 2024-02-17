namespace AccessToDB;

public partial class History
{
    public int Id { get; set; }
    public int BookId{ get; set; }
    public int ReaderId{ get; set; }
    public DateTime TakeDate { get; set; }
    public int BookingTime{ get; set; }
    public DateTime? ReturnDate { get; set; }
    public virtual Reader Reader{ get; set; } = null!;
    public virtual Book Book{ get; set; } = null!;
}
