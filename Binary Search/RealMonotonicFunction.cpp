
double Search(double val, System.Func<double, double> f, double l = 0, double r = 10, double eps = 1e-8)
{
    while (r - l > eps)
    {
        var m = (r + l) / 2;
        if (f(m) > val)
            r = m;
        else l = m;
    }

    return (r + l) / 2;
}
