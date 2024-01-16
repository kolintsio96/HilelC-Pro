using System.Diagnostics;
using Threads;

internal class Program
{
    private static void Main(string[] args)
    {
        var threadCount = 16;

        var arr = new int[1_000_000];

        var gen = new GenRandomArray(threadCount, arr);
        gen.Process();

        var maxProc = new MaxOfArray(threadCount, arr);

        var minProc = new MinOfArray(threadCount, arr);
        
        var sumProc = new SumOfArray(threadCount, arr);

        var averageProc = new AvarageOfArray(threadCount, arr);

        var copyProc = new CopyPartOfArray<int>(threadCount, arr, 245, 1785);

        var charArr = new char[1_000_000];
        var genChar = new GenRandomArray<char>(threadCount, charArr, (random) => (char)random.Next(32, 58));
        genChar.Process();

        var charProc = new CharDictionary<char>(threadCount, charArr);

        var strArr = new string[1_000_000];
        var wordArr = new string[5] { "lorem", "ipsum", "dolor", "sit", "amet" };
        var genStr = new GenRandomArray<string>(threadCount, strArr, (random) => wordArr[random.Next(0, 5)]);
        genStr.Process();

        var stringProc = new CharDictionary<string>(threadCount, strArr);


        IThreadsForArray[] proccesses = { maxProc, minProc, sumProc, averageProc, copyProc, charProc, stringProc };

        PrintResult(proccesses);
    }

    private static void PrintResult(IThreadsForArray[] proccesses)
    { 
        foreach (var proc in proccesses)
        {
            var sw = Stopwatch.StartNew();
            proc.Process();
            Console.WriteLine($"Time: {sw.Elapsed}");
        }
    }
}