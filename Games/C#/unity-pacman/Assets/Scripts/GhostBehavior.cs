using UnityEngine;

[RequireComponent(typeof(Ghost))]
public abstract class GhostBehavior : MonoBehaviour
{
    // Reference to the Ghost component attached to the same game object
    public Ghost ghost { get; private set; }

    // The duration of this behavior in seconds
    public float duration;

    private void Awake()
    {
        // Store a reference to the Ghost component
        ghost = GetComponent<Ghost>();
    }

    // Enables this behavior for the default duration
    public void Enable()
    {
        Enable(duration);
    }

    // Enables this behavior for a specified duration
    public virtual void Enable(float duration)
    {
        // Enable the behavior component
        enabled = true;

        // Cancel any previous invokes that may have been scheduled
        CancelInvoke();

        // Schedule the Disable method to be called after the specified duration
        Invoke(nameof(Disable), duration);
    }

    // Disables this behavior
    public virtual void Disable()
    {
        // Disable the behavior component
        enabled = false;

        // Cancel any previous invokes that may have been scheduled
        CancelInvoke();
    }

}
