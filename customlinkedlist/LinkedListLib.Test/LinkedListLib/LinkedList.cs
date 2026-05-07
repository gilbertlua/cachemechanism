using System.Collections;

namespace LinkedListLib;
public class LinkedList<T> : ICollection<T>
{    
    internal LinkedListNode<T>? head;
    public int _count;
    public LinkedList(IEnumerable<T> collection)
    {
        ArgumentNullException.ThrowIfNull(collection);
        
        foreach (T item in collection)
        {
            AddToEnd(item);
        }
    }
    public void AddToFront(T value)
    {
        LinkedListNode<T> nodeResult = new LinkedListNode<T>(value);
        if( head == null)
        {
            InsertEmptyList(nodeResult);
        }
        else
        {
            InsertNodeBefore(head, nodeResult);
            head = nodeResult;
        }
        
    }
    public void AddToEnd(T value)
    {
        LinkedListNode<T> nodeResult = new LinkedListNode<T>(value);
        if( head == null)
        {
            InsertEmptyList(nodeResult);
        }
        else
        {
            InsertNodeBefore(head,nodeResult);
        }
    }
    public void Add(T value)
    {   
        AddToEnd(value);
    }
    public void Clear()
    {
        LinkedListNode<T>? current = head;
        while (current != null)
        {
            LinkedListNode<T> temp = current;
            current = current.next;
            temp.Clear();
        }

        head = null;
        _count = 0;
    }

    public bool Contains(T value)
    {

        if(FindNode(value) != null)
        {
            return true;   
        }
        return false;
    }

    public void CopyTo(T[] array, int arrayIndex)
    {
        ArgumentNullException.ThrowIfNull(array);
        
        if (arrayIndex < 0 || arrayIndex > array.Length)
            throw new ArgumentOutOfRangeException(nameof(arrayIndex));
        
        if (head == null) return;
        
        if (array is T[] tArray && Count + arrayIndex <= tArray.Length)
        {
            var node = head;
            var index = arrayIndex;
            do
            {
                tArray[index++] = node!.Value;
                node = node.next;
            } 
            while (node != head);
            return;
        }
        throw new ArgumentException("The array is not long enough to fit the items, or the index is invalid");
    }

    public bool Remove(T value)
    {
        LinkedListNode<T>? node = FindNode(value);
        if(node is null)
        {
            return false;
        }
        RemoveNode(node);
        return true;
    }
    
    public int Count {
        get
        {
            return _count;
        }
    }
    public bool IsReadOnly { get; }


    // Private logic

    // insert into empty list
    private void InsertEmptyList(LinkedListNode<T> newNode)
    {
        newNode.next = newNode;
        newNode.prev = newNode;
        head = newNode;
        _count++;
    }

    // insert node before
    private void InsertNodeBefore(LinkedListNode<T> node, LinkedListNode<T> newNode)
    {
        newNode.next = node;
        newNode.prev = node.prev;
        node.prev!.next = newNode;
        node.prev = newNode;
        _count++;
    }
    
    // find linklistnode value
    private LinkedListNode<T>? FindNode(T value)
    {
        if (head == null) return null;
        
        var comparer = EqualityComparer<T>.Default;
        var current = head;
        
        do
        {
            if (comparer.Equals(current.Value, value))
                return current;
            
            current = current.next!;
        } while (current != head);
        
        return null;
    }

    // remove node from given value
    private void RemoveNode(LinkedListNode<T> node)
    {
        if (node.next == node)
        {
            head = null;
        }
        else
        {
            node.next!.prev = node.prev;
            node.prev!.next = node.next;
            if (head == node)
            {
                head = node.next;
            }
        }
        node.Clear();
        _count--;
    }


    // Enumerator Implementation
    public IEnumerator<T> GetEnumerator()
    {
        if (head == null)
        yield break;
    
        LinkedListNode<T>? current = head;
        do
        {
            yield return current!.Value;
            current = current.next;
        } while (current != head);
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}

