using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
	[SerializeField]
	public float health;

	[SerializeField]
	public float maxHealth = 1;

	public GameObject gameOverUI;

	private bool isDead;
    // Start is called before the first frame update
    void Start()
    {
        health = 1;
    }

    // Update is called once per frame
    void Update()
    {
    	Debug.Log("Health: " + health);
        if(health <= 0 && !isDead)
        {
        	isDead = true;
        	gameOver();
        	Debug.Log("died");
        }
    }

    public void gameOver()
    {
    	gameOverUI.SetActive(true);
    }
}
