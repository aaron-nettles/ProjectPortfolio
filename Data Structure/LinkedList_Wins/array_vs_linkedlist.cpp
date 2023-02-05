#include <iostream>
#include <ctime>
#include <cstdlib>
#include <list>
#include <array>

// Function to calculate the execution time of a task
double get_time(clock_t start, clock_t end) {
    return static_cast<double>(end - start) / CLOCKS_PER_SEC;
}

int main() {
    // Array
    std::array<int, 10000> arr;
    for (int i = 0; i < 10000; i++) {
        arr[i] = rand() % 10000;
    }
    
    // Linked List
    std::list<int> list;
    for (int i = 0; i < 10000; i++) {
        list.push_back(rand() % 10000);
    }

    // Time the operation of accessing the 1000th element in the array
    clock_t start = clock();
    int arr_1000 = arr[999];
    clock_t end = clock();
    double arr_time = get_time(start, end);

    // Time the operation of accessing the 1000th element in the linked list
    start = clock();
    std::list<int>::iterator it = list.begin();
    std::advance(it, 999);
    int list_1000 = *it;
    end = clock();
    double list_time = get_time(start, end);

    // Compare the execution time
    std::cout << "Array time: " << arr_time << " sec" << std::endl;
    std::cout << "Linked List time: " << list_time << " sec" << std::endl;

    return 0;
}
