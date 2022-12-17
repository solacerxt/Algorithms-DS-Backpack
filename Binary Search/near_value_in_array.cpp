#include <vector>

int search(const int val, const std::vector<int>& a) {
    int l = -1;
    int r = (int)a.size();

    while (r - l > 1) {
        int m = (l + r) / 2;
        if (a[m] > val)
            r = m;
        else l = m;
    }
    
    if (l == -1)
        return a[r];
    if (r != a.size() && a[r] - val < val - a[l])
        return a[r];
    
    return a[l];
}
