#include <iostream>
#include <vector>
#include <list>
#include <cmath>

using namespace std;

const int TABLE_SIZE = 128;

// Definition for a hash entry
class HashEntry {
public:
    int key;
    int value;
    HashEntry(int k, int v) : key(k), value(v) {}
};

// Hash table class definition
class HashTable {
public:
    // Constructor
    HashTable() {
        table.resize(TABLE_SIZE);
        for (int i = 0; i < TABLE_SIZE; i++) {
            table[i] = new list<HashEntry>;
        }
    }

    // Hash function to get the index for a key
    int hashFunction(int key) {
        return key % TABLE_SIZE;
    }

    // Insert a key-value pair into the hash table
    void insert(int key, int value) {
        int index = hashFunction(key);
        table[index]->push_back(HashEntry(key, value));
    }

    // Search for a value associated with a key in the hash table
    int search(int key) {
        int index = hashFunction(key);
        for (auto i : *table[index]) {
            if (i.key == key) {
                return i.value;
            }
        }
        return -1;
    }

    // Delete a key-value pair from the hash table
    void deleteEntry(int key) {
    int index = hashFunction(key);
    list<HashEntry>::iterator it;
    for (it = table[index]->begin(); it != table[index]->end(); it++) {
        if (it->key == key) {
            table[index]->erase(it);
            break;
        }
    }
}

private:
    vector<list<HashEntry>*> table;
};

int main() {
    HashTable ht;

    ht.insert(1, 10);
    ht.insert(2, 20);
    ht.insert(3, 30);

    cout << "Value for key 1 is: " << ht.search(1) << endl;
    cout << "Value for key 2 is: " << ht.search(2) << endl;
    cout << "Value for key 3 is: " << ht.search(3) << endl;

    ht.deleteEntry(2);
    cout << "Value for key 2 after deletion is: " << ht.search(2) << endl;

    return 0;
}