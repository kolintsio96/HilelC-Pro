namespace Library
{
    public class ListEventArgs<T> : EventArgs
    {
        public EventListEnum EventName { get; set; }

        public T? Data { get; set; }

        public int? Index { get; set; }
    }
    public class EventList<T> : List<T>
    {           
        public override void Add(T data)
        {
            base.Add(data);
            AddEvent?.Invoke(this, new ListEventArgs<T> { EventName = EventListEnum.Add, Data = data});
        }

        public override void Insert(int index, T data)
        {
            base.Insert(index, data);
            InsertEvent?.Invoke(this, new ListEventArgs<T> { EventName = EventListEnum.Insert, Data = data, Index = index });
        }

        public override void Remove(T data)
        {
            base.Remove(data);
            RemoveEvent?.Invoke(this, new ListEventArgs<T> { EventName = EventListEnum.Remove, Data = data });
        }
        
        public override void RemoveAt(int index)
        {
            base.RemoveAt(index);
            RemoveEvent?.Invoke(this, new ListEventArgs<T> { EventName = EventListEnum.RemoveAt, Index = index });
        }

        public event EventHandler<ListEventArgs<T>>? AddEvent;

        public event EventHandler<ListEventArgs<T>>? InsertEvent;

        public event EventHandler<ListEventArgs<T>>? RemoveEvent;

        public event EventHandler<ListEventArgs<T>>? RemoveAtEvent;

    }
}
