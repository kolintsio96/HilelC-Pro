namespace DataStructures.Lib.Interfaces
{
    public interface ILinkedNode<T>
    {
        T? Data { get; }

        ILinkedNode<T>? Next { get; set; }
    }

    public interface ILinkedList<T> : ICollection<T>
    {
        T? First { get; }

        T? Last { get; }

        void AddFirst(T? item);

        void Insert(int index, T? item);

        T? RemoveLast();

        T? RemoveFirst();
    }
}
