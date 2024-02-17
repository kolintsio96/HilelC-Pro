using DataStructures.Lib.Interfaces;
using System;
using System.Collections;

namespace DataStructures.Lib
{
    public class List<T> : Interfaces.IList<T>, IEnumerable<T>
    {
        private const int defaultCapacity = 4;

        private readonly T?[] emptyArray = Array.Empty<T?>();

        private T?[] _data;

        public int Capacity => _data.Length;

        public int Count { get; private set; } = 0;

        public List()
        {
            _data = new T[defaultCapacity];
        }

        public List(int capacity)
        {
            if (capacity < 0)
            {
                throw new ArgumentOutOfRangeException();
            }
            else if (capacity == 0)
            {
                _data = emptyArray;
            }
            else
            {
                _data = new T[capacity];
            }
        }

        /// <summary>
        /// Get list item by index in the list.
        /// </summary>
        /// <param name="index">Zero based index in the list</param>
        /// <returns>The list item.</returns>
        /// <exception cref="ArgumentOutOfRangeException">throws when index is out of range (< 0 or >= Count)</exception>
        public T? this[int index]
        {
            get
            {
                if (index < Count)
                {
                    return _data[index];
                }
                else
                {
                    throw new ArgumentOutOfRangeException();
                }
            }
            set
            {
                if (index < Count)
                {
                    _data[index] = value;
                }
                else
                {
                    throw new ArgumentOutOfRangeException();
                }
            }
        }

        private void TryGrow()
        {
            if (Count + 1 >= Capacity)
            {
                T?[] array = _data;
                _data = new T[array.Length * 2];
                for (int i = 0; i < Count; i++)
                {
                    _data[i] = array[i];
                }
            }
        }

        public void Add(T? value)
        {
            TryGrow();

            _data[Count] = value;
            Count++;
        }

        /// <summary>
        /// Get list item by index in the list.
        /// </summary>
        /// <param name="index">Zero-based index in the list</param>
        /// <returns>The list item.</returns>
        /// <exception cref="ArgumentOutOfRangeException">throws when index is out of range (< 0 or >= Count)</exception>
        public void Insert(int index, T? value)
        {
            if (index < 0 || index > Count)
            {
                throw new ArgumentOutOfRangeException();
            }

            TryGrow();

            for (int i = Count - 1; i >= index; i--)
            {
                var currentItem = _data[i];
                _data[i + 1] = currentItem;
            }

            _data[index] = value;
            Count++;
        }

        public void Remove(T? value)
        {
            int index = IndexOf(value);
            if (index != -1) RemoveAt(index);
        }

        public void RemoveAt(int index)
        {
            if (index < 0 || index > Count - 1)
            {
                throw new ArgumentOutOfRangeException();
            }

            for (int i = index; i < Count - 1; i++)
            {
                var nextItem = _data[i + 1];
                _data[i] = nextItem;
            }

            _data[--Count] = default;
        }

        public void Clear()
        {
            for (int i = 0; i < Count; i++) _data[i] = default;
            //_data = emptyArray;
            Count = 0;
        }

        public bool Contains(T? value) => IndexOf(value) != -1;

        public int IndexOf(T? value)
        {
            for (int i = 0; i < Count; i++)
            {
                if (Equals(_data[i], value))
                    return i;
            }
            return -1;
        }

        public T?[] ToArray()
        {
            if (Count == 0) return emptyArray;

            T?[] array = new T?[Count];
            for (int i = 0; i < array.Length; i++)
                array[i] = _data[i];

            return array;
        }

        public void Reverse()
        {
            T? first, last;
            for (int i = 0; i < Count / 2; i++)
            {
                first = _data[i];
                last = _data[Count - i - 1];
                _data[i] = last;
                _data[Count - i - 1] = first;
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            return new ListIterator<T>(this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private class ListIterator<TItem> : IEnumerator<TItem>
        {
            public TItem Current { get; private set; }

            object IEnumerator.Current => Current;

            private int currentIndex = 0;
            private readonly List<TItem> list;

            public ListIterator(List<TItem> list)
            {
                this.list = list;
            }

            public void Dispose()
            {
            }

            public bool MoveNext()
            {
                if (currentIndex < list.Count)
                {
                    Current = list[currentIndex];
                    currentIndex++;
                    return true;
                }
                return false;
            }

            public void Reset()
            {
                currentIndex = 0;
            }
        }
    }
}
