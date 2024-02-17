namespace Interface
{
    public interface IList<T> : ICollection<T>
    {
        int Capacity { get; }
        
        T this[int index] { get; set; }
        
        void Add(T data);
        
        void Insert(int index, T data);
        
        void Remove(T data);
        
        void RemoveAt(int index);
        
        int IndexOf(T data);
        
        void Reverse();
    }
}
