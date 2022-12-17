
int Search(int val, IList<int> a)
{
    int l = -1;
    int r = a.Count;

    while (r - l > 1)
    {
        int m = (l + r) / 2;
        if (a[m] < val)
            l = m;
        else r = m;
    }

    return r; // a[r] >= val if a[r] exists
}
