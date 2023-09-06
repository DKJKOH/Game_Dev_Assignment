using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
    
public class PauseMenu : MonoBehaviour
{
	[SerializeField]
	GameObject pauseMenuUI;

	private bool isPaused = false; // Track whether the game is paused.

    // Add this Update method to check for the Escape key press.
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
        	Debug.Log("Escape pressed");
        	
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
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
        Debug.Log("Resume Game");
        
	    Time.timeScale = 1f; // Resume the game.
	    isPaused = false;
	    pauseMenuUI.SetActive(false); // Hide the pause menu UI.

	}
	
    // Function to move to main menu
    public void MoveToMainMenu()
    {
        Debug.Log("Move to menu");

        Time.timeScale = 1f; // Resume.
        // To load the next active stage
        SceneManager.LoadScene("MainMenu");
    }

    // Function to exit the game
    public void QuitGame() 
    {
        Debug.Log("Quit Game"); // Check if quit works
        // Quit the game
        Application.Quit();
    }

}