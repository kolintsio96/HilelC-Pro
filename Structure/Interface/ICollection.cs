namespace Interface
{
    public interface ICollection<T>
    {
        int Count { get; }

        void Clear();

        T[] ToArray();

        bool Contains(T data);

    }
}
