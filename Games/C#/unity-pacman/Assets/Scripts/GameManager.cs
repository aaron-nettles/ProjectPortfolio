using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // Variables for the ghosts, pacman, pellets, and the different text elements in the game.
    public Ghost[] ghosts;
    public Pacman pacman;
    public Transform pellets;
    public Text gameOverText;
    public Text scoreText;
    public Text livesText;

    // Variables for the ghost multiplier, score, and lives.
    public int ghostMultiplier { get; private set; } = 1;
    public int score { get; private set; }
    public int lives { get; private set; }

    // This function is called when the game starts.
    private void Start()
    {
        // Start a new game.
        NewGame();
    }

    // This function is called every frame.
    private void Update()
    {
        // If the player is out of lives and presses any key, start a new game.
        if (lives <= 0 && Input.anyKeyDown)
        {
            NewGame();
        }
    }

    // This function starts a new game.
    private void NewGame()
    {
        // Set the score to 0.
        SetScore(0);
        // Set the number of lives to 3.
        SetLives(3);
        // Start a new round.
        NewRound();
    }

    // This function starts a new round.
    private void NewRound()
    {
        // Disable the game over text.
        gameOverText.enabled = false;

        // Enable all pellets.
        foreach (Transform pellet in pellets)
        {
            pellet.gameObject.SetActive(true);
        }

        // Reset the state of all objects in the game.
        ResetState();
    }

    // This function resets the state of all objects in the game.
    private void ResetState()
    {
        // Reset the state of each ghost.
        for (int i = 0; i < ghosts.Length; i++)
        {
            ghosts[i].ResetState();
        }

        // Reset the state of pacman.
        pacman.ResetState();
    }

    // This function triggers the game over state.
    private void GameOver()
    {
        // Enable the game over text.
        gameOverText.enabled = true;

        // Disable all ghosts.
        for (int i = 0; i < ghosts.Length; i++)
        {
            ghosts[i].gameObject.SetActive(false);
        }

        // Disable pacman.
        pacman.gameObject.SetActive(false);
    }

    // This function sets the number of lives.
    private void SetLives(int lives)
    {
        // Set the number of lives.
        this.lives = lives;
        // Update the lives text.
        livesText.text = "x" + lives.ToString();
    }

    // This function sets the score.
    private void SetScore(int score)
    {
        // Set the score.
        this.score = score;
        // Update the score text.
        scoreText.text = score.ToString().PadLeft(2, '0');
    }

    public void PacmanEaten()
    {
        // Call the death sequence of the pacman object
        pacman.DeathSequence();

        // Reduce the number of lives by 1
        SetLives(lives - 1);

        // Check if there are still lives left
        if (lives > 0)
        {
            // If yes, invoke the ResetState method in 3 seconds
            Invoke(nameof(ResetState), 3f);
        }
        else
        {
            // If no, call the GameOver method
            GameOver();
        }
    }

    public void GhostEaten(Ghost ghost)
    {
        // Calculate the points earned from eating the ghost based on the ghostMultiplier
        int points = ghost.points * ghostMultiplier;

        // Add the points to the score
        SetScore(score + points);

        // Increase the ghostMultiplier by 1
        ghostMultiplier++;
    }

    public void PelletEaten(Pellet pellet)
    {
        // Deactivate the pellet object
        pellet.gameObject.SetActive(false);

        // Add the points of the pellet to the score
        SetScore(score + pellet.points);

        // Check if there are still remaining pellets
        if (!HasRemainingPellets())
        {
            // If no, deactivate the pacman object and invoke the NewRound method in 3 seconds
            pacman.gameObject.SetActive(false);
            Invoke(nameof(NewRound), 3f);
        }
    }

    public void PowerPelletEaten(PowerPellet pellet)
    {
        // Loop through all the ghosts and enable the frightened mode for the duration of the power pellet
        for (int i = 0; i < ghosts.Length; i++)
        {
            ghosts[i].frightened.Enable(pellet.duration);
        }

        // Call the PelletEaten method with the power pellet as the argument
        PelletEaten(pellet);

        // Cancel the invoke of the ResetGhostMultiplier method
        CancelInvoke(nameof(ResetGhostMultiplier));

        // Invoke the ResetGhostMultiplier method after the duration of the power pellet
        Invoke(nameof(ResetGhostMultiplier), pellet.duration);
    }

    private bool HasRemainingPellets()
    {
        // Loop through all the pellets and check if there is any active pellet object
        foreach (Transform pellet in pellets)
        {
            if (pellet.gameObject.activeSelf)
            {
                // If yes, return true
                return true;
            }
        }

        // If no, return false
        return false;
    }

    private void ResetGhostMultiplier()
    {
        // Reset the ghostMultiplier to 1
        ghostMultiplier = 1;
    }

}
