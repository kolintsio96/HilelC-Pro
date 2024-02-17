using AccessToDB;

namespace Library
{
    internal class ReaderActions
    {
        public LibraryContext Ctx { get; }
        public IUser? User { get; }

        public ReaderActions(LibraryContext ctx, IUser? user)
        {
            Ctx = ctx;
            User = user;
            Init();
        }

        public void Init()
        {
            while (true)
            {
                Console.Clear();
                ReaderOptions input = ReadOperation();
                Console.Clear();
                switch (input)
                {
                    case ReaderOptions.ViewBooks:
                        ViewBooks();
                        break;
                    case ReaderOptions.ViewAuthors:
                        ViewAuthors();
                        break;
                    case ReaderOptions.History:
                        ViewHistory();
                        break;
                    case ReaderOptions.Take:
                        TakeBook();
                        break;
                    case ReaderOptions.Exit:
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
            var historyList = Ctx.Histories.Where(h => h.ReaderId == User!.Id);
            var overdueBooks = historyList.Where(h => DateTime.Now > h.ReturnDate);
            var takenBooks = historyList.Where(h => DateTime.Now <= h.ReturnDate).OrderByDescending(d => d.ReturnDate);

            if (overdueBooks.Count() > 0)
            {
                Console.WriteLine("Overdue books:");
                foreach (var history in overdueBooks)
                {
                    var book = Ctx.Books.FirstOrDefault(b => b.Id == history.BookId);
                    Console.WriteLine($"Name: {book!.Name}; ReturnDate: {history.ReturnDate}");
                }
                Console.WriteLine();

            }

            if (takenBooks.Count() > 0)
            {
                Console.WriteLine("Taken books:");
                foreach (var history in overdueBooks)
                {
                    var book = Ctx.Books.FirstOrDefault(b => b.Id == history.BookId);
                    Console.WriteLine($"Name: {book!.Name}; ReturnDate: {history.ReturnDate}");
                }
                Console.WriteLine();
            } 
            
            if (overdueBooks.Count() == 0 &&  takenBooks.Count() > 0)
            {
                Console.WriteLine("History list empty!");
            }
            Console.WriteLine("Press Enter for back to menu...");
            Console.ReadKey();
        }

        private void ViewAuthors()
        {
            string? searchField = ReadString("Enter name of author(or empty): ");
            var authors = Ctx.Authors.Where(b => b.Name.StartsWith(searchField)); ;
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

        private void TakeBook()
        {
            string? searchField = ReadString("Enter name of book or author of book(or empty): ");
            var books = Ctx.Books.Where(b => b.Name.StartsWith(searchField) || b.Authors.FirstOrDefault(a => a.Name.StartsWith(searchField)) != null).ToList();
            int countOfTakenBook = 0;
            foreach (var book in books)
            {
                var bookTaken = Ctx.Histories.FirstOrDefault(h => h.ReaderId == User.Id && h.BookId == book.Id) != null;
                if (!bookTaken)
                {
                    countOfTakenBook++;
                    Ctx.Histories.Add(new History() { ReaderId = User.Id, BookId = book.Id, BookingTime = book.BookingTime, TakeDate = DateTime.Now, ReturnDate = DateTime.Now.AddDays(book.BookingTime) });
                    Ctx.SaveChanges();
                }
            }
            Console.WriteLine($"I take {countOfTakenBook} books.");
            Console.WriteLine("Press Enter for back to menu...");
            Console.ReadKey();
        }

        private string ReadString(string message)
        {
            Console.Write(message);
            string input = Console.ReadLine();
            return input;

        }

        private ReaderOptions ReadOperation()
        {
            Console.Write("""
                    1. Find book by name or author 
                    2. Find author by name
                    3. View history
                    4. Take a book
                    5. Exit
                    Choose an option:
                    """);
            string? input = Console.ReadLine();
            bool successfulParsing = Enum.TryParse<ReaderOptions>(input, out ReaderOptions result);
            if (!successfulParsing)
            {
                Console.Clear();
                Console.WriteLine("You enter wrong number");
                Console.WriteLine();
                return ReadOperation();
            }
            else if (result < ReaderOptions.ViewBooks || result > ReaderOptions.Exit)
            {
                Console.Clear();
                Console.WriteLine($"Please enter number from 1 to 5");
                Console.WriteLine();
                return ReadOperation();
            }
            return result;
        }
    }
}