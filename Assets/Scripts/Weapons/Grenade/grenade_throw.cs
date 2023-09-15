using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class grenade_throw : MonoBehaviour
{
    [SerializeField]
    float throw_force = 10f; // Force with which the grenade is thrown

    private Rigidbody2D grenade;
    Animator animator;

    public float start_time;
    private bool isStarted;

    // Time before grenade explodes
    [SerializeField]
    float explosion_time = 2;

    // Grenade sounds
    [HideInInspector]
    public AudioSource audioSource;
    [SerializeField]
    public AudioClip grenade_explode_sound;

    // Sound functions (Used in animation)
    public void Grenade_explode_sound()
    {

        // Find audio listener
        AudioSource audioSource = gameObject.GetComponent<AudioSource>();
        // BOOM BOOM SOUND
        audioSource.PlayOneShot(grenade_explode_sound);
    }

    // This function removes grenade, will be executed on end of grenade explosion animation, can be found under grenade_ani in animations
    void grenade_remove()
    {
            // Destroy the grenade game object
            Destroy(gameObject);
    }

    void Start()
    {
        grenade = GetComponent<Rigidbody2D>();
        // Initially, grenade is not affected by physics 
        grenade.isKinematic = true;
        animator = GetComponent<Animator>();
        // Grenade hasn't been thrown yet
        isStarted = false;


        if (transform.parent != null)
        {
            grenade.transform.rotation = transform.parent.rotation;
        }

        

        // Find audio listener
        audioSource = gameObject.GetComponent<AudioSource>();
    }

    void Update()
    {
        if (!animator.GetCurrentAnimatorStateInfo(0).IsName("idle"))
        {
            gameObject.GetComponent<BoxCollider2D>().enabled = false;

            Destroy(gameObject, grenade_explode_sound.length);
        }


        if (Input.GetMouseButtonDown(0) && transform.parent != null)
        {
            // Grenade throwing process started
            isStarted = true;
            start_time = Time.time;

            grenade.isKinematic = false; // Now, physics affect the grenade

            // Get's hand forward direction (y axis) so that we can orient grenade throw in the correct direction
            grenade.transform.up = transform.parent.up;

            transform.parent = null; // Detach grenade from its parent

            grenade.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;

            // Apply force to simulate the throw
            grenade.AddForce(transform.up * throw_force * Time.deltaTime, ForceMode2D.Impulse);
        }

        if (Time.time - start_time >= explosion_time && isStarted == true)
        {
            // Stop grenade's movement
            grenade.velocity = new Vector3(0, 0, 0);

            if (gameObject.transform.GetChildCount() > 0)
            {
                gameObject.transform.GetChild(0).gameObject.SetActive(true);
            }

            

            // Trigger explosion animation
            animator.SetTrigger("isExplode");

            gameObject.GetComponent<BoxCollider2D>().enabled = true;
        }
    }
}
