#include <functional>

double search(const double val, const std::function<double(double)>& f, double l = 0, double r = 10, const double eps = 1e-8) {
    while (r - l > eps) {
        auto m = (r + l) / 2;
        if (f(m) > val)
            r = m;
        else l = m;
    }

    return (r + l) / 2;
}
