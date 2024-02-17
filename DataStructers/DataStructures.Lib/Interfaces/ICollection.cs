namespace DataStructures.Lib.Interfaces
{
    public interface ICollection<T>
    {
        int Count { get; }

        void Clear();

        bool Contains(T? item);

        void Add(T? item);

        T?[] ToArray();
    }
}
