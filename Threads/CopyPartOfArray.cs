namespace Threads
{
    internal class CopyPartOfArray<T> : ThreadsForArray<T>
    {
        public T[] Result { get; set; }

        private List<T>[] ResultArray { get; set; }

        private int EndIndex { get; }

        private int StartIndex { get; }


        public CopyPartOfArray(int threadCount, T[] enterArray, int startIndex, int endIndex) : base(threadCount, enterArray)
        {
            if (startIndex < 0 || endIndex > enterArray.Length - 1)
            {
                throw new ArgumentOutOfRangeException();
            }

            StartIndex = startIndex;
            EndIndex = endIndex;
            ResultArray = new List<T>[threadCount];
        }

        public override void Process()
        {
            base.Process();
            Result = new T[EndIndex - StartIndex + 1];
            int i = 0;
            foreach (var list in ResultArray)
            {
                foreach (var item in list)
                {
                    Result[i] = item;
                    i++;
                }
            }
        }

        protected override void Callback(Span<T> span, int index, int threadIndex)
        {
            var length = threads.Length;
            var count = result.Length / length;
            var itemIndex = (threadIndex * count) + index;

            if (ResultArray[threadIndex] == null)
            {
                ResultArray[threadIndex] = ResultArray[threadIndex] = new List<T>();
            }

            if (itemIndex >= StartIndex && itemIndex <= EndIndex) {
                ResultArray[threadIndex].Add(span[index]);
            }
        }
    }
}