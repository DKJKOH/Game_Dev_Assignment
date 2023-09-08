using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyCounter : MonoBehaviour
{
    public int enemy;
    public Text enemyDisplay;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Display of enemies left
        enemyDisplay.text = "Enemies Left: " + enemy.ToString();
    }

    // Get the enemy amount to display
    public int getEnemy()
    {
        return enemy;
    }

    // Update the enemy amount
    public void setEnemy(int NewEnemy)
    {
        enemy = NewEnemy;
    }
}
