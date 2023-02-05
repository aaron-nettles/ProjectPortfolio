#include <vector>
#include <iostream>

// Define the Matrix class
class Matrix {
public:
    // Constructor to initialize the matrix
    Matrix(int rows, int columns) : numRows(rows), numColumns(columns), elements(rows, std::vector<int>(columns)) {}

    // Get the number of rows in the matrix
    int getRows() const;

    // Get the number of columns in the matrix
    int getColumns() const;

    // Get the value of an element at a given position in the matrix
    int getElement(int row, int column) const;

    // Set the value of an element at a given position in the matrix
    void setElement(int row, int column, int value);

    // Overloading the << operator for easy printing of the matrix
    friend std::ostream& operator<<(std::ostream& os, const Matrix& matrix);

private:
    // The number of rows in the matrix
    int numRows;

    // The number of columns in the matrix
    int numColumns;

    // The underlying 2D vector to store the elements of the matrix
    std::vector<std::vector<int> > elements;
};

// Get the number of rows in the matrix
int Matrix::getRows() const {
    return numRows;
}

// Get the number of columns in the matrix
int Matrix::getColumns() const {
    return numColumns;
}

// Get the value of an element at a given position in the matrix
int Matrix::getElement(int row, int column) const {
    return elements[row][column];
}

// Set the value of an element at a given position in the matrix
void Matrix::setElement(int row, int column, int value) {
    elements[row][column] = value;
}

// Overloading the << operator for easy printing of the matrix
std::ostream& operator<<(std::ostream& os, const Matrix& matrix) {
    for (int i = 0; i < matrix.numRows; i++) {
        for (int j = 0; j < matrix.numColumns; j++) {
            os << matrix.elements[i][j] << " ";
        }
        os << std::endl;
    }
    return os;
}

// Example usage of the Matrix class
int main() {
    // Create a 2x3 matrix
    Matrix matrix(2, 3);

    // Set the elements of the matrix
    matrix.setElement(0, 0, 1);
    matrix.setElement(0, 1, 2);
    matrix.setElement(0, 2, 3);
    matrix.setElement(1, 0, 4);
    matrix.setElement(1, 1, 5);
    matrix.setElement(1, 2, 6);

    // Print the matrix
    std::cout << matrix << std::endl;

    return 0;
}
