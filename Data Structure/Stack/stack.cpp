#include <iostream>
#include <vector>

// Stack class definition
class Stack {
private:
  std::vector<int> elements;  // vector to store elements of the stack

public:
  // Function to check if the stack is empty
  bool isEmpty() {
    return elements.empty();
  }

  // Function to push an element onto the stack
  void push(int element) {
    elements.push_back(element);
  }

  // Function to pop an element from the stack
  int pop() {
    if (isEmpty()) {
      std::cout << "Error: Stack is empty" << std::endl;
      return -1;
    } else {
      int top = elements.back();
      elements.pop_back();
      return top;
    }
  }

  // Function to get the top element of the stack
  int top() {
    if (isEmpty()) {
      std::cout << "Error: Stack is empty" << std::endl;
      return -1;
    } else {
      return elements.back();
    }
  }
};

int main() {
  Stack stack;

  // Push elements onto the stack
  stack.push(10);
  stack.push(20);
  stack.push(30);
  stack.push(40);

  // Pop elements from the stack
  std::cout << "Popped element: " << stack.pop() << std::endl;
  std::cout << "Popped element: " << stack.pop() << std::endl;

  // Get the top element of the stack
  std::cout << "Top element: " << stack.top() << std::endl;

  return 0;
}
