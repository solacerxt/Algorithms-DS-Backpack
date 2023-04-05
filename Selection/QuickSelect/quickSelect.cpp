#include <vector>
#include <chrono>
#include <random>

void partition(std::vector<int>& a, int x, int start, int end, int& l, int& r) {
    l = start;
    for (int i = l; i < end; ++i) {
        while (i >= l && a[i] < x) {
            std::swap(a[i], a[l]);
            ++l;
        }
    }

    r = l;
    for (int i = r; i < end; ++i) {
        while (i >= r && a[i] == x) {
            std::swap(a[i], a[r]);
            ++r;
        }
    }
}

int quickSelect(std::vector<int>& a, int k, int start = 0, int end = -1) {
    static std::mt19937 rng(std::chrono::steady_clock::now().time_since_epoch().count());

    if (end == -1) {
        end = (int)a.size();
    }
    if (end - start == 1) {
        return a[start];
    }
    
    int x = a[rng() % (end - start) + start];
    int l, r;
    partition(a, x, start, end, l, r);
    
    if (k <= l) {
        return quickSelect(a, k, start, l);
    }
    if (k <= r) {
        return x;
    }
    return quickSelect(a, k, r, end);
}
