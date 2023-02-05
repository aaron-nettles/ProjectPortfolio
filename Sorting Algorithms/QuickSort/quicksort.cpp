#include <iostream>
#include <vector>

// Function to partition the array around a pivot element
int partition(std::vector<int>& arr, int low, int high)
{
    // Pivot element is selected as the last element in the sub-array
    int pivot = arr[high];

    // Index of the smaller element
    int i = (low - 1);

    // Iterating through the sub-array
    for (int j = low; j <= high - 1; j++)
    {
        // If current element is smaller than or equal to pivot
        if (arr[j] <= pivot)
        {
            // Increment the index of the smaller element
            i++;

            // Swap the current element with the element at index i
            std::swap(arr[i], arr[j]);
        }
    }

    // Swap the pivot element with the element at index i + 1
    std::swap(arr[i + 1], arr[high]);

    // Return the index of the pivot element
    return (i + 1);
}

// Recursive function to sort the array using QuickSort
void QuickSort(std::vector<int>& arr, int low, int high)
{
    // If low index is smaller than high index, then there are elements to be sorted
    if (low < high)
    {
        // Partition the array around a pivot element
        int pivotIndex = partition(arr, low, high);

        // Recursively sort the sub-arrays on the left and right of the pivot element
        QuickSort(arr, low, pivotIndex - 1);
        QuickSort(arr, pivotIndex + 1, high);
    }
}

// Function to print the elements of the array
void printArray(const std::vector<int>& arr)
{
    for (const auto& element : arr)
    {
        std::cout << element << " ";
    }

    std::cout << std::endl;
}

int main()
{
    // Example array
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

    // Sort the array using QuickSort
    QuickSort(arr, 0, arr.size() - 1);

    std::cout << "After sorting:" << std::endl;
    printArray(arr);

    return 0;
}
