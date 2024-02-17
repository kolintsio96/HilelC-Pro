using DataStructers.Tests.Interfaces;
using System.Drawing;

namespace DataStructers
{
    class ConsoleTestRenderer
    {
        class TestView
        {
            public string? Name { get; init; }
            public Point Position { get; init; }
        }

        private readonly ITestsGroup[] _testList;
        private readonly System.Collections.Generic.List<TestView> _testViews = new System.Collections.Generic.List<TestView>();

        public ConsoleTestRenderer(ITestsGroup[] testList)
        {
            this._testList = testList;
            foreach (var test in _testList) test.TestCompleted += TestStateChanged;
        }

        public void Show()
        {
            foreach (var testGroup in _testList)
            {
                Console.BackgroundColor = ConsoleColor.Gray;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.WriteLine(testGroup.Title);
                Console.ResetColor();

                foreach (var test in testGroup.GetTestList())
                {
                    Console.Write("  ");
                    Console.Write($"{test}: ");

                    string? resultMsg = Enum.GetName(TestState.Pending);
                    Console.BackgroundColor = ConsoleColor.Gray;
                    Console.ForegroundColor = ConsoleColor.Black;
                    if (Console.CursorLeft < 35)
                    {
                        Console.SetCursorPosition(40, Console.CursorTop);
                    }

                    _testViews.Add(new TestView { Name = test, Position = new Point(Console.CursorLeft, Console.CursorTop) });

                    Console.Write($"{resultMsg}");
                    Console.ResetColor();
                    Console.WriteLine(" ");
                }
            }
        }

        public void TestStateChanged(string testName, TestState state)
        {
            var testView = _testViews.Find(view => view.Name == testName);
            if (testView != null)
            {
                Point currentPos = new Point(Console.CursorLeft, Console.CursorTop);
                Console.SetCursorPosition(testView.Position.X, testView.Position.Y);

                if (state == TestState.Success)
                {
                    Console.BackgroundColor = ConsoleColor.Green;
                    Console.ForegroundColor = ConsoleColor.White;
                }
                else
                {
                    Console.BackgroundColor = ConsoleColor.Red;
                    Console.ForegroundColor = ConsoleColor.White;
                }

                Console.Write(Enum.GetName(state)?.ToUpper());

                Console.SetCursorPosition(currentPos.X, currentPos.Y);
            }
        }
    }
}
