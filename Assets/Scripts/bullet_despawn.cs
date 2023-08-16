using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet_despawn : MonoBehaviour
{
	//Set from inspector
	[SerializeField]
	float seconds;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, seconds);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
