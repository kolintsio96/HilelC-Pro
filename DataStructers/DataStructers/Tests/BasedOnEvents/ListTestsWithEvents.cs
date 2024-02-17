using DataStructers.Tests.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace DataStructers
{
    [System.AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = false)]
    class TestAttribute : Attribute
    {
        public TestAttribute()
        {
        }
    }

    class ListTestsWithEvents : ITestsGroup
    {
        public string Title => "List tests";

        public IEnumerable<string> GetTestList()
        {
            var methods = GetType().GetMethods(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
            return methods.Where(m => m.GetCustomAttribute<TestAttribute>() != null).Select(m => m.Name);
        }

        private Func<TestResult>[] GetTests()
        {
            var methods = GetType().GetMethods(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
            return methods
                .Where(m => m.GetCustomAttribute<TestAttribute>() != null)
                .Select(m => new Func<TestResult>(() => (TestResult)m.Invoke(this, null)))
                .ToArray();
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

        [Test]
        private TestResult IndexOfIntsInListTest()
        {
            var list = new DataStructures.Lib.List<int>(2);
            list.Add(1);
            list.Add(2);
            list.Add(3);

            bool isSuccess = list.IndexOf(1) == 0 && list.IndexOf(2) == 1 && list.IndexOf(3) == 2;
            return new TestResult { Name = nameof(IndexOfIntsInListTest), State = isSuccess ? TestState.Success : TestState.Failed };
        }

        [Test]
        private TestResult NotIndexOfIntsInListTest()
        {
            var list = new DataStructures.Lib.List<int>(2);
            list.Add(1);
            list.Add(2);
            list.Add(3);

            bool isSuccess = list.IndexOf(5) < 0;
            return new TestResult { Name = nameof(NotIndexOfIntsInListTest), State = isSuccess ? TestState.Success : TestState.Failed };
        }

        [Test]
        private TestResult ContainsIntsInListTest()
        {
            var list = new DataStructures.Lib.List<int>(2);
            list.Add(1);
            list.Add(2);
            list.Add(3);

            bool isSuccess = list.Contains(1) && list.Contains(2) && list.Contains(3);
            return new TestResult { Name = nameof(ContainsIntsInListTest), State = isSuccess ? TestState.Success : TestState.Failed };
        }

        [Test]
        private TestResult NotContainsIntsInListTest()
        {
            var list = new DataStructures.Lib.List<int>(2);
            list.Add(1);
            list.Add(2);
            list.Add(3);

            bool isSuccess = !list.Contains(5);
            return new TestResult { Name = nameof(NotContainsIntsInListTest), State = isSuccess ? TestState.Success : TestState.Failed };
        }

        [Test]
        private TestResult ContainsObjectsInListTest()
        {
            var list = new DataStructures.Lib.List<PersonTest>(2);
            list.Add(new PersonTest { Id = 1, Name = "Sam" });
            list.Add(new PersonTest { Id = 2, Name = "John" });
            list.Add(new PersonTest { Id = 3, Name = "Bill" });

            bool isSuccess = list.Contains(new PersonTest { Id = 1, Name = "Sam" });
            return new TestResult { Name = nameof(ContainsObjectsInListTest), State = isSuccess ? TestState.Success : TestState.Failed };
        }

        private TestResult NotContainsObjectsInListTest()
        {
            var list = new DataStructures.Lib.List<PersonTest>(2);
            list.Add(new PersonTest { Id = 1, Name = "Sam" });
            list.Add(new PersonTest { Id = 2, Name = "John" });
            list.Add(new PersonTest { Id = 3, Name = "Bill" });

            bool isSuccess = !list.Contains(new PersonTest { Id = 4, Name = "Sam" });
            return new TestResult { Name = nameof(NotContainsObjectsInListTest), State = isSuccess ? TestState.Success : TestState.Failed };
        }

        private TestResult IndexOfObjectsInListTest()
        {
            var list = new DataStructures.Lib.List<PersonTest>(2);
            list.Add(new PersonTest { Id = 1, Name = "Sam" });
            list.Add(new PersonTest { Id = 2, Name = "John" });
            list.Add(new PersonTest { Id = 3, Name = "Bill" });

            bool isSuccess = list.IndexOf(new PersonTest { Id = 1, Name = "Sam" }) == 0
                && list.IndexOf(new PersonTest { Id = 2, Name = "John" }) == 1
                && list.IndexOf(new PersonTest { Id = 3, Name = "Bill" }) == 2;
            return new TestResult { Name = nameof(IndexOfObjectsInListTest), State = isSuccess ? TestState.Success : TestState.Failed };
        }

        private TestResult NotIndexOfObjectsInListTest()
        {
            var list = new DataStructures.Lib.List<PersonTest>(2);
            list.Add(new PersonTest { Id = 1, Name = "Sam" });
            list.Add(new PersonTest { Id = 2, Name = "John" });
            list.Add(new PersonTest { Id = 3, Name = "Bill" });

            bool isSuccess = list.IndexOf(new PersonTest { Id = 1, Name = "Bill" }) < 0;
            return new TestResult { Name = nameof(NotIndexOfObjectsInListTest), State = isSuccess ? TestState.Success : TestState.Failed };
        }

        private TestResult RemoveFromListTest()
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

            return new TestResult { Name = nameof(RemoveFromListTest), State = isSuccess ? TestState.Success : TestState.Failed };
        }

        private TestResult RemoveAtFromListTest()
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

            return new TestResult { Name = nameof(RemoveAtFromListTest), State = isSuccess ? TestState.Success : TestState.Failed };
        }

        private TestResult AddToListTest()
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

            return new TestResult { Name = nameof(AddToListTest), State = isSuccess ? TestState.Success : TestState.Failed };
        }

        private TestResult InsertToListTest()
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

            return new TestResult { Name = nameof(InsertToListTest), State = isSuccess ? TestState.Success : TestState.Failed };
        }

        private TestResult GetOutOfIndexTest()
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
            return new TestResult { Name = nameof(GetOutOfIndexTest), State = isSuccess ? TestState.Success : TestState.Failed };
        }

        private TestResult AddNullToListTest()
        {
            var list = new DataStructures.Lib.List<object>(2);
            list.Add(null);
            list.Add(null);

            bool isSuccess = list.Contains(null) && list.IndexOf(null) == 0
                            && list[0] == null && list[1] == null;

            return new TestResult { Name = nameof(AddNullToListTest), State = isSuccess ? TestState.Success : TestState.Failed };
        }

        private TestResult InsertNullToListTest()
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

            return new TestResult { Name = nameof(InsertNullToListTest), State = isSuccess ? TestState.Success : TestState.Failed };
        }

        private TestResult RemoveNullFromListTest()
        {
            var list = new DataStructures.Lib.List<object>(2);
            list.Add(new object());
            list.Add(null);
            list.Add(new object());
            list.Add(null);

            list.Remove(null);

            var isSuccess = list.Count == 3 && list[0] != null && list[1] != null && list[2] == null;
            return new TestResult { Name = nameof(RemoveNullFromListTest), State = isSuccess ? TestState.Success : TestState.Failed };
        }

        private TestResult FindNullInListTest()
        {
            var list = new DataStructures.Lib.List<object>(2);
            list.Add(new object());
            list.Add(null);
            list.Add(new object());
            list.Add(null);

            var isSuccess = list.Contains(null) && list.IndexOf(null) == 1;
            return new TestResult { Name = nameof(FindNullInListTest), State = isSuccess ? TestState.Success : TestState.Failed };
        }

        private TestResult GetArrayFromListTest()
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

            return new TestResult { Name = nameof(GetArrayFromListTest), State = isSuccess ? TestState.Success : TestState.Failed };
        }

        private TestResult ClearListTest()
        {
            var list = new DataStructures.Lib.List<object>(2);
            list.Add(new object());

            list.Clear();

            var isSuccess = list.Count == 0 && list.Capacity == 2;
            return new TestResult { Name = nameof(ClearListTest), State = isSuccess ? TestState.Success : TestState.Failed };
        }

        private TestResult ReverseListTest()
        {
            var list = new DataStructures.Lib.List<int>(2);
            list.Add(1);
            list.Add(2);
            list.Add(3);
            list.Add(4);

            list.Reverse();

            var isSuccess = list.Count == 4 && list[0] == 4 && list[1] == 3 && list[2] == 2 && list[3] == 1;
            return new TestResult { Name = nameof(ReverseListTest), State = isSuccess ? TestState.Success : TestState.Failed };
        }

        public event Action<string, TestState>? TestCompleted;

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
    }
}
