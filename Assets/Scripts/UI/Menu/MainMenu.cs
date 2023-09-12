using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    //Play Game Button
    public void PlayGame()
    {
        // To load the next active stage
        SceneManager.LoadScene("TutorialLevel");
    }
    
    //Quit Game Button
    public void QuitGame() 
    {
        Debug.Log("Quit!"); //To test if the quit button works
        //Quit the game
        Application.Quit();
    }
}
