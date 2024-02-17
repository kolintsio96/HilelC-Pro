namespace Interface
{
    public interface IDoubleLinkedNode<T> : ILinkedNode<T>
    {
        IDoubleLinkedNode<T> Previous { get; set; }
    }

    public interface IDoubleLinkedList<T> : ILinkedList<T>, ICollection<T>
    {
        bool Remove(T data);

        void RemoveLast();
    }
}
