#include <vector>

int search(const int val, const std::vector<int>& a) {
    int l = -1;
    int r = (int)a.size();

    while (r - l > 1) {
        int m = (l + r) / 2;
        if (a[m] < val)
            l = m;
        else r = m;
    }

    return r; // a[r] >= val if a[r] exists
}
