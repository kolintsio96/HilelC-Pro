using Microsoft.EntityFrameworkCore;

namespace AccessToDB;

public partial class LibraryContext : DbContext
{
    public LibraryContext()
    {
        Database.EnsureCreated();
    }

    public virtual DbSet<Author> Authors { get; set; }

    public virtual DbSet<Book> Books { get; set; }

    public virtual DbSet<Document> Documents { get; set; }

    public virtual DbSet<Librarian> Librarians { get; set; }

    public virtual DbSet<PublishingHouse> PublishingHouses { get; set; }

    public virtual DbSet<Reader> Readers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Data Source=KOLINTSO;Initial Catalog=Library;Integrated Security=True;Trust Server Certificate=True");
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Author>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Authors__3213E83F0AF23D61");

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
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__AuthorBoo__bookI__49C3F6B7"),
                    l => l.HasOne<Author>().WithMany()
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__AuthorBoo__autho__48CFD27E"),
                    j =>
                    {
                        j.HasKey("AuthorId", "BookId").HasName("PK__AuthorBo__46996BA909D4D79D");
                        j.ToTable("AuthorBooks");
                        j.IndexerProperty<int>("AuthorId");
                        j.IndexerProperty<int>("BookId");
                    });
        });

        modelBuilder.Entity<Book>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Books__3213E83F830CD8C0");

            entity.Property(e => e.Id);
            entity.Property(e => e.City);
            entity.Property(e => e.Country);
            entity.Property(e => e.Name).IsRequired();
            entity.Property(e => e.PublishKey);
            entity.Property(e => e.PublishingHousesType).IsRequired();
            entity.Property(e => e.Year).IsRequired();

            entity.HasOne(d => d.PublishingHousesTypeNavigation).WithMany(p => p.Books)
                .HasForeignKey(d => d.PublishingHousesType)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Books__publishin__44FF419A");
        });

        modelBuilder.Entity<Book>().ToTable(t => t.HasCheckConstraint("CK_MaxMinYear", "Year between 1900 and 2024"));

        modelBuilder.Entity<Document>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Document__3213E83F6B18BB8D");

            entity.Property(e => e.Id);
            entity.Property(e => e.Type).IsRequired();
        });

        modelBuilder.Entity<Librarian>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Libraria__3213E83F6C706801");

            entity.Property(e => e.Id);
            entity.Property(e => e.Email).IsRequired();
            entity.Property(e => e.Login).IsRequired();
            entity.Property(e => e.Password).IsRequired();
        });

        modelBuilder.Entity<PublishingHouse>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Publishi__3213E83FA44B0A0A");

            entity.Property(e => e.Id);
            entity.Property(e => e.Type).IsRequired();
        });

        modelBuilder.Entity<Reader>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Readers__3213E83F87F02B2E");

            entity.Property(e => e.Id);
            entity.Property(e => e.DocumentNumber);
            entity.Property(e => e.DocumentTypeId);
            entity.Property(e => e.Email).IsRequired();
            entity.Property(e => e.LibrarianId).IsRequired();
            entity.Property(e => e.Login).IsRequired();
            entity.Property(e => e.Name).IsRequired();
            entity.Property(e => e.Password).IsRequired();
            entity.Property(e => e.Surname);

            entity.HasOne(d => d.DocumentType).WithMany(p => p.Readers)
                .HasForeignKey(d => d.DocumentTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Readers__documen__38996AB5");

            entity.HasOne(d => d.Librarian).WithMany(p => p.Readers)
                .HasForeignKey(d => d.LibrarianId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Readers__librari__398D8EEE");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
