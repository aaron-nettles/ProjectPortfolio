#include <iostream>
#include <vector>

using namespace std;

// Class to represent a vertex in the graph
class Vertex {
public:
    int data;
    vector<Vertex*> neighbors;
    Vertex(int data) : data(data) {}
};

// Class to represent a graph
class Graph {
public:
    vector<Vertex*> vertices;

    // Function to add a vertex to the graph
    void addVertex(int data) {
        vertices.push_back(new Vertex(data));
    }

    // Function to add an edge between two vertices
    void addEdge(Vertex *v1, Vertex *v2) {
        v1->neighbors.push_back(v2);
        v2->neighbors.push_back(v1);
    }

    // Function to display the vertices and their neighbors
    void display() {
        for (int i = 0; i < vertices.size(); i++) {
            cout << vertices[i]->data << " -> ";
            for (int j = 0; j < vertices[i]->neighbors.size(); j++) {
                cout << vertices[i]->neighbors[j]->data << " ";
            }
            cout << endl;
        }
    }
};

int main() {
    Graph g;
    g.addVertex(0);
    g.addVertex(1);
    g.addVertex(2);
    g.addVertex(3);
    g.addVertex(4);

    g.addEdge(g.vertices[0], g.vertices[1]);
    g.addEdge(g.vertices[0], g.vertices[2]);
    g.addEdge(g.vertices[1], g.vertices[3]);
    g.addEdge(g.vertices[2], g.vertices[4]);

    g.display();

    return 0;
}
