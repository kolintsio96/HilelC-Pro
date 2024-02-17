namespace Interface
{
    public interface IStack<T> : ICollection<T>
    {
        void Push(T data);

        T Pop();

        T Peek();
    }
}
