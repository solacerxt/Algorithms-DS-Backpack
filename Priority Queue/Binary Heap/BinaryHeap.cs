using System.Collections.Generic;
using System.Linq;

public class BinaryHeap<TElement, TKey> where TKey : notnull, IComparable<TKey>
{
    private readonly List<(TElement Element, TKey Key)> _nodes;

    private readonly IComparer<TKey> _comparer;

    public BinaryHeap()
    {
        _nodes = new();
        _comparer = Comparer<TKey>.Default;
    }

    public BinaryHeap(int capacity)
    {
        _nodes = new(capacity);
        _comparer = Comparer<TKey>.Default;
    }

    public BinaryHeap(int capacity, IComparer<TKey> comparer)
    {
        _nodes = new(capacity);
        _comparer = comparer;
    }

    public BinaryHeap(int capacity, Comparison<TKey> comparison)
    {
        _nodes = new(capacity);
        _comparer = Comparer<TKey>.Create(comparison);
    }

    public BinaryHeap(IEnumerable<(TElement, TKey)> elements)
    {
        _nodes = elements.ToList();
        _comparer = Comparer<TKey>.Default;

        Heapify();
    }

    public BinaryHeap(IEnumerable<(TElement, TKey)> elements, IComparer<TKey> comparer)
    {
        _nodes = elements.ToList();
        _comparer = comparer;

        Heapify();
    }

    public BinaryHeap(IEnumerable<(TElement, TKey)> elements, Comparison<TKey> comparison)
    {
        _nodes = elements.ToList();
        _comparer = Comparer<TKey>.Create(comparison);

        Heapify();
    }

    public IComparer<TKey> Comparer => _comparer;

    public int Count => _nodes.Count;
    public int Capacity => _nodes.Capacity;

    public void Insert(TElement element, TKey key) => Insert((element, key));

    public void Insert((TElement Element, TKey Key) item)
    {
        _nodes.Add(item);

        int cur = _nodes.Count - 1;
        while (cur > 0)
        {
            int next = (cur - 1) / 2;
            
            if (_comparer.Compare(_nodes[next].Key, item.Key) <= 0)
            {
                return;
            }

            (_nodes[cur], _nodes[next]) = (_nodes[next], _nodes[cur]);
            cur = next;
        }
    }

    public (TElement Element, TKey key) GetMin()
    {
        if (_nodes.Count == 0)
        {
            throw new InvalidOperationException("Empty heap");
        }

        return _nodes[0];
    }

    public (TElement Element, TKey Key) RemoveMin()
    {
        if (_nodes.Count == 0)
        {
            throw new InvalidOperationException("Empty heap");
        }

        (TElement, TKey) ret = _nodes[0];

        _nodes[0] = _nodes[^1];
        _nodes.RemoveAt(_nodes.Count - 1);

        SiftDown(0, _nodes.Count);

        return ret;
    }

    public void Clear() => _nodes.Clear();

    public int EnsureCapacity() => _nodes.EnsureCapacity(_nodes.Capacity);

    private void Heapify()
    {
        int n = _nodes.Count;

        for (int i = n - 1; i >= 0; --i)
        {
            SiftDown(i, n);
        }
    }

    private void SiftDown(int cur, int bound)
    {
        int left = 2 * cur + 1;

        while (left < bound)
        {
            int next = left;
            int right = left + 1;

            if (right < bound && _comparer.Compare(_nodes[right].Key, _nodes[left].Key) < 0)
            {
                next = right;
            }
            
            if (_comparer.Compare(_nodes[cur].Key, _nodes[next].Key) <= 0)
            {
                return;
            }

            (_nodes[cur], _nodes[next]) = (_nodes[next], _nodes[cur]);
            cur = next;

            left = 2 * cur + 1;
        }
    }
}
