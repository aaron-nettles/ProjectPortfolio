using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Movement : MonoBehaviour
{
    // Public variables for speed, speedMultiplier, initialDirection and obstacleLayer for the movement.
    public float speed = 8f;
    public float speedMultiplier = 1f;
    public Vector2 initialDirection;
    public LayerMask obstacleLayer;

    // Variables for the rigidbody component, direction and nextDirection of movement, and starting position of the object.
    public new Rigidbody2D rigidbody { get; private set; }
    public Vector2 direction { get; private set; }
    public Vector2 nextDirection { get; private set; }
    public Vector3 startingPosition { get; private set; }

    // Function called before the start of the game.
    private void Awake()
    {
        // Get the Rigidbody2D component and store it in the `rigidbody` variable.
        rigidbody = GetComponent<Rigidbody2D>();
        // Store the starting position of the object.
        startingPosition = transform.position;
    }

    // Function called at the start of the game.
    private void Start()
    {
        // Call the ResetState function.
        ResetState();
    }

    // Function to reset the state of the object.
    public void ResetState()
    {
        // Reset the speedMultiplier to 1.
        speedMultiplier = 1f;
        // Set the direction to the initialDirection.
        direction = initialDirection;
        // Reset the nextDirection to zero.
        nextDirection = Vector2.zero;
        // Set the position of the object back to the startingPosition.
        transform.position = startingPosition;
        // Make sure that the rigidbody component is not kinematic.
        rigidbody.isKinematic = false;
        // Enable the script component.
        enabled = true;
    }

    private void Update()
    {
        // Try to move in the next direction while it's queued to make movements
        // more responsive
        if (nextDirection != Vector2.zero) {
            SetDirection(nextDirection);
        }
    }

    private void FixedUpdate()
    {
        Vector2 position = rigidbody.position;
        Vector2 translation = direction * speed * speedMultiplier * Time.fixedDeltaTime;

        rigidbody.MovePosition(position + translation);
    }

    public void SetDirection(Vector2 direction, bool forced = false)
    {
        // Only set the direction if the tile in that direction is available
        // otherwise we set it as the next direction so it'll automatically be
        // set when it does become available
        if (forced || !Occupied(direction))
        {
            this.direction = direction;
            nextDirection = Vector2.zero;
        }
        else
        {
            nextDirection = direction;
        }
    }

    public bool Occupied(Vector2 direction)
    {
        // If no collider is hit then there is no obstacle in that direction
        RaycastHit2D hit = Physics2D.BoxCast(transform.position, Vector2.one * 0.75f, 0f, direction, 1.5f, obstacleLayer);
        return hit.collider != null;
    }

}
