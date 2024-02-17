using DataStructures.Lib.Interfaces;

namespace DataStructures.Lib
{
    public class DoubleLinkedNode<T> : LinkedNode<T>, IDoubleLinkedNode<T>
    {
        public ILinkedNode<T>? Previous { get; set; }

        public DoubleLinkedNode(T? data) : base(data) { }
    }
}