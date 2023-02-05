#include <vector>
#include <iostream>

// Function to merge two subarrays
// Takes in the input array, start index, middle index and end index
void merge(std::vector<int> &arr, int start, int mid, int end) {
    int n1 = mid - start + 1; // size of the first subarray
    int n2 = end - mid; // size of the second subarray

    // Create two temporary arrays to store the subarrays
    std::vector<int> left(n1), right(n2);

    // Copy the elements of the first subarray into the left array
    for (int i = 0; i < n1; i++) {
        left[i] = arr[start + i];
    }

    // Copy the elements of the second subarray into the right array
    for (int i = 0; i < n2; i++) {
        right[i] = arr[mid + i + 1];
    }

    // Initialize two pointers to keep track of the current index in each subarray
    int i = 0, j = 0;

    // Initialize a pointer to keep track of the current index in the main array
    int k = start;

    // Merge the subarrays back into the main array in sorted order
    while (i < n1 && j < n2) {
        if (left[i] <= right[j]) {
            arr[k] = left[i];
            i++;
        } else {
            arr[k] = right[j];
            j++;
        }
        k++;
    }

    // Copy the remaining elements of the left array into the main array
    while (i < n1) {
        arr[k] = left[i];
        i++;
        k++;
    }

    // Copy the remaining elements of the right array into the main array
    while (j < n2) {
        arr[k] = right[j];
        j++;
        k++;
    }
}

// Recursive function to sort the array using merge sort
void mergeSort(std::vector<int> &arr, int start, int end) {
    if (start < end) {
        int mid = (start + end) / 2; // find the middle index

        // Sort the first and second halves of the array recursively
        mergeSort(arr, start, mid);
        mergeSort(arr, mid + 1, end);

        // Merge the sorted subarrays back into the main array
        merge(arr, start, mid, end);
    }
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
    int n = arr.size(); // size of the input array

    std::cout << "Original Array: ";
    for (int i = 0; i < n; i++) {
        std::cout << arr[i] << " ";
    }
    std::cout << std::endl;

    // Call the mergeSort function to sort the array
    mergeSort(arr, 0, n - 1);

    // Print the sorted array
  std::cout << "Sorted array is: \n";
  for (int i = 0; i < n; i++) {
    std::cout << arr[i] << " ";
  }
  std::cout << std::endl;

  return 0;
}