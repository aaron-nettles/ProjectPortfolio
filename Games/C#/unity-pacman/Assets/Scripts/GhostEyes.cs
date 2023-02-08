using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class GhostEyes : MonoBehaviour
{
    // Sprites for when the ghost is facing up, down, left, and right
    public Sprite up;
    public Sprite down;
    public Sprite left;
    public Sprite right;

    // Reference to the sprite renderer component
    public SpriteRenderer spriteRenderer { get; private set; }
    // Reference to the movement component of the parent game object
    public Movement movement { get; private set; }

    private void Awake()
    {
        // Get the reference to the sprite renderer component
        spriteRenderer = GetComponent<SpriteRenderer>();
        // Get the reference to the movement component of the parent game object
        movement = GetComponentInParent<Movement>();
    }

    private void Update()
    {
        // Change the sprite based on the direction of the movement component
        if (movement.direction == Vector2.up)
        {
            spriteRenderer.sprite = up;
        }
        else if (movement.direction == Vector2.down)
        {
            spriteRenderer.sprite = down;
        }
        else if (movement.direction == Vector2.left)
        {
            spriteRenderer.sprite = left;
        }
        else if (movement.direction == Vector2.right)
        {
            spriteRenderer.sprite = right;
        }
    }

}
