
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

    if (l == -1)
        return a[r];
    if (r != a.Count && a[r] - val < val - a[l])
        return a[r];

    return a[l];
}
