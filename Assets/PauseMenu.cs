using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
    
public class PauseMenu : MonoBehaviour
{
	[SerializeField]
	GameObject pauseMenuUI;

    [SerializeField]
    public GameObject visionObject;

    [SerializeField]
    public GameObject easyModeText;

	public bool isPaused = false; // Track whether the game is paused.
    public bool isEasy = false; // Make game easy or not

    // Add this Update method to check for the Escape key press.
    public void Update()
    {
    }  

    // Function to pause the game
    public void PauseGame()
	{
        Time.timeScale = 0f; // Pause the game.
	    isPaused = true;
	    pauseMenuUI.SetActive(true); // Show the pause menu UI.
	}

    // Function to resume the game
    public void ResumeGame()
	{
        Time.timeScale = 1f; // Resume the game.
	    isPaused = false;
	    pauseMenuUI.SetActive(false); // Hide the pause menu UI.
	}

    public void EasyMode()
    {
        // Set hard
        if (isEasy)
        {
            isEasy = false;
        }
        // Set easy
        else
        {
            isEasy = true;
        }

        // If easy mode is on
        if (isEasy)
        {
            // Disable vision cone
            visionObject.SetActive(true);
            // Change button text to easy
            easyModeText.gameObject.GetComponent<TextMeshProUGUI>().text = "Mode: Easy";
        }
        // If easy mode is off
        else
        {
            // Enable vision cone
            visionObject.SetActive(false);

            // Change button text to hard
            easyModeText.gameObject.GetComponent<TextMeshProUGUI>().text = "Mode: Hard";

        }

    }

    // Function to move to main menu
    public void MoveToMainMenu()
    {
        Time.timeScale = 1f; // Resume.
        // To load the next active stage
        SceneManager.LoadScene("MainMenu");
    }

    // Function to exit the game
    public void QuitGame() 
    {
        // Quit the game
        Application.Quit();
    }

}