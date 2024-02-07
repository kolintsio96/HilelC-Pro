using AccessToDB;
namespace Library
{
    internal class Program
    {
        static void Main(string[] args)
        {
            LibraryContext ctx = new LibraryContext();
            Auth auth = new Auth(ctx);


        }
    }
}
