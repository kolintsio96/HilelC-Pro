namespace DataStructers.Tests
{
    class LinkedListTests : Tests
    {
        public override string Title => "LinkedList tests run:";

        protected override void RunTests()
        {
            AddToList();
            AddFirstToList();
            InsertToList();
            ClearList();
            ContainsInList();
            ListToArray();
        }

        private void AddToList()
        {
            var list = new DataStructures.Lib.LinkedList<int>();
            list.Add(1);
            list.Add(2);
            list.Add(3);

            ShowTestResult("Add to list", list.Count == 3 && list.First == 1 && list.Last == 3);
        }

        private void AddFirstToList()
        {
            var list = new DataStructures.Lib.LinkedList<int>();
            list.AddFirst(1);
            list.AddFirst(2);
            list.AddFirst(3);

            ShowTestResult("Add first to list", list.Count == 3 && list.First == 3 && list.Last == 1);
        }

        private void InsertToList()
        {
            var list = new DataStructures.Lib.LinkedList<int>();
            list.AddFirst(1);
            list.AddFirst(2);
            list.AddFirst(3);

            list.Insert(1, 99);
            var isSuccess = list.Count == 4;

            list.RemoveFirst();

            ShowTestResult("Insert to list", isSuccess && list.Count == 3 && list.First == 99 && list.Last == 1);
        }

        private void ContainsInList()
        {
            var list = new DataStructures.Lib.LinkedList<string>();
            list.AddFirst("a");
            list.AddFirst("b");
            list.AddFirst("c");

            ShowTestResult("Contains in list", list.Count == 3 && list.Contains("b") && list.Contains("a") && list.Contains("c") && !list.Contains("z"));
        }

        private void ListToArray()
        {
            var list = new DataStructures.Lib.LinkedList<string>();
            list.AddFirst("a");
            list.AddFirst("b");
            list.AddFirst("c");

            var array = list.ToArray();

            ShowTestResult("List to array", array.Length == list.Count && array[0] == list.First && array[array.Length - 1] == list.Last && array[1] == "b");
        }

        private void ClearList()
        {
            var list = new DataStructures.Lib.LinkedList<int>();
            list.AddFirst(1);
            list.AddFirst(2);
            list.AddFirst(3);

            list.Clear();

            ShowTestResult("Clear list", list.Count == 0 && list.First == default && list.Last == default);
        }
    }
}
