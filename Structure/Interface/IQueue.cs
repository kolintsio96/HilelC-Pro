namespace Interface
{
    public interface IQueue<T> : ICollection<T>
    {
        void Enqueue(T data);

        T Dequeue();

        T Peek();
    }
}
