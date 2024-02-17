using DataStructures.Lib.Interfaces;
using System.Collections;

namespace DataStructures.Lib
{
    public class LinkedList<T> : ILinkedList<T>, IEnumerable<T>
    {
        protected ILinkedNode<T>? _first;
        protected ILinkedNode<T>? _last;

        public T? First => _first == null ? default : _first.Data;
        public T? Last => _last == null ? default : _last.Data;

        public int Count { get; protected set; }

        protected virtual TNode CreateNode<TNode>(T? value, ILinkedNode<T>? next = null, ILinkedNode<T>? prev = null) where TNode : LinkedNode<T>
        {
            var node = new LinkedNode<T>(value) { Next = next };
            return (TNode)node;
        }

        protected virtual void UpdateNode(ILinkedNode<T>? node, ILinkedNode<T>? next = null, ILinkedNode<T>? prev = null)
        {
            if (node != null)
                node.Next = next;
        }

        public void Add(T? data)
        {
            ILinkedNode<T> node;
            if (_first == null)
            {
                _first = node = CreateNode<LinkedNode<T>>(data);
            }
            else
            {
                _last!.Next = node = CreateNode<LinkedNode<T>>(data, prev: _last);
            }

            _last = node;

            Count++;
        }

        public void AddFirst(T? data)
        {
            ILinkedNode<T> node = CreateNode<LinkedNode<T>>(data, next: _first);
            _first = node;

            if (Count == 0)
            {
                _last = _first;
            }
            else
            {
                UpdateNode(node.Next!, prev: node, next: node.Next?.Next);
            }

            Count++;
        }

        public T? RemoveFirst()
        {
            if (Count == 0)
            {
                throw new InvalidOperationException();
            }

            T? result = _first.Data;
            _first = _first?.Next;
            UpdateNode(_first!, next: _first?.Next, prev: null);
            Count--;

            if (Count == 0) _last = null;

            return result;
        }

        public virtual T? RemoveLast()
        {
            if (Count <= 0)
            {
                throw new InvalidOperationException("The list is empty.");
            }

            T? result = _last.Data;
            if (_last == _first)
            {
                _first = _last = null;
            }
            else
            {
                var prevLast = _first;
                while (prevLast != null && prevLast.Next != _last) prevLast = prevLast.Next;
                _last = prevLast;
                _last!.Next = null;
            }

            Count--;
            return result;
        }

        public void Insert(int index, T? data)
        {
            if (index < 0 || index > Count)
            {
                throw new ArgumentOutOfRangeException();
            }

            if (index == 0)
            {
                AddFirst(data);
                return;
            }

            ILinkedNode<T>? current = _first;

            for (int i = 0; i < index - 1 && current != null; i++)
            {
                current = current.Next;
            }

            if (current == null)
            {
                throw new ArgumentOutOfRangeException();
            }

            LinkedNode<T> newNode = CreateNode<LinkedNode<T>>(data, next: current.Next, prev: current);
            UpdateNode(current.Next!, prev: newNode);
            UpdateNode(current, next: newNode);
            Count++;
        }

        public bool Contains(T? data) => FindNodeByData(data) != null;

        protected ILinkedNode<T>? FindNodeByData(T? data)
        {
            var current = _first;

            while (current != null)
            {
                if (Equals(current.Data, data)) break;
                current = current.Next;
            }

            return current;
        }

        public void Clear()
        {
            Count = 0;
            _first = null;
            _last = null;
        }

        public T?[] ToArray()
        {
            T?[] result = new T?[Count];
            int index = 0;
            ILinkedNode<T>? current = _first;
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
            return new LinkedListIterator<T>(_first);
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

            public void Dispose()
            {
            }
        }
    }
}