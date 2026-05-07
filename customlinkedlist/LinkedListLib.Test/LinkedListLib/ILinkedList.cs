namespace LinkedListLib;

public interface ILinkedList<T>: ICollection<T>
{
    public void AddToFront(T item);
    public void AddToEnd(T item);
}
