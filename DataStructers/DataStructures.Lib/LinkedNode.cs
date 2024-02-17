using DataStructures.Lib.Interfaces;

namespace DataStructures.Lib
{
    public class LinkedNode<T> : ILinkedNode<T>
    {
        public T? Data { get; }

        public ILinkedNode<T>? Next { get; set; }

        public LinkedNode(T? data)
        {
            Data = data;
        }

        public override string? ToString()
        {
            return Data == null ? string.Empty : Data.ToString();
        }
    }
}