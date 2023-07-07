public class BinaryIndexedTree
{
    private readonly int _length;
    private readonly int[] _array;

    public FenwickTree(int length)
    {
        _array = new int[length];
        _length = length;
    }

    public int Sum(int r)
    {
        var i = r - 1;
        var ret = 0;

        while (i >= 0)
        {
            ret += _array[i];
            i = (i & (i + 1)) - 1;
        }

        return ret;
    }

    public void Increase(int i, int value)
    {
        var j = i;
        while (j < _length)
        {
            _array[j] += value;
            j |= (j + 1);
        }
    }
}
