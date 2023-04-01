#include <vector>
#include <unordered_map>

class UnionFind {
private:
    std::unordered_map<int, int> parent;
    std::unordered_map<int, int> rank;
public:
    UnionFind(const std::vector<int>& a);

    int find(int x);
    void merge(int x, int y);

    bool contains(int x);
};

UnionFind::UnionFind(const std::vector<int>& a) {
    for (int x : a) {
        parent[x] = x;
        rank[x] = 0;
    }
}

int UnionFind::find(int x) {
    if (parent[x] != x) {
        parent[x] = find(parent[x]);
    }
    return parent[x];
}

void UnionFind::merge(int x, int y) {
    x = find(x);
    y = find(y);

    if (x == y) {
        return;
    }

    if (rank[x] < rank[y]) {
        std::swap(x, y);
    }
    parent[y] = x;
    rank[x] = std::max(rank[x], rank[y] + 1);
}

bool UnionFind::contains(int x) {
    return parent.find(x) != parent.end();
}
