#include <iostream>
#include <vector>
using namespace std;

// Define the Heap class
class Heap {
public:
    // Constructor to initialize the heap
    Heap(int type) : heap_type(type) {}

    // Insert an element into the heap
    void insert(int key);

    // Remove the root element from the heap
    int extractRoot();

    // Get the size of the heap
    int getSize() const;

    // Get the root element of the heap
    int getRoot() const;

private:
    // Helper function to heapify the tree after an insertion
    void heapifyUp(int index);

    // Helper function to heapify the tree after a removal
    void heapifyDown(int index);

    // Type of the heap (min heap or max heap)
    int heap_type;

    // The underlying vector to store the elements of the heap
    vector<int> elements;
};

// Insert an element into the heap
void Heap::insert(int key) {
    // Add the new key to the end of the vector
    elements.push_back(key);

    // Heapify the tree to maintain the heap property
    heapifyUp(elements.size() - 1);
}

// Remove the root element from the heap
int Heap::extractRoot() {
    // Swap the first and last elements of the vector
    swap(elements[0], elements[elements.size() - 1]);

    // Remove the last element (which is now the root) from the vector
    int root = elements.back();
    elements.pop_back();

    // Heapify the tree to maintain the heap property
    heapifyDown(0);

    return root;
}

// Get the size of the heap
int Heap::getSize() const {
    return elements.size();
}

// Get the root element of the heap
int Heap::getRoot() const {
    return elements[0];
}

// Heapify the tree after an insertion
void Heap::heapifyUp(int index) {
    // Calculate the parent index of the current node
    int parentIndex = (index - 1) / 2;

    // If the parent is greater than the current node, swap them
    if (heap_type == 1 && elements[parentIndex] > elements[index]) {
        swap(elements[parentIndex], elements[index]);

        // Recursively heapify the tree
        heapifyUp(parentIndex);
    }
    else if (heap_type == 0 && elements[parentIndex] < elements[index]) {
        swap(elements[parentIndex], elements[index]);

        // Recursively heapify the tree
        heapifyUp(parentIndex);
    }
}

// Heapify the tree after a removal
void Heap::heapifyDown(int index) {
    // Calculate the indices of the left and right children of the current node
    int leftIndex = 2 * index + 1;
    int rightIndex = 2 * index + 2;

    // Find the smallest child
    int smallestIndex = index;
    if (leftIndex < elements.size() && elements[leftIndex] < elements[smallestIndex]) {
        smallestIndex = leftIndex;
}
if (rightIndex < elements.size() && elements[rightIndex] < elements[smallestIndex]) {
smallestIndex = rightIndex;
}
// If the smallest child is smaller than the current node, swap them
if (smallestIndex != index) {
    swap(elements[index], elements[smallestIndex]);

    // Recursively heapify the tree
    heapifyDown(smallestIndex);
}
}

// Example usage of the Heap class
int main() {
// Create a min heap
Heap minHeap(0);

// Insert some elements into the heap
minHeap.insert(5);
minHeap.insert(2);
minHeap.insert(7);
minHeap.insert(1);
minHeap.insert(6);
minHeap.insert(9);

// Print the root element of the heap (should be 1)
cout << "Root: " << minHeap.getRoot() << endl;

// Extract the root element from the heap
cout << "Extracted: " << minHeap.extractRoot() << endl;

// Print the root element of the heap again (should be 2)
cout << "Root: " << minHeap.getRoot() << endl;

return 0;

}
