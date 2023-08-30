using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet_despawn : MonoBehaviour
{
	//Set from inspector
	[SerializeField]
	float seconds = 10;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, seconds);
    }
   
}
