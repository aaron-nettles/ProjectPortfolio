using UnityEngine;

[RequireComponent(typeof(Movement))]
public class Pacman : MonoBehaviour
{
    // Reference to the animated sprite component for death sequence
    public AnimatedSprite deathSequence;

    // Reference to the sprite renderer component of Pacman
    public SpriteRenderer spriteRenderer { get; private set; }

    // Reference to the collider component of Pacman
    public new Collider2D collider { get; private set; }

    // Reference to the movement component of Pacman
    public Movement movement { get; private set; }

    // Called when the script is first enabled and before any Update calls

    private void Awake()
    {
        // Get references to components attached to the game object
        spriteRenderer = GetComponent<SpriteRenderer>();
        collider = GetComponent<Collider2D>();
        movement = GetComponent<Movement>();
    }

    // Update is called once per frame
    private void Update()
    {
        // Check for input for movement direction
        // W or Up Arrow key sets the movement direction to up
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            movement.SetDirection(Vector2.up);
        }
        // S or Down Arrow key sets the movement direction to down
        else if (Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            movement.SetDirection(Vector2.down);
        }
        // A or Left Arrow key sets the movement direction to left
        else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            movement.SetDirection(Vector2.left);
        }
        // D or Right Arrow key sets the movement direction to right
        else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            movement.SetDirection(Vector2.right);
        }

        // Rotate the Pacman sprite to face the movement direction
        float angle = Mathf.Atan2(movement.direction.y, movement.direction.x);
        transform.rotation = Quaternion.AngleAxis(angle * Mathf.Rad2Deg, Vector3.forward);
    }
    // Reset the state of Pacman to its initial state
    public void ResetState()
    {
        enabled = true;
        spriteRenderer.enabled = true;
        collider.enabled = true;
        deathSequence.enabled = false;
        deathSequence.spriteRenderer.enabled = false;
        movement.ResetState();
        gameObject.SetActive(true);
    }
    // Play the death sequence animation of Pacman
    public void DeathSequence()
    {
        enabled = false;
        spriteRenderer.enabled = false;
        collider.enabled = false;
        movement.enabled = false;
        deathSequence.enabled = true;
        deathSequence.spriteRenderer.enabled = true;
        deathSequence.Restart();
    }

}
