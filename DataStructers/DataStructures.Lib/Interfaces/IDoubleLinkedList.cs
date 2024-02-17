namespace DataStructures.Lib.Interfaces
{
    public interface IDoubleLinkedNode<T> : ILinkedNode<T>
    {
        ILinkedNode<T>? Previous { get; set; }
    }

    public interface IDoubleLinkedList<T> : ILinkedList<T>
    {
        bool Remove(T? item);
    }
}
