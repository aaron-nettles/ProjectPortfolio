using UnityEngine;

public class PowerPellet : Pellet
{
    // The duration of the power effect which can be changed to make the level easier!
    //This is overrided in the GhostFrightened script, so the changes must be made there
    public float duration = 8f;

    // Override the `Eat` method from the base class
    protected override void Eat()
    {
        // Notify the GameManager that a power pellet was eaten
        FindObjectOfType<GameManager>().PowerPelletEaten(this);
    }
}
