Array Randomization Program in C++
This is a simple C++ program that initializes an array, randomizes its elements, and displays both the original and the randomized arrays.

The program starts by declaring the size of the array and initializing it with 5 elements: 10, 20, 30, 40, 50.

It then defines two functions, printArray and randomizeArray, to print and randomize the elements of an array respectively.

The printArray function takes an integer array and its size as arguments and prints its elements on the console.

The randomizeArray function uses the srand function to seed the random number generator with the current time, ensuring that the random numbers generated are truly random. It then uses a loop to swap the elements of the array with random elements.

The program then calls the printArray function to display the original array, followed by calling the randomizeArray function to randomize its elements.

Finally, the program calls the printArray function again to display the randomized array.

Requirements
C++ compiler
