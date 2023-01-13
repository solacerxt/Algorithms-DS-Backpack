#include <vector>
#include <functional>

class SparseTable32 {
private:
    std::vector<std::vector<int>> table;
    std::function<int(int, int)> f;

    int log2_floor(int x) const;
public:
    SparseTable32(const std::vector<int>& a, std::function<int(int, int)> f);

    int query(int l, int r) const;
};

SparseTable32::SparseTable32(const std::vector<int>& a, std::function<int(int, int)> f) {
    this->f = f;

    int n = a.size();
    int log = log2_floor(n);
    table = std::vector<std::vector<int>>(log + 1);

    table[0] = std::vector<int>(n);
    for (int i = 0; i < n; ++i) {
        table[0][i] = a[i];
    }

    for (int i = 1; i <= log; ++i) {
        int len = n + 1 - (1 << i);
        table[i] = std::vector<int>(len);
        for (int j = 0; j < len; ++j) {
            table[i][j] = f(table[i - 1][j], table[i - 1][j + (1 << (i - 1))]);
        }
    }
}

int SparseTable32::log2_floor(int x) const { return 31 - __lzcnt(x); }

int SparseTable32::query(int l, int r) const {
    int p = log2_floor(r - l + 1);
    return f(table[p][l], table[p][r + 1 - (1 << p)]);
}
