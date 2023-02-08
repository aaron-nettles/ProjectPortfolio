using UnityEngine;

[DefaultExecutionOrder(-10)]
[RequireComponent(typeof(Movement))]
public class Ghost : MonoBehaviour
{
    // Public properties for referencing Movement component and home, scatter, chase, and frightened behaviors
    public Movement movement { get; private set; }
    public GhostHome home { get; private set; }
    public GhostScatter scatter { get; private set; }
    public GhostChase chase { get; private set; }
    public GhostFrightened frightened { get; private set; }

    // Reference to the initial behavior of the ghost
    public GhostBehavior initialBehavior;

    // Target transform for the ghost
    public Transform target;

    // Number of points the ghost is worth
    public int points = 200;

    private void Awake()
    {
        // Get references to the Movement component and the home, scatter, chase, and frightened behaviors
        movement = GetComponent<Movement>();
        home = GetComponent<GhostHome>();
        scatter = GetComponent<GhostScatter>();
        chase = GetComponent<GhostChase>();
        frightened = GetComponent<GhostFrightened>();
    }

    private void Start()
    {
        ResetState();
    }

    // Reset the ghost's state
    public void ResetState()
    {
        // Enable the ghost's gameObject
        gameObject.SetActive(true);

        // Reset the movement state
        movement.ResetState();

        // Disable frightened behavior and enable scatter behavior
        frightened.Disable();
        chase.Disable();
        scatter.Enable();

        // If the initial behavior is not the home behavior, disable the home behavior
        if (home != initialBehavior)
        {
            home.Disable();
        }

        // If the initial behavior is not null, enable it
        if (initialBehavior != null)
        {
            initialBehavior.Enable();
        }
    }

    // Set the ghost's position
    public void SetPosition(Vector3 position)
    {
        // Keep the z-position the same to maintain draw depth
        position.z = transform.position.z;
        transform.position = position;
    }

    // Handle collision with other objects
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the collision is with Pacman
        if (collision.gameObject.layer == LayerMask.NameToLayer("Pacman"))
        {
            // If the frightened behavior is enabled, call the GhostEaten method in the GameManager
            if (frightened.enabled)
            {
                FindObjectOfType<GameManager>().GhostEaten(this);
            }
            // Otherwise, call the PacmanEaten method in the GameManager
            else
            {
                FindObjectOfType<GameManager>().PacmanEaten();
            }
        }
    }
}
