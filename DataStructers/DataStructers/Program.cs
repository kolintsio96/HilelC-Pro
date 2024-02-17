using DataStructers.Tests;
using DataStructers.Tests.Interfaces;
using DataStructures.Lib;
using System.Reflection;

namespace DataStructers
{
    internal class Program
    {
        static void Main(string[] args)
        {
            RunTests(true);
        }

        static void RunTests(bool withEvents)
        {
            if (withEvents)
            {
                var testGroups = new ITestsGroup[] { new ListTestsWithEvents(), new LinkedListTestWithEvents(), new BinaryTreeTestsWithEvents() };
                var testRenderer = new ConsoleTestRenderer(testGroups);

                testRenderer.Show();
                foreach (var testGroup in testGroups)
                {
                    testGroup.Run();
                }
            }
            else
            {
                ITestsGroup[] tests = { new ListTests(), new LinkedListTests(), new BinaryTreeTests() };
                foreach (var t in tests)
                {
                    t.Run();
                }
            }
        }
    }
}
