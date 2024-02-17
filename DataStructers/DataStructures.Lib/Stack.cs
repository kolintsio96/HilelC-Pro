using DataStructures.Lib.Interfaces;

namespace DataStructures.Lib
{
    public class Stack<T> : IStack<T>
    {
        private readonly LinkedList<T> _linkedList = new LinkedList<T>();

        public int Count => _linkedList.Count;

        public void Push(T? data) => _linkedList.AddFirst(data);

        public T? Pop() => _linkedList.RemoveFirst();

        public T? Peek()
        {
            if (_linkedList.Count == 0)
            {
                throw new InvalidOperationException("Empty stack");
            }

            return _linkedList.First;
        }

        public bool Contains(T? data) => _linkedList.Contains(data);

        public void Clear() => _linkedList.Clear();

        public T?[] ToArray() => _linkedList.ToArray();

        void Interfaces.ICollection<T>.Add(T? item) => Push(item);
    }
}