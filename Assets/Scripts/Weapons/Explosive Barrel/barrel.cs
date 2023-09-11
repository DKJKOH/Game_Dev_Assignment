using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class barrel : MonoBehaviour
{
    // Grenade sounds
    [HideInInspector]
    public AudioSource audioSource;
    [SerializeField]
    public AudioClip grenade_explode_sound;

    // Sound functions (Used in animation)
    public void barrel_explode_sound()
    {
        // Find audio listener
        AudioSource audioSource = gameObject.GetComponent<AudioSource>();
        // BOOM BOOM SOUND
        audioSource.PlayOneShot(grenade_explode_sound);
    }

    // Start is called before the first frame update
    void Start()
    {
        // Find audio listener
        audioSource = gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Done"))
        {
            // Destroy itself
            Destroy(gameObject);
        }
    }

}
