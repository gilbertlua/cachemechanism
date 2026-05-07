
namespace LinkedListLib;
public class LinkedListNode<T>
{
    private LinkedList<T>? list;
    public LinkedListNode<T>? next;
    public LinkedListNode<T>? prev;
    private T item;

    public LinkedListNode(T value)
    {
        item = value;
    }
    public LinkedList<T>? List
    {
        get { return list; }
    }

    public LinkedListNode<T>? Next
    {
        get { return next == null || next == list!.head ? null : next; }
    }

    public LinkedListNode<T>? Previous
    {
        get { return prev == null || this == list!.head ? null : prev; }
    }

    public T Value
    {
        get { return item; }
        set { item = value; }
    }
    public void Clear()
    {
        list = null;
        next = null;
        prev = null;
    }
}