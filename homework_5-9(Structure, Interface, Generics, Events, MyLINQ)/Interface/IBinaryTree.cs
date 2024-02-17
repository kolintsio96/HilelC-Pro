namespace Interface
{
    public interface INode<T>
    {
        T Key { get; }

        INode<T> Left { get; set; }

        INode<T> Right { get; set; }

    }
    public interface IBinaryTree<T> : ICollection<T>
    {
        INode<T> Root { get; }

        void Add(T data);
    }

}
