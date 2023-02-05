#include <iostream>
#include <vector>

// Function to implement bubble sort algorithm
void bubbleSort(std::vector<int> &arr) {
    int n = arr.size();

    for (int i = 0; i < n-1; i++) {
        // Last i elements are already sorted
        for (int j = 0; j < n-i-1; j++) {
            if (arr[j] > arr[j+1]) {
                // Swap arr[j] and arr[j+1] if arr[j] is greater
                int temp = arr[j];
                arr[j] = arr[j+1];
                arr[j+1] = temp;
            }
        }
    }
}

// Function to print the elements of an array
void printArray(std::vector<int> &arr) {
    for (int i : arr) {
        std::cout << i << " ";
    }
    std::cout << std::endl;
}

int main() {
    std::vector<int> arr;
arr.push_back(64);
arr.push_back(34);
arr.push_back(25);
arr.push_back(12);
arr.push_back(22);
arr.push_back(11);
arr.push_back(90);

    std::cout << "Before sorting:" << std::endl;
    printArray(arr);
    bubbleSort(arr);
    std::cout << "After sorting:" << std::endl;
    printArray(arr);

    return 0;
}
