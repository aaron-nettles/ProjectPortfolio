#include <iostream>
#include <cstdlib>
#include <ctime>

// Function to print the elements of an array
void printArray(int arr[], int size) {
  for (int i = 0; i < size; i++) {
    std::cout << arr[i] << " ";
  }
  std::cout << std::endl;
}

// Function to randomize the elements of an array
void randomizeArray(int arr[], int size) {
  // Seed the random number generator
  srand(time(0));

  for (int i = 0; i < size; i++) {
    // Generate a random index
    int randomIndex = rand() % size;

    // Swap the current element with the element at the random index
    int temp = arr[i];
    arr[i] = arr[randomIndex];
    arr[randomIndex] = temp;
  }
}

int main() {
  int size = 5;
  int arr[5] = {10, 20, 30, 40, 50};

  // Print the original array
  std::cout << "The original array is: ";
  printArray(arr, size);

  // Randomize the array
  randomizeArray(arr, size);

  // Print the randomized array
  std::cout << "The randomized array is: ";
  printArray(arr, size);

  return 0;
}
