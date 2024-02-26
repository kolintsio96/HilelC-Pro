using Microsoft.EntityFrameworkCore;

namespace AccessToDB;

public partial class LibraryContext : DbContext
{
    public LibraryContext(DbContextOptions<LibraryContext> options)
            : base(options)
    {
    }

    public virtual DbSet<Author> Authors { get; set; }

    public virtual DbSet<Book> Books { get; set; }

    public virtual DbSet<Document> Documents { get; set; }

    public virtual DbSet<Librarian> Librarians { get; set; }

    public virtual DbSet<PublishingHouse> PublishingHouses { get; set; }

    public virtual DbSet<Reader> Readers { get; set; }
    
    public virtual DbSet<History> Histories { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Data Source=KOLINTSO;Initial Catalog=Library;Integrated Security=True;Trust Server Certificate=True");
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Author>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.Property(e => e.Id);
            entity.Property(e => e.Birthday)
                .HasColumnType("date")
                .IsRequired()
                .HasColumnName("birthday");
            entity.Property(e => e.Name).IsRequired();
            entity.Property(e => e.SecondName);
            entity.Property(e => e.Surname);

            entity.HasMany(d => d.Books).WithMany(p => p.Authors)
                .UsingEntity<Dictionary<string, object>>(
                    "AuthorBook",
                    r => r.HasOne<Book>().WithMany()
                        .HasForeignKey("BookId")
                        .OnDelete(DeleteBehavior.ClientSetNull),
                    l => l.HasOne<Author>().WithMany().HasForeignKey("AuthorId").OnDelete(DeleteBehavior.ClientSetNull),
                    j =>
                    {
                        j.HasKey("AuthorId", "BookId");
                        j.ToTable("AuthorBooks");
                        j.IndexerProperty<int>("AuthorId");
                        j.IndexerProperty<int>("BookId");
                    });
        });

        modelBuilder.Entity<Book>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.Property(e => e.Id);
            entity.Property(e => e.City);
            entity.Property(e => e.Country);
            entity.Property(e => e.Name).IsRequired();
            entity.Property(e => e.PublishKey);
            entity.Property(e => e.PublishingHousesType).IsRequired();
            entity.Property(e => e.Year).IsRequired();
            entity.Property(e => e.BookingTime).HasDefaultValue(30);

            entity.HasOne(d => d.PublishingHousesTypeNavigation).WithMany(p => p.Books).HasForeignKey(d => d.PublishingHousesType).OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<Book>().ToTable(t => t.HasCheckConstraint("CK_MaxMinYear", "Year between 1900 and 2024"));

        modelBuilder.Entity<Document>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.Property(e => e.Id);
            entity.Property(e => e.Type).IsRequired();
        });

        modelBuilder.Entity<Librarian>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.Property(e => e.Id);
            entity.Property(e => e.Email).IsRequired();
            entity.Property(e => e.Login).IsRequired();
            entity.Property(e => e.Password).IsRequired();
        });

        modelBuilder.Entity<PublishingHouse>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.Property(e => e.Id);
            entity.Property(e => e.Type).IsRequired();
        });

        modelBuilder.Entity<Reader>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.Property(e => e.Id);
            entity.Property(e => e.DocumentNumber);
            entity.Property(e => e.DocumentTypeId);
            entity.Property(e => e.Email).IsRequired();
            entity.Property(e => e.LibrarianId).IsRequired();
            entity.Property(e => e.Login).IsRequired();
            entity.Property(e => e.Name).IsRequired();
            entity.Property(e => e.Password).IsRequired();
            entity.Property(e => e.Surname);

            entity.HasOne(d => d.Librarian).WithMany(p => p.Readers)
                .HasForeignKey(d => d.LibrarianId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<History>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.Property(e => e.Id);
            entity.Property(e => e.BookId).IsRequired();
            entity.Property(e => e.ReaderId).IsRequired();
            entity.Property(e => e.TakeDate).IsRequired();
            entity.Property(e => e.ReturnDate);
            entity.Property(e => e.BookingTime).IsRequired();

            entity.HasOne(d => d.Book).WithMany(p => p.Histories)
                .HasForeignKey(d => d.BookId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.Reader).WithMany(p => p.Histories)
                .HasForeignKey(d => d.ReaderId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
