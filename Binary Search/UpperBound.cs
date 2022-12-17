
int Search(int val, IList<int> a)
{
    int l = -1;
    int r = a.Count;

    while (r - l > 1)
    {
        int m = (l + r) / 2;
        if (a[m] > val)
            r = m;
        else l = m;
    }

    return l; // a[l] <= val if a[l] exists
}
