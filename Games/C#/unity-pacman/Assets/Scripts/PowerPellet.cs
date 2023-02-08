using UnityEngine;

public class PowerPellet : Pellet
{
    // The duration of the power effect which can be changed to make the level easier!
    public float duration = 16f;

    // Override the `Eat` method from the base class
    protected override void Eat()
    {
        // Notify the GameManager that a power pellet was eaten
        FindObjectOfType<GameManager>().PowerPelletEaten(this);
    }
}
