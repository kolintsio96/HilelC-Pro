namespace Threads
{
    public class AvarageOfArray : SumOfArray
    {
        public long Average { get { return Result / result.Length; } }

        public AvarageOfArray(int threadCount, int[] enterArray) : base(threadCount, enterArray) {}
    }
}
