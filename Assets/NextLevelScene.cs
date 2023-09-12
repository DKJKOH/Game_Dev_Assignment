using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class NextLevelScene : MonoBehaviour
{
	 Scene scene;

	[SerializeField]
	GameObject NextLevel;

    private int totalEnemies = 0;
    GameObject[] enemies;

    // Start is called before the first frame update
    void Start()
    {
    	scene = SceneManager.GetActiveScene();
        NextLevel.SetActive(false);
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        totalEnemies = enemies.Length;

    }

    // Update is called once per frame
    void Update()
    {
    	 int enemyDeadCount = 0;
        // To Find How Many Enemies are in the Level
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        // To Display Enemies Left
        foreach (GameObject enemy in enemies)
        {
            if (enemy.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Dead"))
            {
                enemyDeadCount++;
            }
        }

        int enemiesLeft = totalEnemies - enemyDeadCount;

        if (enemyDeadCount >= totalEnemies)
        {
        	gameObject.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, Camera.main.nearClipPlane + 1));

        	if(scene.name == "Final Map v3 (New Save")
        	{
        		SceneManager.LoadScene("VictoryScreen");
        	}
        	else
        	{
        		NextLevel.SetActive(true); // Show the next Scene UI.
        	}
        }

    }

    public void NextScene()
    {
        // To load the next active stage
        SceneManager.LoadScene("Final Map v3 (New Save)");
        NextLevel.SetActive(false);
    }
}
