namespace Library
{
    enum Options
    {
        Login = 1,
        Registration = 2,
        Exit = 3,
    }

    enum LibrarianOptions
    {
        ViewBooks = 1,
        ViewAuthors = 2,
        AddBook = 3,
        UpdateBook = 4,
        AddAuthor = 5,
        UpdateAuthor = 6,
        AddReader = 7,
        UpdateReader = 8,
        RemoveReader = 9,
        History = 10,
        Exit = 11,
    }

    enum ReaderOptions
    {
        ViewBooks = 1,
        ViewAuthors = 2,
        History = 3,
        Take = 4,
        Exit = 5,
    }

    enum HistoryOptions
    {
        ViewDebtors = 1,
        ViewReaders = 2,
        ReaderHistory = 3,
        Exit = 4,
    }
}