using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AutoEnemyCounter : MonoBehaviour
{
    GameObject[] enemies;
    public Text enemyCountText;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // To Find How Many Enemies are in the Level
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        // To Display Enemies Left
        enemyCountText.text = "Enemies Left : " + enemies.Length.ToString();
    }
}
