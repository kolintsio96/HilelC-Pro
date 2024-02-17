using System;

namespace Threads
{
    public abstract class ThreadsForArray<T> : IThreadsForArray
    {
        protected readonly Thread[] threads;
        public readonly T[] result;

        public ThreadsForArray(int threadCount, T[] resultArray)
        {
            threads = new Thread[threadCount];
            result = resultArray;
        }

        public virtual void Process()
        {
            for (int i = 0; i < threads.Length; i++)
            {
                threads[i] = new Thread(ThreadProc) { IsBackground = true };
                threads[i].Start(i);
            }

            foreach (var thread in threads) thread.Join();
        }

        protected virtual void ThreadProc(object? state)
        {

            var index = (int)state;
            var length = threads.Length;
            var count = result.Length / length;

            var span = index == length - 1
                ? result.AsSpan((index * count)..)
                : result.AsSpan((index * count)..((index * count) + count));

            for (var i = 0; i < span.Length; i++)
            {
                Callback(span, i, index);
            }
        }

        protected abstract void Callback(Span<T> span, int index, int threadIndex);
    }
}
