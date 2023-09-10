using System.Collections;
using System.Collections.Generic;

// The commented code below causes build errors (find alternative may to import UnityEditor.Animations)
//using UnityEditor.Animations;


using UnityEngine;
using UnityEngine.UI;

public class AutoEnemyCounter : MonoBehaviour
{
    GameObject[] enemies;
    //public Text enemyCountText;

    private int totalEnemies = 0;
    void Start()
    {
        // To Find How Many Enemies are in the Level
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

        if (enemyDeadCount >= totalEnemies)
        {
            Debug.Log("All enemies dead! You win!");

            gameObject.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, Camera.main.nearClipPlane + 1));

            gameObject.GetComponent<TextMesh>().text = "You Win!";
        }
        else
        {
            gameObject.GetComponent<TextMesh>().text = "";
        }

        Debug.Log("Enemies Left : " + enemies.Length);
        //enemyCountText.text = "Enemies Left : " + enemies.Length.ToString();
    }
}
