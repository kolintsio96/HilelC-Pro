using DataStructures.Lib.Interfaces;

namespace DataStructures.Lib
{
    public class DoubleLinkedList<T> : LinkedList<T>, IDoubleLinkedList<T>
    {
        protected override TNode CreateNode<TNode>(T? value, ILinkedNode<T>? next = null, ILinkedNode<T>? prev = null)
        {
            var result = new DoubleLinkedNode<T>(value) { Next = next, Previous = prev };
            return (result as TNode)!;
        }

        protected override void UpdateNode(ILinkedNode<T>? node, ILinkedNode<T>? next = null, ILinkedNode<T>? prev = null)
        {
            base.UpdateNode(node, next, prev);
            if (node != null)
            {
                ((DoubleLinkedNode<T>)node).Previous = prev;
            }
        }

        public bool Remove(T? data)
        {
            var current = (DoubleLinkedNode<T>?)FindNodeByData(data);
            if (current == null) return false;

            if (current == _first)
            {
                RemoveFirst();
            }
            else if (current == _last)
            {
                RemoveLast();
            }
            else
            {
                var nextNode = (DoubleLinkedNode<T>?)current.Next;
                nextNode!.Previous = current.Previous;
                current.Previous!.Next = nextNode;
                Count--;
            }

            return true;
        }

        public override T? RemoveLast()
        {
            if (Count <= 0)
            {
                throw new InvalidOperationException("The list is empty.");
            }

            T? data = _last.Data;
            DoubleLinkedNode<T>? last = (DoubleLinkedNode<T>?)_last;
            _last = last!.Previous;
            _last!.Next = null;
            Count--;

            if (Count == 0) _first = null;
            return data;
        }
    }
}