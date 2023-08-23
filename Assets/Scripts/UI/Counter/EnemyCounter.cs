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
        enemyDisplay.text = "Enemies Left: " + enemy.ToString();
    }

     public int getEnemy()
    {
        return enemy;
    }

    public void setEnemy(int NewEnemy)
    {
        enemy = NewEnemy;
    }
}
