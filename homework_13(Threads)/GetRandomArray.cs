namespace Threads
{
    public class GenRandomArray<T> : ThreadsForArray<T>
    {
        private readonly Func<Random, T> randomize;
        private Random[] randoms;

        public GenRandomArray(int threadCount, T[] resultArray, Func<Random, T> rand) : base(threadCount, resultArray)
        {
            randomize = rand;
            randoms = new Random[threadCount];
        }

        public override void Process()
        {
            for (int i = 0; i < threads.Length; i++)
            {
                randoms[i] = new Random();                
            }
            base.Process();
        }

        protected override void Callback(Span<T> span, int index, int threadIndex)
        {
            span[index] = randomize(randoms[threadIndex]);
        }
    }

    public class GenRandomArray : GenRandomArray<int>
    {
        public GenRandomArray(int threadCount, int[] resultArray)
            : base(threadCount, resultArray, r => r.Next())
        {
        }
    }
}
