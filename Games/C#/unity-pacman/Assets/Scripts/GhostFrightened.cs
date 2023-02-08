using UnityEngine;

public class GhostFrightened : GhostBehavior
{
    // Variables to store the reference of sprite renderers for body, eyes, blue, and white
    public SpriteRenderer body;
    public SpriteRenderer eyes;
    public SpriteRenderer blue;
    public SpriteRenderer white;

    // Property to keep track if the ghost has been eaten
    public bool eaten { get; private set; }

    // Override the Enable method from base class GhostBehavior
    public override void Enable(float duration)
    {
        //this code controls how easy the level can be
        duration = 12f;
        // Call the Enable method from the base class
        base.Enable(duration);
        // Disable body sprite renderer
        body.enabled = false;
        // Disable eyes sprite renderer
        eyes.enabled = false;
        // Enable blue sprite renderer
        blue.enabled = true;
        // Disable white sprite renderer
        white.enabled = false;

        // Invoke the Flash method after duration / 2 seconds
        Invoke(nameof(Flash), duration / 2f);
    }
    // Override the Disable method from the base class
    public override void Disable()
    {
        base.Disable();

        // Call the Disable method from the base class
        base.Disable();

        // Enable body sprite renderer
        body.enabled = true;

        // Enable eyes sprite renderer
        eyes.enabled = true;

        // Disable blue sprite renderer
        blue.enabled = false;

        // Disable white sprite renderer
        white.enabled = false;
    }

    // Method to handle eating of ghost
    private void Eaten()
    {
        // Set the eaten property to true
        eaten = true;

        // Set the ghost position to its home position
        ghost.SetPosition(ghost.home.inside.position);

        // Enable the ghost home
        ghost.home.Enable(duration);

        // Disable body sprite renderer
        body.enabled = false;

        // Enable eyes sprite renderer
        eyes.enabled = true;

        // Disable blue sprite renderer
        blue.enabled = false;

        // Disable white sprite renderer
        white.enabled = false;
    }

    // Method to flash the white sprite renderer
    private void Flash()
    {
        // If the ghost has not been eaten
        if (!eaten)
        {
            // Disable blue sprite renderer
            blue.enabled = false;

            // Enable white sprite renderer
            white.enabled = true;

            // Restart the animation of the white sprite renderer component
            white.GetComponent<AnimatedSprite>().Restart();
        }
    }

    // Method called when the component is enabled
    private void OnEnable()
    {
        // Restart the animation of the blue sprite renderer component
        blue.GetComponent<AnimatedSprite>().Restart();

        // Set the speed multiplier of the ghost movement component to 0.5f
        ghost.movement.speedMultiplier = 0.5f;

        // Set the eaten property to false
        eaten = false;
    }

    // Method that is called when the script is disabled
    private void OnDisable()
    {
        // Set the speed multiplier for the ghost's movement back to 1
        ghost.movement.speedMultiplier = 1f;

        // Set the eaten variable to false
        eaten = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the collider that triggered the event is a Node component
        Node node = other.GetComponent<Node>();

        // If the collider is a Node and the ghost behavior is enabled
        if (node != null && enabled)
        {
            // Initialize direction and max distance as zero
            Vector2 direction = Vector2.zero;
            float maxDistance = float.MinValue;

            // Loop through all the available directions of the Node component
            foreach (Vector2 availableDirection in node.availableDirections)
            {
                // Calculate the new position if the ghost moves in this direction
                Vector3 newPosition = transform.position + new Vector3(availableDirection.x, availableDirection.y);

                // Calculate the distance between the new position and pacman's position
                float distance = (ghost.target.position - newPosition).sqrMagnitude;

                // If the distance in this direction is greater than the current
                // max distance then this direction becomes the new farthest
                if (distance > maxDistance)
                {
                    direction = availableDirection;
                    maxDistance = distance;
                }
            }

            // Set the ghost's direction to the farthest direction from pacman
            ghost.movement.SetDirection(direction);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // If the ghost collides with an object with a layer named "Pacman"
        if (collision.gameObject.layer == LayerMask.NameToLayer("Pacman"))
        {
            // If the ghost behavior is enabled
            if (enabled)
            {
                // Call the Eaten method
                Eaten();
            }
        }
    }


}
