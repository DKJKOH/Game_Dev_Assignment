using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Death : MonoBehaviour
{
    private GameObject hand;

    // Enemy Death Sounds here!
    private AudioSource audioSource;

    [SerializeField]
    public AudioClip enemy_uhh_dead_sound;

    public void Enemy_uhh_dead_sound()
    {
        audioSource.PlayOneShot(enemy_uhh_dead_sound);
    }

    // Start is called before the first frame update
    void Start()
    {
        hand = transform.GetChild(0).gameObject;

        audioSource = gameObject.GetComponent<AudioSource>();   
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void removeEnemy()
    {
        // If enemy is holding gun
        if (hand.transform.childCount > 0)
        {
            // Remove weapon from hand
            GameObject weaponHeld = hand.transform.GetChild(0).gameObject;
            weaponHeld.transform.SetParent(null);

            // Retrieve gun's rigidbody
            Rigidbody2D weaponRigidbody = weaponHeld.GetComponent<Rigidbody2D>();

            // Set gun to be dynamic (can kick around)
            weaponHeld.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;

            // Remove all gun constraints (cannot transform, only can rotate)
            weaponRigidbody.constraints = RigidbodyConstraints2D.None;
        }


        

        // Disable box collider for enemy
        gameObject.GetComponent<BoxCollider2D>().enabled = false;

        // Freeze the sprite
        gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
    }

}
