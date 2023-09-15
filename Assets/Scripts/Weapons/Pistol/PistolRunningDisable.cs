using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistolRunningDisable : MonoBehaviour
{
    public Animator animator;

    public GameObject gunToDisableObject;

    // Update is called once per frame
    void Update()
    {
        try
        {
            if (gameObject.transform.parent.name == "Hand")
            {
                // If gun is not being animated and user is running
                if (animator.GetCurrentAnimatorStateInfo(0).IsName("idle") && Input.GetButton("Shift"))
                {
                    // Issue, if game object is disabled, this script cannot be run
                    gunToDisableObject.SetActive(false);
                }

                // If user releases the shift button (Walk)
                if (Input.GetButtonUp("Shift"))
                {
                    // Issue, if game object is disabled, this script cannot be run
                    gunToDisableObject.SetActive(true);
                }
            }
        }
        catch
        {
        }
    }
}
