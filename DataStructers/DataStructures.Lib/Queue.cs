using DataStructures.Lib.Interfaces;

namespace DataStructures.Lib
{
    public class Queue<T> : IQueue<T>
    {
        private readonly LinkedList<T> _linkedList = new LinkedList<T>();

        public int Count => _linkedList.Count;

        public void Enqueue(T? data) => _linkedList.Add(data);

        public T? Dequeue() => _linkedList.RemoveFirst();

        public T? Peek()
        {
            if (_linkedList.Count == 0)
            {
                throw new InvalidOperationException();
            }

            return _linkedList.First;
        }

        public bool Contains(T? data) => _linkedList.Contains(data);

        public void Clear() => _linkedList.Clear();

        public T?[] ToArray() => _linkedList.ToArray();

        void Interfaces.ICollection<T>.Add(T? item) => Enqueue(item);
    }
}