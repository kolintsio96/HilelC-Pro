internal class Program
{
    private static void Main(string[] args)
    {
        string str1 = "aaa;abb;ccc;dap";
        string str2 = "aaa;xabbx;abb;ccc;dap";
        string str3 = "aaa;xabbx;abb;ccc;dap;zh";
        string str4 = "baaa;aabbb;aaa;xabbx;abb;ccc;dap;zh";
        Console.WriteLine(string.Join(",", Enumerable.Range(10, 41)));
        Console.WriteLine(string.Join(",", Enumerable.Range(10, 41).Where(number => number % 3 == 0)));
        Console.WriteLine(string.Join(",", Enumerable.Repeat("Linq", 10)));
        Console.WriteLine(string.Join(",", str1.Split(";").Where(word => word.Contains("a"))));
        Console.WriteLine(string.Join(",", str1.Split(";").Select(word => word.Sum(letter => letter == 'a' ? 1 : 0))));
        Console.WriteLine(str2.Contains("abb"));
        Console.WriteLine(str2.Split(";").FirstOrDefault(word => word.Length == str2.Split(";").Max(w => w.Length)));
        Console.WriteLine(str2.Split(";").Average(word => word.Length));
        Console.WriteLine(string.Join("", str3.Split(";").FirstOrDefault(word => word.Length == str3.Split(";").Min(w => w.Length))!.Reverse()));
        Console.WriteLine(str4.Split(";").First(word => word.StartsWith("aa")).Skip(2).All(letter => letter == 'b'));
        Console.WriteLine(str4.Split(";").Count(word => word.EndsWith("bb")) <= 2 ? "Not Found" : str4.Split(";").Last(word => word.EndsWith("bb")));
    }
}