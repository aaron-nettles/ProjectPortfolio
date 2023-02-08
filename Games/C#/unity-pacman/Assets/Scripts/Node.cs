using System.Collections.Generic;
using UnityEngine;

// This class is responsible for checking the availability of a particular direction for a node.
public class Node : MonoBehaviour
{
    // The layer mask to check for obstacles 
    public LayerMask obstacleLayer;
    // A list to store the available directions
    public List<Vector2> availableDirections { get; private set; }

    private void Start()
    {
        // Initializing the availableDirections list
        availableDirections = new List<Vector2>();

        // We determine if the direction is available by box casting to see if
        // we hit a wall. The direction is added to list if available.
        CheckAvailableDirection(Vector2.up);
        CheckAvailableDirection(Vector2.down);
        CheckAvailableDirection(Vector2.left);
        CheckAvailableDirection(Vector2.right);
    }
    // This method checks if a particular direction is available or not
    private void CheckAvailableDirection(Vector2 direction)
    {
        // Performing a box cast to check for obstacles in the desired direction.
        // A hit is considered if a collider is detected in the desired direction.
        RaycastHit2D hit = Physics2D.BoxCast(transform.position, Vector2.one * 0.5f, 0f, direction, 1f, obstacleLayer);

        // If no collider is hit then there is no obstacle in that direction
        if (hit.collider == null) {
            // Adding the direction to the availableDirections list
            availableDirections.Add(direction);
        }
    }

}
