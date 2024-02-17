namespace Interface
{
    public interface ILinkedNode<T>
    {
        T Data { get; }

        ILinkedNode<T> Next { get; set; }
    }
    
    public interface ILinkedList<T> : ICollection<T>
    {
        ILinkedNode<T> First { get; }

        ILinkedNode<T> Last { get; }

        void Add(T data);
        
        void AddFirst(T data);
        
        T RemoveFirst();

        void Insert(int index, T data);
    }
}
