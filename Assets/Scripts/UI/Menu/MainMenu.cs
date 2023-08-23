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
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    
    //Quit Game Button
    public void QuitGame() 
    {
        Debug.Log("Quit!"); //To test if the quit button works
        Application.Quit();
    }
}
