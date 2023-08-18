using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet_movement : MonoBehaviour
{

	[SerializeField]
	float force = 1000;

	Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
    	// calling rigidbody2d
        rb = GetComponent<Rigidbody2D>();

        // Adding bullet force (In Y direction as it is 2D)
        rb.AddForce(transform.up * force * Time.deltaTime, ForceMode2D.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
    	
    }
}
