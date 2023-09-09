using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Death : MonoBehaviour
{
    private GameObject hand;


    // Start is called before the first frame update
    void Start()
    {
        hand = transform.GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void removeEnemy()
    {
        // Destroy themself (enemy)
        //Destroy(transform.gameObject);



        // Remove weapon from hand
        GameObject weaponHeld = hand.transform.GetChild(0).gameObject;
        weaponHeld.transform.SetParent(null);

        Rigidbody2D weaponRigidbody = weaponHeld.GetComponent<Rigidbody2D>();
        weaponHeld.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;

        weaponRigidbody.constraints = RigidbodyConstraints2D.None;

        // Disable box collider for enemy
        gameObject.GetComponent<BoxCollider2D>().enabled = false;

        gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
    }

}
