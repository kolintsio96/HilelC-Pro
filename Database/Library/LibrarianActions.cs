using AccessToDB;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

namespace Library
{
    internal class LibrarianActions
    {
        public LibraryContext Ctx { get; }
        public Librarian? User { get; }

        public LibrarianActions(LibraryContext ctx, IUser? user)
        {
            Ctx = ctx;
            User = (Librarian)user;
            Init();
        }

        public void Init()
        {
            while (true)
            {
                Console.Clear();
                LibrarianOptions input = ReadOperation();
                Console.Clear();
                switch (input)
                {
                    case LibrarianOptions.ViewBooks:
                        ViewBooks();
                        break;
                    case LibrarianOptions.ViewAuthors:
                        ViewAuthors();
                        break;
                    case LibrarianOptions.AddBook:
                        AddBook();
                        break;
                    case LibrarianOptions.UpdateBook:
                        UpdateBook();
                        break;
                    case LibrarianOptions.AddAuthor:
                        AddAuthor();
                        break;
                    case LibrarianOptions.UpdateAuthor:
                        UpdateAuthor();
                        break;
                    case LibrarianOptions.AddReader:
                        AddReader();
                        break;
                    case LibrarianOptions.UpdateReader:
                        UpdateReader();
                        break;
                    case LibrarianOptions.RemoveReader:
                        RemoveReader();
                        break;
                    case LibrarianOptions.History:
                        ViewHistory();
                        break;
                    case LibrarianOptions.Exit:
                        Console.WriteLine("Exiting program");
                        return;
                    default:
                        Console.WriteLine("Invalid option");
                        Console.ReadLine();
                        break;
                }
                Console.Clear();
            }
        }

        private void ViewHistory()
        {
            while (true)
            {
                Console.Clear();
                HistoryOptions input = ReadHistoryOperation();
                Console.Clear();
                switch (input)
                {
                    case HistoryOptions.ViewDebtors:
                        ViewDebtors();
                        break;
                    case HistoryOptions.ViewReaders:
                        ViewReaders();
                        break;
                    case HistoryOptions.ReaderHistory:
                        ReaderHistory();
                        break;
                    case HistoryOptions.Exit:
                        Console.WriteLine("Exiting program");
                        return;
                    default:
                        Console.WriteLine("Invalid option");
                        Console.ReadLine();
                        break;
                }
                Console.Clear();
            }
        }

        private void ReaderHistory()
        {
            string? searchField = ReadString("Enter email of reader: ", true);
            Reader? reader = Ctx.Readers.FirstOrDefault(r => r.Email == searchField);
            var readers = Ctx.Histories.Include(h => h.Book).Include(h => h.Reader).Where(h => h.Reader.Email == reader.Email);

            if (readers.Count() > 0)
            {
                foreach (var debtor in readers)
                {
                    Console.WriteLine($"{reader.Name} - {debtor.Book.Name} - Take at: {debtor.TakeDate} - Returned at: {debtor.ReturnDate}");
                }
            }
            else
            {
                Console.WriteLine($"{reader.Name} dont take books yet!");
            }
            Console.ReadKey();
        }

        private void ViewReaders()
        {
            var readers = Ctx.Histories.Include(h => h.Book).Include(h => h.Reader);
            if (readers.Count() > 0)
            {
                foreach (var reader in readers)
                {
                    Console.WriteLine($"{reader.Reader.Name} - {reader.Book.Name}");
                }
            }
            else
            {
                Console.WriteLine("List of readers empty!");
            }
            Console.ReadKey();
        }

        private void ViewDebtors()
        {
            var debtors = Ctx.Histories.Where(h => DateTime.Now > h.ReturnDate).Include(h => h.Book).Include(h => h.Reader);
            if (debtors.Count() > 0)
            {
                foreach (var debtor in debtors)
                {
                    Console.WriteLine($"{debtor.Reader.Name} - {debtor.Book.Name}");
                }
            } else
            {
                Console.WriteLine("List of debtors empty!");
            }
            Console.ReadKey();
        }

        private Reader newReader()
        {
            string? login = ReadString("Enter login: ");
            string? email = ReadString("Enter email: ", true);
            string? password = ReadString("Enter password: ");
            string? name = ReadString("Enter name: ");
            string? surname = ReadString("Enter surname: ");
            string? documentType = ReadString("Enter document type: ");
            int documentTypeId = Ctx.Documents.FirstOrDefault(d => d.Type == documentType)!.Id;
            string? documentNumber = ReadString("Enter document number: ");
            string? librarianLogin = User!.Login;
            int librarianId = Ctx.Librarians.FirstOrDefault(d => d.Login == librarianLogin)!.Id;
            return new Reader() { Login = login, Email = email, Password = password, Name = name, Surname = surname, DocumentNumber = documentNumber, DocumentTypeId = documentTypeId, LibrarianId = librarianId };
        }

        private void AddReader()
        {
            Reader reader = newReader();
            Ctx.Readers.Add(reader);
            Ctx.SaveChanges();
        }

        private void RemoveReader()
        {
            string? searchField = ReadString("Enter email of reader: ", true);
            Reader? reader = Ctx.Readers.FirstOrDefault(r => r.Email == searchField);
            if(reader != null)
            {
                Ctx.Readers.Remove(reader);
            }
            Ctx.SaveChanges();
        } 

        private void UpdateReader()
        {
            string? searchField = ReadString("Enter email of reader: ", true);
            Reader? reader = Ctx.Readers.FirstOrDefault(r => r.Email == searchField);
            if (reader != null)
            {
                Reader updatedReader = newReader();
                reader.Name = updatedReader.Name;
                reader.Login = updatedReader.Login;
                reader.Email = updatedReader.Email;
                reader.Password = updatedReader.Password;
                reader.Surname = updatedReader.Surname;
                reader.LibrarianId = updatedReader.LibrarianId;
                Ctx.SaveChanges();
            }
        }

        private Book newBook()
        {
            string name = ReadString("Enter name of book: ");
            string publishKey = ReadString("Enter publishing key: ");
            string? publishHouseType = ReadString("Enter type of publishing house: ");
            int publishHouseId = Ctx.PublishingHouses.FirstOrDefault(p => p.Type.StartsWith(publishHouseType))!.Id;
            int year = ReadNumber("Enter year of publishing: ");
            string? country = ReadString("Enter country of publishing: ");
            string? city = ReadString("Enter city of publishing: ");
            int bookingTime = ReadNumber("Enter booking time: ");
            return new Book() { Name = name, PublishKey = publishKey, PublishingHousesType = publishHouseId, Year = year, Country = country, City = city, BookingTime = bookingTime };
        }
        
        private void AddBook()
        {
            Book book = newBook();
            Ctx.Books.Add(book);
            Ctx.SaveChanges();
        }

        private void UpdateBook()
        {
            string? searchField = ReadString("Enter name of book or author of book(or empty): ");
            var book = Ctx.Books.FirstOrDefault(b => b.Name.StartsWith(searchField) || b.Authors.FirstOrDefault(a => a.Name.StartsWith(searchField)) != null);
            if (book != null)
            {
                Book updatedBook = newBook();
                book.Name = updatedBook.Name;
                book.PublishKey = updatedBook.PublishKey;
                book.PublishingHousesType = updatedBook.PublishingHousesType;
                book.Year = updatedBook.Year;
                book.City = updatedBook.City;
                book.Country = updatedBook.Country;
                book.BookingTime = updatedBook.BookingTime;
                Ctx.SaveChanges();
            }
        }

        private Author newAuthor()
        {
            string name = ReadString("Enter name: ");
            string? surname = ReadString("Enter surname: ");
            string? secondName = ReadString("Enter second name: ");
            DateTime? birthday = ReadDate("Enter date of birthday: ");
            return new Author() { Name = name, Surname = surname, SecondName = secondName, Birthday = birthday };
        }

        private void AddAuthor()
        {
            Author author = newAuthor();
            Ctx.Authors.Add(author);
            Ctx.SaveChanges();
        }

        private void UpdateAuthor()
        {
            string? searchField = ReadString("Enter name of author(or empty): ");
            var author = Ctx.Authors.FirstOrDefault(b => b.Name.StartsWith(searchField));
            if (author != null)
            {
                Author updatedAuthor = newAuthor();
                author.Name = updatedAuthor.Name;
                author.Surname = updatedAuthor.Surname;
                author.SecondName = updatedAuthor.SecondName;
                author.Birthday = updatedAuthor.Birthday;                
                Ctx.SaveChanges();
            }
        }

        private void ViewAuthors()
        {
            string? searchField = ReadString("Enter name of author(or empty): ");
            var authors = Ctx.Authors.Where(b => b.Name.StartsWith(searchField));
            foreach (var author in authors)
            {
                Console.WriteLine($"Name: {author.Name}; Surname: {author.Surname}; SecondName: {author.SecondName}; Birthday: {author.Birthday}");
            }
            Console.WriteLine("Press Enter for back to menu...");
            Console.ReadKey();
        }

        private void ViewBooks()
        {
            string? searchField = ReadString("Enter name of book or author of book(or empty): ");
            var books = Ctx.Books.Where(b => b.Name.StartsWith(searchField) || b.Authors.FirstOrDefault(a => a.Name.StartsWith(searchField)) != null);
            foreach (var book in books)
            {
                Console.WriteLine($"Name: {book.Name}; Year: {book.Year}");
            }
            Console.WriteLine("Press Enter for back to menu...");
            Console.ReadKey();
        }

        private LibrarianOptions ReadOperation()
        {
            Console.Write("""
                    1. Find book by name or author 
                    2. Find author by name
                    3. Add book
                    4. Update book
                    5. Add author
                    6. Update author
                    7. Add reader
                    8. Update reader
                    9. Remove reader
                    10. View history
                    11. Exit
                    Choose an option:
                    """);
            string? input = Console.ReadLine();
            bool successfulParsing = Enum.TryParse<LibrarianOptions>(input, out LibrarianOptions result);
            if (!successfulParsing)
            {
                Console.Clear();
                Console.WriteLine("You enter wrong number");
                Console.WriteLine();
                return ReadOperation();
            }
            else if (result < LibrarianOptions.ViewBooks || result > LibrarianOptions.Exit)
            {
                Console.Clear();
                Console.WriteLine($"Please enter number from 1 to 11");
                Console.WriteLine();
                return ReadOperation();
            }
            return result;
        }

        private HistoryOptions ReadHistoryOperation()
        {
            Console.Write("""
                    1. View list of debtors
                    2. View list of readers
                    3. View history by reader
                    4. Exit
                    Choose an option:
                    """);
            string? input = Console.ReadLine();
            bool successfulParsing = Enum.TryParse<HistoryOptions>(input, out HistoryOptions result);
            if (!successfulParsing)
            {
                Console.Clear();
                Console.WriteLine("You enter wrong number");
                Console.WriteLine();
                return ReadHistoryOperation();
            }
            else if (result < HistoryOptions.ViewDebtors || result > HistoryOptions.Exit)
            {
                Console.Clear();
                Console.WriteLine($"Please enter number from 1 to 4");
                Console.WriteLine();
                return ReadHistoryOperation();
            }
            return result;
        }

        private string ReadString(string message, bool isEmail = false)
        {
            Console.Write(message);
            string? input = Console.ReadLine();
            string regex = @"^[^@\s]+@[^@\s]+\.(com|net|org|gov)$";

             if (isEmail && !Regex.IsMatch(input, regex, RegexOptions.IgnoreCase))
            {
                Console.WriteLine("Email is invalid!");
                return ReadString(message);
            }
            return input;

        }

        private DateTime ReadDate(string message)
        {
            Console.Write(message);
            string input = Console.ReadLine();
            bool succesfullParsing = DateTime.TryParse(input, out DateTime result);
            if (!succesfullParsing)
            {
                Console.WriteLine("You enter wrong date");
                return ReadDate(message);
            }
            return result;

        }

        private static int ReadNumber(string message)
        {
            Console.Write(message);
            string input = Console.ReadLine();
            bool succesfullParsing = int.TryParse(input, out int result);
            if (!succesfullParsing)
            {
                Console.WriteLine("You enter wrong number");
                return ReadNumber(message);
            }
            return result;

        }
    }
}