using Interface;
namespace Library
{
    public class DoubleLinkedNode<T> : LinkedNode<T>, IDoubleLinkedNode<T>   {
        public DoubleLinkedNode(T data) : base(data) { }
        public IDoubleLinkedNode<T>? Previous { get; set; }
    }
    public class DoubleLinkedList<T> : LinkedList<T>, IDoubleLinkedList<T>
    {        
        public override void Add(T data)
        {
            IDoubleLinkedNode<T> node = new DoubleLinkedNode<T>(data);
           
            if (First == null)
                First = node;
            else
            {
                Last!.Next = node;
                node.Previous = (DoubleLinkedNode<T>)Last;
            }
            Last = node;
            Count++;
        }
        
        public override void AddFirst(T data)
        {
            IDoubleLinkedNode<T> node = new DoubleLinkedNode<T>(data);
            IDoubleLinkedNode<T>? temp = (IDoubleLinkedNode<T>?)First;
            node.Next = temp;
            First = node;
            if (Count == 0)
            {
                Last = First;
            } else {
                temp!.Previous = node;
            }
                
            Count++;
        }

        public bool Remove(T data)
        {
            IDoubleLinkedNode<T>? current = (IDoubleLinkedNode<T>?)First;

            while (current != null)
            {
                if (current.Data.Equals(data))
                {
                    break;
                }
                current = (IDoubleLinkedNode<T>)current.Next;
            }
            if (current != null)
            {
                if (current.Next != null)
                {
                    IDoubleLinkedNode<T> next = (IDoubleLinkedNode<T>)current.Next;
                    next.Previous = current.Previous;
                }
                else
                {
                    Last = current.Previous;
                }

                if (current.Previous != null)
                {
                    current.Previous.Next = current.Next;
                }
                else
                {
                    First = current.Next;
                }
                Count--;
                return true;
            }
            return false;
        }

        private void RemoveElement(bool removeLast = false)
        {
            if (First == null)
            {
                throw new InvalidOperationException("The list is empty.");
            }

            if (First == Last)
            {
                First = null;
                Last = null;
            }
            else
            {
                if (removeLast)
                {
                    IDoubleLinkedNode<T> last = (IDoubleLinkedNode<T>)Last;
                    Last = last!.Previous;
                    Last!.Next = null;
                } else
                {
                    IDoubleLinkedNode<T> first = (IDoubleLinkedNode<T>)First;
                    First = first.Next;
                    first!.Previous = null;
                } 
            }
        }

        public void RemoveFirst()
        {
            RemoveElement();
        }

        public void RemoveLast()
        {
            RemoveElement(true);
        }
    }
}
