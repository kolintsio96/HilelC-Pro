using DataStructers.Tests.Interfaces;

namespace DataStructers.Tests
{
    abstract class Tests : ITestsGroup
    {
        private int _indentCount;

        protected int IndentCount
        {
            get => _indentCount;
            set
            {
                if (_indentCount == value) return;
                if (value < 0 || value > 6)
                {
                    throw new ArgumentException();
                }

                _indentCount = value;
            }
        }

        public abstract string Title { get; }

        public void Run()
        {
            Console.BackgroundColor = ConsoleColor.Gray;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine(Title);
            Console.ResetColor();

            IndentCount++;

            RunTests();
        }

        protected abstract void RunTests();

        protected void ShowTestResult(string testName, bool isSuccess)
        {
            Console.ResetColor();

            for (int num = 1; num <= IndentCount; num++) Console.Write("  ");
            Console.Write($"{testName}: ");

            string resultMsg;
            if (isSuccess)
            {
                Console.BackgroundColor = ConsoleColor.Green;
                Console.ForegroundColor = ConsoleColor.White;
                resultMsg = "SUCCESS!";
            }
            else
            {
                Console.BackgroundColor = ConsoleColor.Red;
                Console.ForegroundColor = ConsoleColor.White;
                resultMsg = "FAILED!";
            }

            if (Console.CursorLeft < 35)
            {
                Console.SetCursorPosition(40, Console.CursorTop);
            }
            Console.Write($"{resultMsg}");
            Console.ResetColor();
            Console.WriteLine(" ");
        }

        public IEnumerable<string> GetTestList()
        {
            throw new NotImplementedException();
        }

        public event Action<string, TestState> TestCompleted;
    }
}
