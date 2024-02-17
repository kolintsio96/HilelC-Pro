namespace DataStructures.Lib.Interfaces
{
    public interface IList<T> : ICollection<T>
    {
        T? this[int index] { get; }

        int Capacity { get; }

        void Insert(int index, T? item);

        void Remove(T? item);

        void RemoveAt(int index);

        int IndexOf(T? item);

        void Reverse();
    }
}
