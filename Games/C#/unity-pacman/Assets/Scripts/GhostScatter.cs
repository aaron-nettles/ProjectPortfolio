using UnityEngine;

public class GhostScatter : GhostBehavior
{
    // Start the "Chase" behavior when this behavior is disabled
    private void OnDisable()
    {
        ghost.chase.Enable();
    }
    // Handle the trigger when entering a node
    private void OnTriggerEnter2D(Collider2D other)
    {
        Node node = other.GetComponent<Node>();

        // Do nothing while the ghost is frightened
        if (node != null && enabled && !ghost.frightened.enabled)
        {
            // Pick a random available direction
            int index = Random.Range(0, node.availableDirections.Count);

            // Prefer not to go back the same direction so increment the index to
            // the next available direction
            if (node.availableDirections.Count > 1 && node.availableDirections[index] == -ghost.movement.direction)
            {
                index++;

                // Wrap the index back around if overflowed
                if (index >= node.availableDirections.Count) {
                    index = 0;
                }
            }
            // Set the ghost's direction to the selected direction
            ghost.movement.SetDirection(node.availableDirections[index]);
        }
    }

}
