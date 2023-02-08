using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Pellet : MonoBehaviour
{
    // Points awarded to the player for eating this pellet
    public int points = 10;

    // Virtual method that can be overridden by a derived class
    protected virtual void Eat()
    {
        // Call the PelletEaten method of the GameManager object to update the game state
        FindObjectOfType<GameManager>().PelletEaten(this);
    }

    // Triggered when another Collider2D enters the trigger collider attached to this game object
    private void OnTriggerEnter2D(Collider2D other)
    {
        // If the other collider is from the "Pacman" layer, then this pellet has been eaten
        if (other.gameObject.layer == LayerMask.NameToLayer("Pacman"))
        {
            Eat();
        }
    }
}
