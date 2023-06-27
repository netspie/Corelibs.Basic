using System.Collections;

namespace Corelibs.Basic.Collections;

public interface IRemoveOnlyList<out T> : IEnumerable<T>
{
    bool RemoveAt(int index);
    int Count { get; }
    T this[int index] { get; }
}

public class RemoveOnlyList<T> : IRemoveOnlyList<T>
{
    private readonly T[] _source;

    public int Count { get; private set; }

    public RemoveOnlyList(IEnumerable<T> source)
    {
        _source = source.ToArray();
        Count = _source.Length;
    }

    public T this[int index] {
        get {
            if ((uint) index >= (uint) Count)
                throw new IndexOutOfRangeException();

            return _source[index];
        }

        set {
            if ((uint) index >= (uint) Count)
                throw new IndexOutOfRangeException();

            _source[index] = value;
        }
    }

    /// <summary>
    /// Removes an item at specified index.
    /// </summary>
    /// <param name="index"></param>
    /// <returns>True if successfully removed an element, false otherwise.</returns>
    public bool RemoveAt(int index)
    {
        if (index < 0 || index >= Count)
            return false;

        _source.Swap(index, Count - 1);
        --Count;
        _source[Count - 1] = default;

        return true;
    }

    IEnumerator IEnumerable.GetEnumerator() => _source.Take(Count).GetEnumerator();

    public IEnumerator<T> GetEnumerator()
    {
        foreach (T item in _source.Take(Count))
            yield return item;
    }
}

public static class RemoveOnlyListExtensions
{
    public static IRemoveOnlyList<T> ToRemoveOnlyList<T>(this IEnumerable<T> source) => 
        new RemoveOnlyList<T>(source);
}
