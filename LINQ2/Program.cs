using System.Collections.Generic;
using System.Text.Json.Nodes;

internal class Program
{
    private static void Main(string[] args)
    {
        var data = new List<object>() {
            "Hello",
            new Book() { Author = "Terry Pratchett", Name = "Guards! Guards!", Pages = 810 },
            new List<int>() {4, 6, 8, 2},
            new string[] {"Hello inside array"},
            new Film() { Author = "Martin Scorsese", Name= "The Departed", Actors = new List<Actor>() {
                new Actor() { Name = "Jack Nickolson", Birthdate = new DateTime(1937, 4, 22)},
                new Actor() { Name = "Leonardo DiCaprio", Birthdate = new DateTime(1974, 11, 11)},
                new Actor() { Name = "Matt Damon", Birthdate = new DateTime(1970, 8, 10)}
            }},
            new Film() { Author = "Gus Van Sant", Name = "Good Will Hunting", Actors = new List<Actor>() {
                new Actor() { Name = "Matt Damon", Birthdate = new DateTime(1970, 8, 10)},
                new Actor() { Name = "Robin Williams", Birthdate = new DateTime(1951, 8, 11)},
            }},
            new Book() { Author = "Stephen King", Name="Finders Keepers", Pages = 200}, 
            "Leonardo DiCaprio"
        };

        Console.WriteLine(string.Join(", ", data.Where(item => !(item is ArtObject))));
        Console.WriteLine(string.Join(", ", data
            .OfType<Film>()
            .SelectMany(film => film.Actors, (film, actor) => actor.Name)));
        Console.WriteLine(data
            .OfType<Film>()
            .SelectMany(film => film.Actors, (film, actor) => actor.Birthdate)
            .Where(date => date.Month == 8)
            .Count());
        Console.WriteLine(string.Join(", ", data
            .OfType<Film>()
            .SelectMany(film => film.Actors, (film, actor) => actor)
            .OrderBy(actor => actor.Birthdate)
            .Take(2)
            .Select(actor => actor.Name)));
        Console.WriteLine(string.Join(", ", data
            .OfType<Book>()
            .GroupBy(book => book.Author, (author, books) => $"{author}: {books.Count()}")));
        Console.WriteLine(string.Join(", ", data
            .OfType<ArtObject>()
            .GroupBy(artObject => artObject.Author, (author, arts) => $"{author}: {arts.Count()}")));
        Console.WriteLine(string.Join("", data
            .OfType<Film>()
            .SelectMany(film => film.Actors, (film, actor) => actor.Name))
            .Replace(" ", "")
            .Distinct()
            .Count());
        Console.WriteLine(string.Join(", ", data
            .OfType<Book>()
            .OrderBy(book => book.Author)
            .ThenBy(book => book.Pages)
            .Select(book => book.Name)));
        Console.WriteLine(string.Join(", ", data
            .OfType<Film>()
            .SelectMany(film => film.Actors)
            .GroupBy(actor => actor.Name)
            .Select(group => $"Actor: {group.Key}, films: {group.Count()}")));
        Console.WriteLine(data.Aggregate(0, (result, item) => result + 
                                                              ((item is Book book) ? book.Pages : 
                                                               (item is List<int> list) ? list.Sum() : 0)));
        Console.WriteLine(string.Join(", ", data
            .OfType<Book>()
            .GroupBy(book => book.Author)
            .ToDictionary(group => group.Key, group => group)));
        Console.WriteLine(string.Join(", ", data
            .OfType<Film>()
            .Where(film => film.Actors.Select(actor => actor.Name).Contains("Matt Damon") && 
                           film.Actors.Select(actor => actor.Name).Intersect(data).Count() == 0)
            .Select(film => film.Name)
            ));
        Console.ReadKey();
    }

    class Actor
    {
        public string Name { get; set; }
        public DateTime Birthdate { get; set; }
    }

    abstract class ArtObject
    {
        public string Author { get; set; }
        public string Name { get; set; }
        public int Year { get; set; }
    }

    class Film : ArtObject
    {
        public int Length { get; set; }
        public IEnumerable<Actor> Actors { get; set; }
    }

    class Book : ArtObject
    {
        public int Pages { get; set; }
    }
}