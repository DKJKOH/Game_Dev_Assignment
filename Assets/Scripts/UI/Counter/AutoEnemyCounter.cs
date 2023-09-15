using System.Collections;
using System.Collections.Generic;
using TMPro;


// The commented code below causes build errors (find alternative may to import UnityEditor.Animations)
//using UnityEditor.Animations;


using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AutoEnemyCounter : MonoBehaviour
{
    Scene scene;

    GameObject[] enemies;

    private int totalEnemies = 0;
    void Start()
    {
        // To Find How Many Enemies are in the Level
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        totalEnemies = enemies.Length;

        scene = SceneManager.GetActiveScene();

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
            gameObject.GetComponent<TextMeshProUGUI>().text = "All Enemies Dead!";

            if (scene.name != "TutorialLevel")
            {

                // Link the gameover scene 
                SceneManager.LoadScene("VictoryScreen");
            }


        }
        else
        {
            gameObject.GetComponent<TextMeshProUGUI>().text = "Enemies left: " + enemiesLeft;
            gameObject.SetActive(true);
        }

    }
}
