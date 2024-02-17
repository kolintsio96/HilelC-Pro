namespace Threads
{
    public class MinOfArray : ThreadsForArray<int>
    {
        public int Result { get; set; }

        private int[] ResultArray { get; set; }

        public MinOfArray(int threadCount, int[] enterArray) : base(threadCount, enterArray)
        {
            Result = enterArray[0];
            ResultArray = new int[threadCount];
        }

        public override void Process()
        {
            base.Process();
            Result = ResultArray.Min();
        }

        protected override void Callback(Span<int> span, int index, int threadIndex)
        {
            if (ResultArray[threadIndex] > span[index]) ResultArray[threadIndex] = span[index];
        }
    }
}
