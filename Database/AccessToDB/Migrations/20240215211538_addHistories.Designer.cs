﻿// <auto-generated />
using System;
using AccessToDB;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace AccessToDB.Migrations
{
    [DbContext(typeof(LibraryContext))]
    [Migration("20240215211538_addHistories")]
    partial class addHistories
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("AccessToDB.Author", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime?>("Birthday")
                        .IsRequired()
                        .HasColumnType("date")
                        .HasColumnName("birthday");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SecondName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Surname")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Authors");
                });

            modelBuilder.Entity("AccessToDB.Book", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("BookingTime")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(30);

                    b.Property<string>("City")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Country")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PublishKey")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PublishingHousesType")
                        .HasColumnType("int");

                    b.Property<int?>("Year")
                        .IsRequired()
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PublishingHousesType");

                    b.ToTable("Books", t =>
                        {
                            t.HasCheckConstraint("CK_MaxMinYear", "Year between 1900 and 2024");
                        });
                });

            modelBuilder.Entity("AccessToDB.Document", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Documents");
                });

            modelBuilder.Entity("AccessToDB.History", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("BookId")
                        .HasColumnType("int");

                    b.Property<int>("BookingTime")
                        .HasColumnType("int");

                    b.Property<int>("ReaderId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("ReturnDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("TakeDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("BookId");

                    b.HasIndex("ReaderId");

                    b.ToTable("History");
                });

            modelBuilder.Entity("AccessToDB.Librarian", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Librarians");
                });

            modelBuilder.Entity("AccessToDB.PublishingHouse", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("PublishingHouses");
                });

            modelBuilder.Entity("AccessToDB.Reader", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("DocumentNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("DocumentTypeId")
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("LibrarianId")
                        .HasColumnType("int");

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Surname")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("DocumentTypeId");

                    b.HasIndex("LibrarianId");

                    b.ToTable("Reader");
                });

            modelBuilder.Entity("AuthorBook", b =>
                {
                    b.Property<int>("AuthorId")
                        .HasColumnType("int");

                    b.Property<int>("BookId")
                        .HasColumnType("int");

                    b.HasKey("AuthorId", "BookId");

                    b.HasIndex("BookId");

                    b.ToTable("AuthorBooks", (string)null);
                });

            modelBuilder.Entity("AccessToDB.Book", b =>
                {
                    b.HasOne("AccessToDB.PublishingHouse", "PublishingHousesTypeNavigation")
                        .WithMany("Books")
                        .HasForeignKey("PublishingHousesType")
                        .IsRequired();

                    b.Navigation("PublishingHousesTypeNavigation");
                });

            modelBuilder.Entity("AccessToDB.History", b =>
                {
                    b.HasOne("AccessToDB.Book", "Book")
                        .WithMany("Histories")
                        .HasForeignKey("BookId")
                        .IsRequired();

                    b.HasOne("AccessToDB.Reader", "Reader")
                        .WithMany("Histories")
                        .HasForeignKey("ReaderId")
                        .IsRequired();

                    b.Navigation("Book");

                    b.Navigation("Reader");
                });

            modelBuilder.Entity("AccessToDB.Reader", b =>
                {
                    b.HasOne("AccessToDB.Document", "DocumentType")
                        .WithMany("Readers")
                        .HasForeignKey("DocumentTypeId")
                        .IsRequired();

                    b.HasOne("AccessToDB.Librarian", "Librarian")
                        .WithMany("Readers")
                        .HasForeignKey("LibrarianId")
                        .IsRequired();

                    b.Navigation("DocumentType");

                    b.Navigation("Librarian");
                });

            modelBuilder.Entity("AuthorBook", b =>
                {
                    b.HasOne("AccessToDB.Author", null)
                        .WithMany()
                        .HasForeignKey("AuthorId")
                        .IsRequired();

                    b.HasOne("AccessToDB.Book", null)
                        .WithMany()
                        .HasForeignKey("BookId")
                        .IsRequired();
                });

            modelBuilder.Entity("AccessToDB.Book", b =>
                {
                    b.Navigation("Histories");
                });

            modelBuilder.Entity("AccessToDB.Document", b =>
                {
                    b.Navigation("Readers");
                });

            modelBuilder.Entity("AccessToDB.Librarian", b =>
                {
                    b.Navigation("Readers");
                });

            modelBuilder.Entity("AccessToDB.PublishingHouse", b =>
                {
                    b.Navigation("Books");
                });

            modelBuilder.Entity("AccessToDB.Reader", b =>
                {
                    b.Navigation("Histories");
                });
#pragma warning restore 612, 618
        }
    }
}
