using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckInput : MonoBehaviour
{
    public PauseMenu PauseMenu; 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("p"))
        {
            Debug.Log("Escape pressed");
            
            if (PauseMenu.isPaused)
            {
                PauseMenu.ResumeGame();
            }
            else
            {
                PauseMenu.PauseGame();
            }
        }
    }
}
