namespace Threads
{
    public class SumOfArray : ThreadsForArray<int>
    {
        public long Result { get; set; }
        private long[] ResultArray { get; set; }

        public SumOfArray(int threadCount, int[] enterArray) : base(threadCount, enterArray)
        {
            Result = 0;
            ResultArray = new long[threadCount];
        }

        public override void Process()
        {
            base.Process();
            Result = ResultArray.Sum();
        }

        protected override void Callback(Span<int> span, int index, int threadIndex)
        {
            ResultArray[threadIndex] += span[index];
        }
    }
}
