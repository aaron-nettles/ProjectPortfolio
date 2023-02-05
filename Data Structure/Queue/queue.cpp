#include <iostream>
#include <queue>

class Queue {
private:
  std::queue<int> elements;

public:
  // Function to check if the queue is empty
  bool isEmpty() {
    return elements.empty();
  }

  // Function to add an element to the queue
  void enqueue(int element) {
    elements.push(element);
  }

  // Function to remove an element from the queue
  int dequeue() {
    if (isEmpty()) {
      std::cout << "Error: Queue is empty" << std::endl;
      return -1;
    } else {
      int element = elements.front();
      elements.pop();
      return element;
    }
  }

  // Function to get the front element of the queue
  int front() {
    if (isEmpty()) {
      std::cout << "Error: Queue is empty" << std::endl;
      return -1;
    } else {
      return elements.front();
    }
  }
};

int main() {
  Queue queue;

  queue.enqueue(1);
  queue.enqueue(2);
  queue.enqueue(3);

  std::cout << "Front element: " << queue.front() << std::endl;
  std::cout << "Dequeued element: " << queue.dequeue() << std::endl;
  std::cout << "Dequeued element: " << queue.dequeue() << std::endl;

  return 0;
}
