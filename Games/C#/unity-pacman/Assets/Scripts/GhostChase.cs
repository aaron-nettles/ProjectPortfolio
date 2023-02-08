using UnityEngine;

public class GhostChase : GhostBehavior
{
    // This method is called when the component is disabled.
    private void OnDisable()
    {
        // Enable the scatter behavior when this behavior is disabled.
        ghost.scatter.Enable();
    }
    // This method is called when the component's collider enters a trigger.
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Get the node component from the triggered object.
        Node node = other.GetComponent<Node>();

        // Do nothing while the ghost is frightened
        // Check if the triggered object has a Node component and if this behavior is enabled and not frightened.
        if (node != null && enabled && !ghost.frightened.enabled)
        {
            Vector2 direction = Vector2.zero;
            float minDistance = float.MaxValue;

            // Find the available direction that moves closet to pacman
            // Loop through the available directions of the node.
            foreach (Vector2 availableDirection in node.availableDirections)
            {
                // If the distance in this direction is less than the current
                // min distance then this direction becomes the new closest
                // Calculate the distance between the new position in this direction and the target position.
                Vector3 newPosition = transform.position + new Vector3(availableDirection.x, availableDirection.y);
                float distance = (ghost.target.position - newPosition).sqrMagnitude;

                // Check if this distance is less than the current minimum distance.
                if (distance < minDistance)
                {
                    // Set the direction to this available direction and update the minimum distance.
                    direction = availableDirection;
                    minDistance = distance;
                }
            }
            // Set the direction of the ghost movement to the closest direction.
            ghost.movement.SetDirection(direction);
        }
    }

}
