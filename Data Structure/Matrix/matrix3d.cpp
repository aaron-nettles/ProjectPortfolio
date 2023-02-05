#include <vector>
#include <iostream>

class Matrix {
private:
    int row, col;
    std::vector<std::vector<int> > data;

public:
    // Constructor to initialize the matrix with a given number of rows and columns
    Matrix(int row, int col) : row(row), col(col) {
        data.resize(row, std::vector<int>(col, 0));
    }

    // Function to access an element at a given position
    int getElement(int i, int j) const {
        return data[i][j];
    }

    // Function to set an element at a given position
    void setElement(int i, int j, int value) {
        data[i][j] = value;
    }

    // Function to print the matrix
    void printMatrix() const {
        for (int i = 0; i < row; i++) {
            for (int j = 0; j < col; j++) {
                std::cout << data[i][j] << " ";
            }
            std::cout << std::endl;
        }
    }
};

// Example usage of the matrix class
int main() {
    Matrix matrix(3, 3);
    matrix.setElement(0, 0, 1);
    matrix.setElement(0, 1, 2);
    matrix.setElement(0, 2, 3);
    matrix.setElement(1, 0, 4);
    matrix.setElement(1, 1, 5);
    matrix.setElement(1, 2, 6);
    matrix.setElement(2, 0, 7);
    matrix.setElement(2, 1, 8);
    matrix.setElement(2, 2, 9);
    matrix.printMatrix();
    return 0;
}
