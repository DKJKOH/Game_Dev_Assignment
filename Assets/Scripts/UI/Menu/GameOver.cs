using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    

    // Restart Button
    public void RestartButtonTutorial() {
        //Reload the same level
        SceneManager.LoadScene("TutorialLevel");
        Time.timeScale = 1f;

    }

    public void RestartButtonMain() {
        //Reload the same level
        SceneManager.LoadScene("Final Map v3 (New Save)");
        Time.timeScale = 1f;

    }
    // Return Main Menu Button
    public void MainMenu(){
        // Loading of scene to main menu
        SceneManager.LoadScene("MainMenu");
    }
        
    
}
