using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet_movement : MonoBehaviour
{

	[SerializeField]
	float force;

	Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
    	// calling rigidbody2d
        rb = GetComponent<Rigidbody2D>();

        // Adding bullet force
        rb.AddForce(transform.up * force * Time.deltaTime, ForceMode2D.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
    	
    }
}
