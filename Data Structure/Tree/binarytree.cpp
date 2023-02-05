#include <iostream>

using namespace std;

// Definition for a binary tree node
struct TreeNode {
    int val;
    TreeNode *left;
    TreeNode *right;
    TreeNode(int x) : val(x), left(NULL), right(NULL) {}
};

class BinaryTree {
public:
    // Constructor to create the root node
    BinaryTree(int data) {
        root = new TreeNode(data);
    }

    // Function to insert a new node in the binary tree
    void insert(int data) {
        insertHelper(root, data);
    }

    // Helper function for the insert function
    void insertHelper(TreeNode* node, int data) {
        if (data < node->val) {
            if (node->left == NULL) {
                node->left = new TreeNode(data);
            } else {
                insertHelper(node->left, data);
            }
        } else {
            if (node->right == NULL) {
                node->right = new TreeNode(data);
            } else {
                insertHelper(node->right, data);
            }
        }
    }

    // Function to search for a node in the binary tree
    bool search(int data) {
        return searchHelper(root, data);
    }

    // Helper function for the search function
    bool searchHelper(TreeNode* node, int data) {
        if (node == NULL) {
            return false;
        }
        if (node->val == data) {
            return true;
        }
        if (data < node->val) {
            return searchHelper(node->left, data);
        } else {
            return searchHelper(node->right, data);
        }
    }

private:
    TreeNode *root;
};

int main() {
    BinaryTree bt(5);
    bt.insert(3);
    bt.insert(7);
    bt.insert(2);
    bt.insert(4);
    bt.insert(6);
    bt.insert(8);

    int num = 6;
    if (bt.search(num)) {
        cout << num << " is present in the binary tree." << endl;
    } else {
        cout << num << " is not present in the binary tree." << endl;
    }

    return 0;
}
