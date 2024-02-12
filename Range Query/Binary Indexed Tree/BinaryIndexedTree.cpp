#include <vector>

class BinaryIndexedTree {
private:
    int m_length;
    std::vector<int> m_array;
public:
    BinaryIndexedTree(int length) {
        m_array = std::vector<int>(length);
        m_length = length;
    }

    int Sum(int r) {
        int i{ r - 1 };
        int ret{};

        while (i >= 0) {
            ret += m_array[i];
            i = (i & (i + 1)) - 1;
        }

        return ret;
    }

    int Sum(int l, int r) {
        return Sum(r) - Sum(l - 1);
    }

    void Increase(int i, int value) {
        int j{ i };
        while (j < m_length) {
            m_array[j] += value;
            j |= (j + 1);
        }
    }
};
