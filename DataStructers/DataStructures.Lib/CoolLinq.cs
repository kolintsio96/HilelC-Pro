using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructures.Lib
{
    public static class CollectionHelpers
    {
        private class FileterIterable<T> : IEnumerable<T>
        {
            private readonly IEnumerable<T> collection;
            private readonly Predicate<T> predicate;

            public FileterIterable(IEnumerable<T> collection, Predicate<T> predicate)
            {
                this.collection = collection;
                this.predicate = predicate;
            }

            public IEnumerator<T> GetEnumerator()
            {
                return new FilterIterator<T>(collection, predicate);
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }
        }

        private class FilterIterator<T> : IEnumerator<T>
        {
            private readonly IEnumerable<T> _collection;
            private readonly Predicate<T> _predicate;

            private IEnumerator<T> _iterator;

            public T? Current => _iterator.Current;

            object IEnumerator.Current => Current;

            public FilterIterator(IEnumerable<T> collection, Predicate<T> predicate)
            {
                this._collection = collection;
                this._predicate = predicate;
            }

            public bool MoveNext()
            {
                if (_iterator == null) _iterator = _collection.GetEnumerator();

                bool result = false;
                do
                {
                    result = _iterator.MoveNext();
                    if (result && _predicate(_iterator.Current))
                    {
                        return true;
                    }
                } while (result);

                return false;
            }

            public void Reset()
            {
            }

            public void Dispose()
            {
            }
        }

        //public static IEnumerable<T> Filter<T>(this IEnumerable<T> list, Func<T, bool> func)
        //{
        //    return new FileterIterable<T>(list, item => func(item));
        //}

        //public static IEnumerable<T> Take<T>(this IEnumerable<T> iterable, int count)
        //{
        //    int current = 0;
        //    foreach (var item in iterable)
        //    {

        //        if (current >= count) yield break;

        //        yield return item;
        //        current++;
        //    }
        //}

        //public static IEnumerable<T> Take<T>(this IEnumerable<T> iterable, Func<T, bool> takeWhen)
        //{
        //    int current = 0;
        //    foreach (var item in iterable)
        //    {
        //        if (!takeWhen(item)) yield break;

        //        yield return item;
        //        current++;
        //    }
        //}

        //public static IEnumerable<T> Skip<T>(this IEnumerable<T> iterable, int count)
        //{
        //    int current = 0;
        //    foreach (var item in iterable)
        //    {
        //        if (current >= count)
        //        {
        //            yield return item;
        //        }
        //        else
        //            current++;
        //    }
        //}
    }
}
