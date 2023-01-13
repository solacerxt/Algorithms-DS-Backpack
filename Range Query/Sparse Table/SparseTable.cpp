#include <vector>
#include <functional>

template<typename T>
class SparseTable {
private:
    std::vector<std::vector<T>> table;
    std::function<T(T, T)> f;

    int log2_floor(int x) const;
public:
    SparseTable(const std::vector<T>& a, std::function<T(T, T)> f);

    T query(int l, int r) const;
};

template<typename T>
SparseTable<T>::SparseTable(const std::vector<T>& a, std::function<T(T, T)> f) {
    this->f = f;

    int n = a.size();
    int log_n = log2_floor(n);
    table = std::vector<std::vector<T>>(log_n + 1);

    table[0] = std::vector<T>(n);
    for (int i = 0; i < n; ++i) {
        table[0][i] = a[i];
    }

    for (int i = 1; i <= log_n; ++i) {
        int len = n + 1 - (1 << i);
        table[i] = std::vector<T>(len);
        for (int j = 0; j < len; ++j) {
            table[i][j] = f(table[i - 1][j], table[i - 1][j + (1 << (i - 1))]);
        }
    }
}

template<typename T>
int SparseTable<T>::log2_floor(int x) const { return 31 - __lzcnt(x); }

template<typename T>
T SparseTable<T>::query(int l, int r) const {
    int p = log2_floor(r - l + 1);
    return f(table[p][l], table[p][r + 1 - (1 << p)]);
}
