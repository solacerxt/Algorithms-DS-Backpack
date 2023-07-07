using System;
using System.Collections.Generic;

public class UnionFind
{
    private readonly Dictionary<int, int> _parent = new();
    private readonly Dictionary<int, int> _rank = new();

    public UnionFind(IReadOnlyCollection<int> a)
    {
        foreach (var x in a)
        {
            _parent[x] = x;
            _rank[x] = 0;
        }
    }

    public int Find(int x)
    {
        if (_parent[x] != x)
            _parent[x] = Find(_parent[x]);

        return _parent[x];
    }

    public void Merge(int x, int y)
    {
        x = Find(x);
        y = Find(y);

        if (x == y)
            return;

        if (_rank[x] < _rank[y])
            (x, y) = (y, x);

        _parent[y] = x;
        _rank[x] = Math.Max(_rank[x], _rank[y] + 1);
    }

    public bool Contains(int x) => _parent.ContainsKey(x);
}
