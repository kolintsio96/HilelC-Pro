using Library;
namespace Example
{
    class Program
    {
        private static void Main(string[] args)
        {
            #region List
            Library.List<int> list = new Library.List<int>();
            list.Add(1);
            list.Insert(0, 2);
            list.Add(3);           
            list.Remove(1);           
            list.RemoveAt(0);           
            list.Contains(3);           
            list.IndexOf(3);
            list.ToArray();
            list.Reverse();
            #endregion

            #region BinaryTree
            BinaryTree<int> binaryTree = new BinaryTree<int>();
            binaryTree.Add(100);
            binaryTree.Add(58);
            binaryTree.Add(2);
            binaryTree.Add(12);
            binaryTree.Add(33);
            binaryTree.Add(0);

            binaryTree.Contains(12);
            binaryTree.Contains(14);
            binaryTree.ToArray(); 

            Console.WriteLine(binaryTree.Count);
            Console.WriteLine(binaryTree.Root);
            #endregion

            #region LinkedList
            Library.LinkedList<int> linkedList = new Library.LinkedList<int>();
            linkedList.Add(100);
            linkedList.Add(21);
            linkedList.AddFirst(11);
            linkedList.Insert(2, 32);
            linkedList.Contains(21);
            linkedList.Contains(210);
            linkedList.ToArray();

            Console.WriteLine(linkedList.First);
            Console.WriteLine(linkedList.Last);
            Console.WriteLine(linkedList.Count);
            #endregion

            #region DoubleLinkedList
            DoubleLinkedList<int> doubleLinkedList = new DoubleLinkedList<int>();
            doubleLinkedList.Add(100);
            doubleLinkedList.Add(21);
            doubleLinkedList.Add(1);
            doubleLinkedList.AddFirst(11);
            doubleLinkedList.Remove(21);
            doubleLinkedList.RemoveFirst();
            doubleLinkedList.RemoveLast();
            doubleLinkedList.Contains(21);
            doubleLinkedList.Contains(210);
            doubleLinkedList.ToArray();

            Console.WriteLine(linkedList.First);
            Console.WriteLine(linkedList.Last);
            Console.WriteLine(linkedList.Count);
            #endregion

            #region Queue
            Library.Queue<int> queue = new Library.Queue<int>();
            queue.Enqueue(100);
            queue.Enqueue(145);
            queue.Enqueue(12);
            queue.Dequeue();
            queue.Contains(12);
            queue.Contains(2);
            queue.ToArray();

            Console.WriteLine(queue.Peek());
            Console.WriteLine(queue.Count);
            #endregion

            #region Stack
            Library.Stack<int> stack = new Library.Stack<int>();
            stack.Push(32);
            stack.Push(12);
            stack.Push(43);
            stack.Pop();
            stack.Contains(12);
            stack.Contains(2);
            stack.ToArray();

            Console.WriteLine(stack.Peek());
            Console.WriteLine(stack.Count);
            #endregion

            #region EventList
            Logger<int> logger = new Logger<int>();
            EventList<int> eventList = new EventList<int>();

            eventList.AddEvent += logger.Log;
            eventList.InsertEvent += logger.Log;
            eventList.RemoveEvent += logger.Log;

            eventList.Add(1);
            eventList.Add(2);
            eventList.Add(3);
            eventList.Insert(0, 4);
            eventList.Remove(2);

            eventList.AddEvent -= logger.Log;
            eventList.InsertEvent -= logger.Log;
            eventList.RemoveEvent -= logger.Log;

            eventList.Add(5);
            eventList.Add(6);
            eventList.Insert(0, 7);
            eventList.Remove(5);
            #endregion
            Console.ReadLine();
        }
    }

    class Logger<T>
    {
        public void Log(object? _, ListEventArgs<T> eventArgs)
        {
            if (eventArgs.Index != null && eventArgs.Data != null)
            {
                Console.WriteLine($"Called event: {eventArgs.EventName}, Index: {eventArgs.Index}, Data: {eventArgs.Data}");
            }
            else if (eventArgs.Index != null)
            {
                Console.WriteLine($"Called event: {eventArgs.EventName}, Index: {eventArgs.Index}");
            }
            else if (eventArgs.Data != null)
            {
                Console.WriteLine($"Called event: {eventArgs.EventName}, Data: {eventArgs.Data}");
            } else
            {
                Console.WriteLine($"Called event: {eventArgs.EventName}");
            }
        }
    }
}