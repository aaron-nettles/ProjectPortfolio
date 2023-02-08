using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class AnimatedSprite : MonoBehaviour
{
    // Reference to the sprite renderer component.
    public SpriteRenderer spriteRenderer { get; private set; }

    // List of sprites to animate.
    public Sprite[] sprites = new Sprite[0];

    // Time between each frame of the animation. This can be modified to slow the animation to be easier!
    public float animationTime = 0.25f;

    // The current frame of the animation.
    public int animationFrame { get; private set; }

    // Whether the animation should loop back to the first frame after the last frame.
    public bool loop = true;

    private void Awake()
    {
        // Get reference to sprite renderer component.
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        // Start repeating the `Advance` method every `animationTime` seconds.
        InvokeRepeating(nameof(Advance), animationTime, animationTime);
    }

    private void Advance()
    {
        // Check if the sprite renderer component is enabled.
        if (!spriteRenderer.enabled)
        {
            return;
        }

        // Increase the animation frame by 1.
        animationFrame++;

        // If the current animation frame is greater than or equal to the number of sprites and looping is enabled, reset the frame to 0.
        if (animationFrame >= sprites.Length && loop)
        {
            animationFrame = 0;
        }

        // If the current animation frame is within the bounds of the list of sprites, update the sprite renderer's sprite.
        if (animationFrame >= 0 && animationFrame < sprites.Length)
        {
            spriteRenderer.sprite = sprites[animationFrame];
        }
    }

    // Restart the animation from the first frame.
    public void Restart()
    {
        // Set the animation frame to -1.
        animationFrame = -1;

        // Advance to the first frame of the animation.
        Advance();
    }

}
