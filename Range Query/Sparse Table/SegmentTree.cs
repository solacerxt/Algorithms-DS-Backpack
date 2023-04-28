
public interface IBinaryOperation<T>
{
    T IdentityElement { get; }
    T Invoke(T arg1, T arg2);
}

public class SegmentTree<T, TOperation> where TOperation : IBinaryOperation<T>, new()
{
    private T[][] _tree;

    private readonly int Length;
    private readonly TOperation Operation;

    public SegmentTree(int length)
    {
        if (length < 0 || length > int.MaxValue / 2)
        {
            throw new ArgumentOutOfRangeException(nameof(length));
        }

        Length = length;
        Operation = new TOperation();
        BuildTree();
    }

    public SegmentTree(IEnumerable<T> items)
    {
        Length = items.Count();
        Operation = new TOperation();

        BuildTree();

        int level = _tree!.Length - 1;
        int cur = 0;
        foreach (var item in items)
        {
            _tree[level][cur++] = item;
        }

        for (; level > 0; --level)
        {
            int len = 1 << (level - 1);
            for (int i = 0; i < len; ++i)
            {
                _tree[level - 1][i] = Operation.Invoke(_tree[level][2 * i], _tree[level][2 * i + 1]);
            }
        }
    }

    public T this[int index]
    {
        get
        {
            if (index < 0 || index >= Length)
            {
                throw new ArgumentOutOfRangeException(nameof(index));
            }

            return _tree[^1][index];
        }

        set
        {
            if (index < 0 || index >= Length)
            {
                throw new ArgumentOutOfRangeException(nameof(index));
            }

            int level = _tree.Length - 1;
            _tree[level][index] = value;
            
            for (; level > 0; --level)
            {
                index /= 2;
                _tree[level - 1][index] = Operation.Invoke(_tree[level][2 * index], _tree[level][2 * index + 1]);
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="left"></param>
    /// <param name="right"></param>
    /// <returns>The result of operation on range [left..right) if the range is not empty, otherwise returns the identity element of operation</returns>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    public T Query(int left, int right)
    {
        if (left < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(left));
        }

        if (right > Length)
        {
            throw new ArgumentOutOfRangeException(nameof(right));
        }

        return right > left ? Count(left, right, 0, 0) : Operation.IdentityElement;
    }

    private T Count(int l, int r, int index, int level)
    {
        int nodeLeft = index << (_tree.Length - 1 - level);
        int nodeRight = (index + 1) << (_tree.Length - 1 - level);

        if (r <= nodeLeft || l >= nodeRight)
        {
            return Operation.IdentityElement;
        }

        if (l <= nodeLeft && r >= nodeRight)
        {
            return _tree[level][index];
        }

        return Operation.Invoke(Count(l, r, 2 * index, level + 1), Count(l, r, 2 * index + 1, level + 1));
    }

    private void BuildTree()
    {
        int cnt = Log2Ceil((uint)Length);
        _tree = new T[cnt + 1][];

        for (int i = 0; i <= cnt; ++i)
        {
            _tree[i] = new T[1u << i];
            for (int j = 0; j < _tree[i].Length; ++j)
            {
                _tree[i][j] = Operation.IdentityElement;
            }
        }
    }

    private int Log2Ceil(uint x)
    {
        int ret = 31 - BitOperations.LeadingZeroCount(x);

        return (1u << ret) < x ? ret + 1 : ret;
    }
}
