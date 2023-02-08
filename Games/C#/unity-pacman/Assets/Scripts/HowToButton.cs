using UnityEngine;
using UnityEngine.UI;

public class HowToButton : MonoBehaviour
{
    // Reference to the Canvas that holds the pop-up message
    public GameObject HowToPanel;

    // Reference to the "Close" button in the pop-up message
    public Button CloseButton;

    private void Start()
    {
        // Set up the event handler for the "Close" button
        CloseButton.onClick.AddListener(ClosePopUp);

        // Disable the pop-up message at the start of the game
        HowToPanel.SetActive(false);
    }

    // Open the pop-up message when the "How to Play" button is pressed
    public void OpenPopUp()
    {
        HowToPanel.SetActive(true);
    }

    // Close the pop-up message when the "Close" button is pressed
    public void ClosePopUp()
    {
        HowToPanel.SetActive(false);
    }
}
