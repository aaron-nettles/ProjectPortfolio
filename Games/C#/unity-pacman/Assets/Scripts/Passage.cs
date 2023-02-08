using UnityEngine;
//This script is likely used to create a passage between two parts of a game level,
//where the player can move from one area to another through the trigger of the Collider2D.
[RequireComponent(typeof(Collider2D))]
public class Passage : MonoBehaviour
{
    // Reference to the connected passage's Transform component
    public Transform connection;

    private void OnTriggerEnter2D(Collider2D other)
    {
        // If the collider of an object enters the trigger collider of this object,
        // the object's position will be set to the position of the connected passage.

        Vector3 position = connection.position;
        // Set the z-axis value of the new position to be the same as the object's current z-axis value
        position.z = other.transform.position.z;
        // Set the object's position to the new position
        other.transform.position = position;
    }

}
