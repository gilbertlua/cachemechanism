using NUnit.Framework;
using LinkedListLib;
using System.Security.Cryptography.X509Certificates;

namespace LinkedListLib.Tests;

[TestFixture]
public class LinkedListLibTests
{
    class User
    {
        public int Id{get;set;}
        public string? Name{get;set;}
    }

    [Test]
    public void Constructor_WithCollection_ShouldAddAllItems()
    {   
        var items = new[] { 1, 2, 3, 4, 5 };
        var list = new LinkedList<int>(items);
        Assert.That(list.Count, Is.EqualTo(items.Length));
        CollectionAssert.AreEqual(items, list);
    }

    [Test]
    public void Constructor_WithNullCollection_ShouldThrowArgumentNullException()
    {
        Assert.That(() => new LinkedList<int>(null!), Throws.ArgumentNullException);
    }

    [Test]
    public void Add_ShouldAddItemToEnd()
    {
        var list = new LinkedList<int>(new List<int>());
        var expected = new[] { 1, 2, 3, 4, 5 };    
        foreach (var item in expected)
        {
            list.Add(item);
        }
        
        Assert.That(list.Count, Is.EqualTo(expected.Length));
        Assert.That(list.SequenceEqual(expected), Is.True);
    }

    [Test]
    public void AddToFront_ShouldAddItemAtBeginning()
    {
        var list = new LinkedList<int>(new List<int>());
        list.AddToEnd(2);
        list.AddToEnd(3);
        list.AddToFront(1);
        var expected = new[] { 1, 2, 3 };
        Assert.That(list.SequenceEqual(expected), Is.True);
    }

    [Test]
    public void AddToEnd_ShouldAddItemsInOrder()
    {
        var list = new LinkedList<string>(new List<string>());
        list.AddToEnd("first");
        list.AddToEnd("second");
        list.AddToEnd("third");
        var expected = new[] { "first", "second", "third" };
        Assert.That(list.SequenceEqual(expected), Is.True);
    }

    [Test]
    public void Count_ShouldReturnCorrectNumberOfItems()
    {
        var list = new LinkedList<int>(new List<int>());
        Assert.That(list.Count, Is.EqualTo(0));
        
        list.Add(10);
        Assert.That(list.Count, Is.EqualTo(1));
        
        list.Add(20);
        Assert.That(list.Count, Is.EqualTo(2));
        
        list.Remove(10);
        Assert.That(list.Count, Is.EqualTo(1));
        
        list.Clear();
        Assert.That(list.Count, Is.EqualTo(0));
    }

    [Test]
    public void Clear_ShouldRemoveAllItems()
    {
        var initialData = new[] { 1, 2, 3, 4, 5 };
        var list = new LinkedList<int>(initialData);
        
        list.Clear();
        
         
        Assert.That(list.Count, Is.EqualTo(0));
        Assert.That(list.Any(), Is.False);
    }

    [Test]
    public void Contains_WithExistingItem_ShouldReturnTrue()
    {
        var list = new LinkedList<string>(new[] { "apple", "banana", "cherry" });
        Assert.That(list.Contains("banana"), Is.True);
        Assert.That(list.Contains("apple"), Is.True);
        Assert.That(list.Contains("cherry"), Is.True);
    }

    [Test]
    public void Contains_WithNonExistingItem_ShouldReturnFalse()
    {
        var list = new LinkedList<int>(new[] { 1, 2, 3, 4, 5 });
        Assert.That(list.Contains(10), Is.False);
        Assert.That(list.Contains(0), Is.False);
    }

    [Test]
    public void Contains_WithNullValue_ShouldWorkCorrectly()
    {
        var list = new LinkedList<string?>(new[] { "a", null, "b" });
        Assert.That(list.Contains(null), Is.True);
        Assert.That(list.Contains("c"), Is.False);
    }

    [Test]
    public void Remove_ExistingItem_ShouldReturnTrueAndRemoveItem()
    {
        var list = new LinkedList<int>(new[] { 1, 2, 3, 4, 5 });
        var result = list.Remove(3);
        Assert.That(result, Is.True);
        Assert.That(list.Count, Is.EqualTo(4));
        Assert.That(list.Contains(3), Is.False);
        CollectionAssert.AreEqual(new[] { 1, 2, 4, 5 }, list);
    }

    [Test]
    public void Remove_NonExistingItem_ShouldReturnFalse()
    {
        var list = new LinkedList<int>(new[] { 1, 2, 3, 4, 5 });
        var result = list.Remove(99);
        Assert.That(result, Is.False);
        Assert.That(list.Count, Is.EqualTo(5));
    }

    [Test]
    public void Remove_FirstItem_ShouldUpdateHeadCorrectly()
    {
        var list = new LinkedList<int>(new[] { 1, 2, 3, 4, 5 });
        list.Remove(1);
        Assert.That(list.Count, Is.EqualTo(4));
        CollectionAssert.AreEqual(new[] { 2, 3, 4, 5 }, list);
    }

    [Test]
    public void Remove_LastItem_ShouldWorkCorrectly()
    {
        var list = new LinkedList<int>(new[] { 1, 2, 3, 4, 5 });
        list.Remove(5);
        Assert.That(list.Count, Is.EqualTo(4));
        CollectionAssert.AreEqual(new[] { 1, 2, 3, 4 }, list);
    }

    [Test]
    public void Remove_SingleItemList_ShouldMakeListEmpty()
    {
        var list = new LinkedList<int>(new[] { 42 });
        list.Remove(42);
        Assert.That(list.Count, Is.EqualTo(0));
        Assert.That(list.Any(), Is.False);
    }

    [Test]
    public void CopyTo_ShouldCopyAllItemsToArray()
    {
        var list = new LinkedList<int>(new[] { 1, 2, 3, 4, 5 });
        var array = new int[5];
        list.CopyTo(array, 0);
        CollectionAssert.AreEqual(new[] {1, 2, 3, 4, 5 }, array);
    }

    [Test]
    public void CopyTo_WithOffset_ShouldCopyItemsStartingAtOffset()
    {
        var list = new LinkedList<int>(new[] { 1, 2, 3 });
        var array = new int[5] { 10, 20, 30, 40, 50 };
        var expected = new int[5] { 10, 20, 1, 2, 3 };
        list.CopyTo(array, 2);
        CollectionAssert.AreEqual(expected, array);
    }

    [Test]
    public void CopyTo_WithNullArray_ShouldThrowArgumentNullException()
    {
        var list = new LinkedList<int>(new[] { 1, 2, 3 });
        Assert.That(() => list.CopyTo(null!, 0), Throws.ArgumentNullException);
    }

    [Test]
    public void CopyTo_WithInvalidIndex_ShouldThrowArgumentOutOfRangeException()
    {
        var list = new LinkedList<int>(new[] { 1, 2, 3 });
        var array = new int[3];
        Assert.That(() => list.CopyTo(array, -1), Throws.TypeOf(typeof(ArgumentOutOfRangeException)));
        Assert.That(() => list.CopyTo(array, 5), Throws.TypeOf(typeof(ArgumentOutOfRangeException)));
    }

    [Test]
    public void CopyTo_WhenArrayTooSmall_ShouldThrowArgumentException()
    {
         
        var list = new LinkedList<int>(new[] { 1, 2, 3, 4, 5 });
        var array = new int[3];
        
         
        Assert.That(() => list.CopyTo(array, 2), Throws.ArgumentException);
    }

    [Test]
    public void GetEnumerator_ShouldIterateOverAllItems()
    {
         
        var expected = new[] { 10, 20, 30, 40, 50 };
        var list = new LinkedList<int>(expected);
        var result = list.ToArray();
        CollectionAssert.AreEqual(expected, result);
    }

    [Test]
    public void GetEnumerator_EmptyList_ShouldReturnEmptyEnumerator()
    {
         
        var list = new LinkedList<int>(new List<int>());
        
         
        Assert.That(list.Any(), Is.False);
        Assert.That(list.Count, Is.EqualTo(0));
    }

    [Test]
    public void Foreach_ShouldIterateCorrectly()
    {
         
        var list = new LinkedList<string>(new[] { "one", "two", "three" });
        var result = new List<string>();
        
         
        foreach (var item in list)
        {
            result.Add(item);
        }
        
         
        CollectionAssert.AreEqual(new[] { "one", "two", "three" }, result);
    }

    [Test]
    public void MultipleOperations_ShouldMaintainConsistency()
    {
         
        var list = new LinkedList<int>(new List<int>());
        
         
        list.AddToFront(3);
        list.AddToFront(2);
        list.AddToFront(1);
        list.AddToEnd(4);
        list.AddToEnd(5);
        list.Remove(3);
        list.Add(6);
        
         
        var expected = new[] { 1, 2, 4, 5, 6 };
        CollectionAssert.AreEqual(expected, list);
    }

    [Test]
    public void ComplexTypes_ShouldWorkCorrectly()
    {
         
        var person1 = new Person { Id = 1, Name = "Alice" };
        var person2 = new Person { Id = 2, Name = "Bob" };
        var person3 = new Person { Id = 3, Name = "Charlie" };
        
        var list = new LinkedList<Person>(new[] { person1, person2 });
        
         
        list.Add(person3);
        list.Remove(person2);
        
         
        Assert.That(list.Contains(person1), Is.True);
        Assert.That(list.Contains(person2), Is.False);
        Assert.That(list.Contains(person3), Is.True);
        Assert.That(list.Count, Is.EqualTo(2));
        
        var expected = new[] { person1, person3 };
        CollectionAssert.AreEqual(expected, list);
    }


    [Test]
    public void LINQOperations_ShouldWorkCorrectly()
    {
         
        var list = new LinkedList<int>(new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 });
        
        var evenNumbers = list.Where(x => x % 2 == 0).ToList();
        CollectionAssert.AreEqual(new[] { 2, 4, 6, 8, 10 }, evenNumbers);
        
        var sum = list.Sum();
        Assert.That(sum, Is.EqualTo(55));
        
        var max = list.Max();
        Assert.That(max, Is.EqualTo(10));
        
        var firstThree = list.Take(3).ToList();
        CollectionAssert.AreEqual(new[] { 1, 2, 3 }, firstThree);
        
        var hasSeven = list.Any(x => x == 7);
        Assert.That(hasSeven, Is.True);
    }

    [Test]
    public void NullElements_ShouldBeHandledCorrectly()
    {
         
        var list = new LinkedList<string?>(new[] { "a", null, "b", null, "c" });
        
         
        Assert.That(list.Contains(null), Is.True);
        Assert.That(list.Count(x => x == null), Is.EqualTo(2));
        Assert.That(list.Count(x => x != null), Is.EqualTo(3));
    }

    private class Person
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        
        public override bool Equals(object? obj)
        {
            return obj is Person other && Id == other.Id && Name == other.Name;
        }
        
        public override int GetHashCode()
        {
            return HashCode.Combine(Id, Name);
        }
    }
}