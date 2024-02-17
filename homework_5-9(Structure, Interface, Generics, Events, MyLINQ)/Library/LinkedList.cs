using Interface;
using System.Collections;
namespace Library
{
    public class LinkedNode<T> : ILinkedNode<T>
    {
        public LinkedNode(T data)
        {
            Data = data;
        }
        public T Data { get; }
        public ILinkedNode<T>? Next { get; set; }

        public override string ToString()
        {
            return Data == null ? string.Empty : Data.ToString();
        }
    }
    public class LinkedList<T> : ILinkedList<T>, IEnumerable<T>
    {
        public ILinkedNode<T>? First { get; protected set; }
        public ILinkedNode<T>? Last { get; protected set; }
        public int Count { get; protected set; }

        public virtual void Add(T data)
        {
            ILinkedNode<T> node = new LinkedNode<T>(data);

            if (First == null)
            {
                First = node;
            } else
            {
                Last!.Next = node;
            }
               
            Last = node;

            Count++;
        }

        public virtual void AddFirst(T data)
        {
            ILinkedNode<T> node = new LinkedNode<T>(data);
            node.Next = First;
            First = node;
            if (Count == 0)
            {
                Last = First;
            }
                
            Count++;
        }

        public T RemoveFirst()
        {
            if (Count == 0)
            {
                throw new InvalidOperationException();
            }

            T result = First!.Data;
            First = First.Next;
            Count--;
            return result;
        }

        public void Insert(int index, T data)
        {
            if (index < 0 || index > Count)
            {
                throw new ArgumentOutOfRangeException();
            }

            ILinkedNode<T> newNode = new LinkedNode<T>(data);

            if (index == 0)
            {
                AddFirst(data);
                return;
            }

            ILinkedNode<T> current = First;

            for (int i = 0; i < index - 1 && current != null; i++)
            {
                current = current.Next;
            }

            if (current == null)
            {
                throw new ArgumentOutOfRangeException();
            }

            newNode.Next = current.Next;
            current.Next = newNode;
            Count++;
        }
        
        public bool Contains(T data)
        {
            ILinkedNode<T>? current = First;
            while (current != null && current.Data != null)
            {
                if (current.Data.Equals(data)) return true;
                current = current.Next;
            }
            return false;
        }

        public void Clear()
        {
            Count = 0;
            First = null;
            Last = null;
        }

        public T[] ToArray()
        {
            T[] result = new T[Count];
            int index = 0;
            ILinkedNode<T>? current = First;
            while (current != null && current.Data != null)
            {
                result[index] = current.Data;
                current = current.Next;
                index++;
            }
            return result;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return new LinkedListIterator<T>(First);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private class LinkedListIterator<TItem> : IEnumerator<TItem>
        {
            private readonly ILinkedNode<TItem> _startNode;

            private ILinkedNode<TItem>? _currentNode;

            public TItem? Current { get; private set; }

            object IEnumerator.Current => Current;

            public LinkedListIterator(ILinkedNode<TItem> node)
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
