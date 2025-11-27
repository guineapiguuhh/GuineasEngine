using System.Collections;

namespace GuineasEngine.Utils.Collections;

public class FastList<T>
{
    public T[] _items;
    public int Count = 0;

    public FastList(int size)
    {
        _items = new T[size];
    }

    public FastList() : this(5) {}

    public T this[int index] => _items[index];

    public void Clear()
    {
        Array.Clear(_items, 0, Count);
        Count = 0;
    }

    public bool Contains(T item)
    {
        var comparer = EqualityComparer<T>.Default;
        for (int i = 0; i < Count; i++)
        {
            if (comparer.Equals(_items[i], item)) return true;
        }
        return false;
    }

    public void Add(T item) => Insert(Count, item);

    public void Insert(int index, T item)
    {
        Count++;
        if (Count >= _items.Length)
        {
            Array.Resize(ref _items, System.Math.Max(_items.Length << 1, 10));
        }
        if (index < Count)
        {
            Array.Copy(_items, index, _items, index + 1, Count - index);
        }
        _items[index] = item;
    }

    public bool Remove(T item)
    {
        var comparer = EqualityComparer<T>.Default;
        for (int i = 0; i < Count; i++)
        {
            if (comparer.Equals(_items[i], item)) 
            { 
                RemoveAt(i);
                return true;
            }
        }
        return false;
    }

    public void RemoveAt(int index)
    {
        Count--;
        if (index < Count)
        {
            Array.Copy(_items, index + 1, _items, index, Count - index);
        }
        _items[Count] = default;
    }

    public void AddRange(IEnumerable<T> collection)
    {
        foreach (var item in collection)
        {
            Add(item);
        }
    }

    public void InsertRange(int index, IEnumerable<T> collection)
    {
        foreach (var item in collection)
        {
            Insert(index++, item);
        }
    }

    public void RemoveRange(IEnumerable<T> collection)
    {
        foreach (var item in collection)
        {
            Remove(item);
        }
    }

    public T[] ToArray() => _items;

    public void Sort() => Array.Sort(_items, 0, Count);
    
    public void Sort(IComparer comparer) => Array.Sort(_items, 0, Count, comparer);
}
