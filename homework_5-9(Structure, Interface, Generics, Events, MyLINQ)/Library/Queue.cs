using Interface;
using System.Collections;

namespace Library
{
    public class Queue<T> : IQueue<T>, IEnumerable<T>
    {
        private ILinkedList<T> linkedList = new LinkedList<T>();
        public int Count {  get { return linkedList.Count; } }
        public void Enqueue(T data)
        {
            linkedList.Add(data);
        }

        public T Dequeue()
        {
            return linkedList.RemoveFirst();
        }

        public T Peek()
        {
            if (linkedList.Count == 0)
            {
                throw new InvalidOperationException();
            }

            return linkedList.First!.Data;
        }

        public bool Contains(T data)
        {
            return linkedList.Contains(data);
        }

        public void Clear()
        {
            linkedList.Clear();
        }

        public T[] ToArray()
        {
            return linkedList.ToArray();
        }

        public IEnumerator<T> GetEnumerator()
        {
            return new QueueIterator<T>(linkedList.First);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private class QueueIterator<TItem> : IEnumerator<TItem>
        {
            private readonly ILinkedNode<TItem> _startNode;

            private ILinkedNode<TItem>? _currentNode;

            public TItem? Current { get; private set; }

            object IEnumerator.Current => Current;

            public QueueIterator(ILinkedNode<TItem> node)
            {
                this._startNode = _currentNode = node;
            }

            public void Dispose()
            {

            }

            public bool MoveNext()
            {
                if (_currentNode != null)
                {
                    Current = _currentNode.Data;
                    _currentNode = _currentNode.Next;
                    return true;
                }
                return false;
            }

            public void Reset()
            {
                _currentNode = _startNode;
            }
        }
    }
}
