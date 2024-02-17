namespace DataStructers.Tests.Interfaces
{
    interface ITestsGroup
    {
        string Title { get; }

        void Run();

        IEnumerable<string> GetTestList();

        event Action<string, TestState>? TestCompleted;
    }
}
