using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    

    // Restart Button
    public void RestartButton() {
        //Reload the same level
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    // Return Main Menu Button
    public void MainMenu(){
        SceneManager.LoadScene("MainMenu");
    }
        
    
}
