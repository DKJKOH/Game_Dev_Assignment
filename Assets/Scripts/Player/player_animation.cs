using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_animation : MonoBehaviour
{
    private Animator player_animator_controller;

    [SerializeField]
    public GameObject hand;

    // Start is called before the first frame update
    void Start()
    {
        // Retrieve player's player controller animator
        player_animator_controller = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        int horizontalInput = (int)Input.GetAxisRaw("Horizontal");
        int verticalInput = (int)Input.GetAxisRaw("Vertical");

        if (horizontalInput != 0 || verticalInput != 0)
        {
            
            player_animator_controller.SetBool("walking", true);
        }
        else
        {
           
            player_animator_controller.SetBool("walking", false);
        }
    }
}
