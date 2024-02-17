using DataStructers.Tests.Interfaces;

namespace DataStructers
{
    class LinkedListTestWithEvents : ITestsGroup
    {
        public string Title => "Linked List tests";

        public IEnumerable<string> GetTestList()
        {
            yield return nameof(AddToList);
            yield return nameof(AddFirstToList);
            yield return nameof(InsertToList);
            yield return nameof(ContainsInList);
            yield return nameof(ListToArray);
            yield return nameof(ClearList);
        }

        private Func<TestResult>[] GetTests()
        {
            return new Func<TestResult>[]
            {
                AddToList
                , AddFirstToList
                , InsertToList
                , ContainsInList
                , ListToArray
                , ClearList
            };
        }

        public void Run()
        {
            foreach (var test in GetTests())
            {
                var result = test();
                OnTestCompleted(result);
            }
        }

        private void OnTestCompleted(TestResult result)
        {
            TestCompleted?.Invoke(result.Name, result.State);
        }

        private TestResult AddToList()
        {
            var list = new DataStructures.Lib.LinkedList<int>();
            list.Add(1);
            list.Add(2);
            list.Add(3);

            var isSuccess = list.Count == 3 && list.First == 1 && list.Last == 3;
            return new TestResult { Name = nameof(AddToList), State = isSuccess ? TestState.Success : TestState.Failed };
        }

        private TestResult AddFirstToList()
        {
            var list = new DataStructures.Lib.LinkedList<int>();
            list.AddFirst(1);
            list.AddFirst(2);
            list.AddFirst(3);

            var isSuccess = list.Count == 3 && list.First == 3 && list.Last == 1;
            return new TestResult { Name = nameof(AddFirstToList), State = isSuccess ? TestState.Success : TestState.Failed };
        }

        private TestResult InsertToList()
        {
            var list = new DataStructures.Lib.LinkedList<int>();
            list.AddFirst(1);
            list.AddFirst(2);
            list.AddFirst(3);

            list.Insert(1, 99);
            var isSuccess = list.Count == 4;

            list.RemoveFirst();

            isSuccess = isSuccess && list.Count == 3 && list.First == 99 && list.Last == 1;
            return new TestResult { Name = nameof(InsertToList), State = isSuccess ? TestState.Success : TestState.Failed };
        }

        private TestResult ContainsInList()
        {
            var list = new DataStructures.Lib.LinkedList<string>();
            list.AddFirst("a");
            list.AddFirst("b");
            list.AddFirst("c");

            var isSuccess = list.Count == 3 && list.Contains("b") && list.Contains("a") && list.Contains("c") && !list.Contains("z");
            return new TestResult { Name = nameof(ContainsInList), State = isSuccess ? TestState.Success : TestState.Failed };
        }

        private TestResult ListToArray()
        {
            var list = new DataStructures.Lib.LinkedList<string>();
            list.AddFirst("a");
            list.AddFirst("b");
            list.AddFirst("c");

            var array = list.ToArray();

            var isSuccess = array.Length == list.Count && array[0] == list.First && array[array.Length - 1] == list.Last && array[1] == "b";
            return new TestResult { Name = nameof(ListToArray), State = isSuccess ? TestState.Success : TestState.Failed };
        }

        private TestResult ClearList()
        {
            var list = new DataStructures.Lib.LinkedList<int>();
            list.AddFirst(1);
            list.AddFirst(2);
            list.AddFirst(3);

            list.Clear();

            var isSuccess = list.Count == 0 && list.First == default && list.Last == default;
            return new TestResult { Name = nameof(ClearList), State = isSuccess ? TestState.Success : TestState.Failed };
        }

        public event Action<string, TestState>? TestCompleted;
    }
}
