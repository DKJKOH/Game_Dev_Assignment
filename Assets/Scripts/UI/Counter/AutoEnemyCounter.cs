using System.Collections;
using System.Collections.Generic;
using TMPro;


// The commented code below causes build errors (find alternative may to import UnityEditor.Animations)
//using UnityEditor.Animations;


using UnityEngine;
using UnityEngine.UI;

public class AutoEnemyCounter : MonoBehaviour
{
    GameObject[] enemies;
    //public Text enemyCountText;
    //[SerializeField]
    //public GameObject ButtonToEnable;

    private int totalEnemies = 0;
    void Start()
    {
        // To Find How Many Enemies are in the Level
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        totalEnemies = enemies.Length;
        //ButtonToEnable.SetActive(false);

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
            enemiesLeft = totalEnemies - enemyDeadCount;

            gameObject.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, Camera.main.nearClipPlane + 1));

            gameObject.GetComponent<TextMeshProUGUI>().text = "All Enemies Dead!";
            // All enemies are dead, enable the button and allow scene transition
            gameObject.SetActive(true);

        }
        else
        {
            gameObject.GetComponent<TextMeshProUGUI>().text = "Enemies left: " + enemiesLeft;
            gameObject.SetActive(true);
        }

    }
}
