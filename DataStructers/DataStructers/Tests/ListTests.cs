namespace DataStructers.Tests
{
    class ListTests : Tests
    {
        private class PersonTest
        {
            public int Id { get; set; }
            public string? Name { get; set; }

            public override bool Equals(object? obj)
            {
                if (obj == null) return false;
                if (obj == this) return true;

                if (obj is not PersonTest person) return false;

                return person.Id == Id && string.Equals(Name, person.Name);
            }
        }

        public override string Title => "List tests run:";

        protected override void RunTests()
        {
            AddToListTest();
            AddNullToListTest();
            InsertToListTest();
            InsertNullToListTest();
            RemoveFromListTest();
            RemoveAtFromListTest();
            RemoveNullFromListTest();

            ContainsIntsInListTest();
            ContainsObjectsInListTest();
            IndexOfIntsInListTest();
            IndexOfObjectsInListTest();

            NotContainsIntsInListTest();
            NotContainsObjectsInListTest();
            NotIndexOfIntsInListTest();
            NotIndexOfObjectsInListTest();

            ClearListTest();
            GetArrayFromListTest();

            FindNullInListTest();
            GetOutOfIndexTest();

            ReverseListTest();
        }

        private void IndexOfIntsInListTest()
        {
            var list = new DataStructures.Lib.List<int>(2);
            list.Add(1);
            list.Add(2);
            list.Add(3);

            bool isSuccess = list.IndexOf(1) == 0 && list.IndexOf(2) == 1 && list.IndexOf(3) == 2;
            ShowTestResult("IndexOf ints in List", isSuccess);
        }

        private void NotIndexOfIntsInListTest()
        {
            var list = new DataStructures.Lib.List<int>(2);
            list.Add(1);
            list.Add(2);
            list.Add(3);

            bool isSuccess = list.IndexOf(5) < 0;
            ShowTestResult("Not IndexOf ints in List", isSuccess);
        }

        private void ContainsIntsInListTest()
        {
            var list = new DataStructures.Lib.List<int>(2);
            list.Add(1);
            list.Add(2);
            list.Add(3);

            bool isSuccess = list.Contains(1) && list.Contains(2) && list.Contains(3);
            ShowTestResult("Contains ints in List", isSuccess);
        }

        private void NotContainsIntsInListTest()
        {
            var list = new DataStructures.Lib.List<int>(2);
            list.Add(1);
            list.Add(2);
            list.Add(3);

            bool isSuccess = !list.Contains(5);
            ShowTestResult("Not Contains ints in List", isSuccess);
        }

        private void ContainsObjectsInListTest()
        {
            var list = new DataStructures.Lib.List<PersonTest>(2);
            list.Add(new PersonTest { Id = 1, Name = "Sam" });
            list.Add(new PersonTest { Id = 2, Name = "John" });
            list.Add(new PersonTest { Id = 3, Name = "Bill" });

            bool isSuccess = list.Contains(new PersonTest { Id = 1, Name = "Sam" });
            ShowTestResult("Contains objects in List", isSuccess);
        }

        private void NotContainsObjectsInListTest()
        {
            var list = new DataStructures.Lib.List<PersonTest>(2);
            list.Add(new PersonTest { Id = 1, Name = "Sam" });
            list.Add(new PersonTest { Id = 2, Name = "John" });
            list.Add(new PersonTest { Id = 3, Name = "Bill" });

            bool isSuccess = !list.Contains(new PersonTest { Id = 4, Name = "Sam" });
            ShowTestResult("Not Contains objects in List", isSuccess);
        }

        private void IndexOfObjectsInListTest()
        {
            var list = new DataStructures.Lib.List<PersonTest>(2);
            list.Add(new PersonTest { Id = 1, Name = "Sam" });
            list.Add(new PersonTest { Id = 2, Name = "John" });
            list.Add(new PersonTest { Id = 3, Name = "Bill" });

            bool isSuccess = list.IndexOf(new PersonTest { Id = 1, Name = "Sam" }) == 0
                && list.IndexOf(new PersonTest { Id = 2, Name = "John" }) == 1
                && list.IndexOf(new PersonTest { Id = 3, Name = "Bill" }) == 2;
            ShowTestResult("IndexOf objects in List", isSuccess);
        }

        private void NotIndexOfObjectsInListTest()
        {
            var list = new DataStructures.Lib.List<PersonTest>(2);
            list.Add(new PersonTest { Id = 1, Name = "Sam" });
            list.Add(new PersonTest { Id = 2, Name = "John" });
            list.Add(new PersonTest { Id = 3, Name = "Bill" });

            bool isSuccess = list.IndexOf(new PersonTest { Id = 1, Name = "Bill" }) < 0;
            ShowTestResult("IndexOf objects in List", isSuccess);
        }

        private void RemoveFromListTest()
        {
            var list = new DataStructures.Lib.List<int>(2);
            list.Add(1);
            list.Add(2);
            list.Add(3);

            bool isSuccess = true;

            list.Remove(2);
            isSuccess &= list[0] == 1 && list[1] == 3 && list.Count == 2 && list.Capacity == 4;

            list.Remove(1);
            isSuccess &= list[0] == 3 && list.Count == 1 && list.Capacity == 4;

            list.Remove(3);
            isSuccess &= list.Count == 0 && list.Capacity == 4;

            ShowTestResult("Remove from List", isSuccess);
        }

        private void RemoveAtFromListTest()
        {
            var list = new DataStructures.Lib.List<int>(2);
            list.Add(1);
            list.Add(2);
            list.Add(3);

            bool isSuccess = true;

            list.RemoveAt(1);
            isSuccess &= list[0] == 1 && list[1] == 3 && list.Count == 2 && list.Capacity == 4;

            list.RemoveAt(0);
            isSuccess &= list[0] == 3 && list.Count == 1 && list.Capacity == 4;

            list.RemoveAt(0);
            isSuccess &= list.Count == 0 && list.Capacity == 4;

            ShowTestResult("Remove from List", isSuccess);
        }

        private void AddToListTest()
        {
            var list = new DataStructures.Lib.List<int>(2);
            list.Add(1);
            list.Add(2);
            list.Add(3);

            bool isSuccess =
                    list.Count == 3
                && list.Capacity == 4
                && list[0] == 1
                && list[1] == 2
                && list[2] == 3;

            ShowTestResult("Add to List", isSuccess);
        }

        private void InsertToListTest()
        {
            var list = new DataStructures.Lib.List<int>(2);
            list.Add(1);
            list.Add(2);
            list.Add(3);

            bool isSuccess = false;

            list.Insert(0, 10);
            isSuccess = list.Count == 4 && list[0] == 10;

            list.Insert(list.Count - 1, 20);
            isSuccess &= list.Count == 5 && list[^1] == 3 && list[^2] == 20;

            list.Insert(2, 30);
            isSuccess &= list.Count == 6 && list[2] == 30;

            ShowTestResult("Insert to List", isSuccess);
        }

        private void GetOutOfIndexTest()
        {
            var list = new DataStructures.Lib.List<int>(2);
            list.Add(1);
            list.Add(2);
            list.Add(3);

            bool isSuccess = false;
            try
            {
                var item = list[4];
            }
            catch
            {
                isSuccess = true;
            }
            ShowTestResult("Index out of range", isSuccess);
        }

        private void AddNullToListTest()
        {
            var list = new DataStructures.Lib.List<object>(2);
            list.Add(null);
            list.Add(null);

            bool isSuccess = list.Contains(null) && list.IndexOf(null) == 0
                            && list[0] == null && list[1] == null;

            ShowTestResult("Add null to List", isSuccess);
        }

        private void InsertNullToListTest()
        {
            var list = new DataStructures.Lib.List<object>(2);
            list.Add(new object());
            list.Add(new object());
            list.Add(new object());

            list.Insert(0, null);
            bool isSuccess = list.Count == 4 && list[0] == null;

            list.Insert(list.Count - 1, null);
            isSuccess &= list.Count == 5 && list[^1] != null && list[^2] == null;

            list.Insert(2, null);
            isSuccess &= list.Count == 6 && list[2] == null;

            ShowTestResult("Insert null to List", isSuccess);
        }

        private void RemoveNullFromListTest()
        {
            var list = new DataStructures.Lib.List<object>(2);
            list.Add(new object());
            list.Add(null);
            list.Add(new object());
            list.Add(null);

            list.Remove(null);

            ShowTestResult("Remove null from List", list.Count == 3 && list[0] != null && list[1] != null && list[2] == null);
        }

        private void FindNullInListTest()
        {
            var list = new DataStructures.Lib.List<object>(2);
            list.Add(new object());
            list.Add(null);
            list.Add(new object());
            list.Add(null);

            ShowTestResult("Find null in List", list.Contains(null) && list.IndexOf(null) == 1);
        }

        private void GetArrayFromListTest()
        {
            var list = new DataStructures.Lib.List<int>(2);
            list.Add(1);
            list.Add(2);
            list.Add(3);
            list.Add(4);

            var array = list.ToArray();
            bool isSuccess = array.Length == list.Count;

            array[0] = 100;
            isSuccess &= array[0] != list[0];

            ShowTestResult("Get Array form List", isSuccess);
        }

        private void ClearListTest()
        {
            var list = new DataStructures.Lib.List<object>(2);
            list.Add(new object());

            list.Clear();

            ShowTestResult("Clear List", list.Count == 0 && list.Capacity == 2);
        }

        private void ReverseListTest()
        {
            var list = new DataStructures.Lib.List<int>(2);
            list.Add(1);
            list.Add(2);
            list.Add(3);
            list.Add(4);

            list.Reverse();

            ShowTestResult("Reverse List", list.Count == 4 && list[0] == 4 && list[1] == 3 && list[2] == 2 && list[3] == 1);
        }
    }
}
