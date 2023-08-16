using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet_spawn : MonoBehaviour
{
    public GameObject bullet;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    	//upon left click
        if(Input.GetButtonDown("Fire1"))
        {
        	//Create bullet object
        	Instantiate(bullet, transform.position, transform.rotation);
        }


        //upon hitting enemy/wall


        // Bullet object Disappear

    }
}
