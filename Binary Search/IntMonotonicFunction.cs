
int Search(double val, System.Func<int, double> f, int l = 0, int r = 10)
{
    while (r - l > 1)
    {
        var m = l + (r - l) / 2;
        if (f(m) > val)
            r = m;
        else l = m;
    }

    return l;
}
