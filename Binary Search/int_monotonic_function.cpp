#include <functional>

int search(const double val, const std::function<double(int)>& f, int l = 0, int r = 10) {
    while (r - l > 1) {
        auto m = l + (r - l) / 2;
        if (f(m) > val)
            r = m;
        else l = m;
    }

    return l;
}
