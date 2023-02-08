using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGameButton : MonoBehaviour
{
    // Reference to the Pacman scene
    public int Pacman;

    // Function to start the game
    public void StartGame()
    {
        // Load the Pacman scene
        SceneManager.LoadScene("Pacman");
    }
}
