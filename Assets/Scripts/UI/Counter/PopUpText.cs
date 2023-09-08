using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PopUpText : MonoBehaviour
{
    private TextMeshProUGUI TextMeshPro;

    public GameObject GameObject;

    void Start()
    {
        // Assign the TextMeshPro component from the GameObject in the Inspector
        TextMeshPro = GameObject.GetComponent<TextMeshProUGUI>();
        TextMeshPro.enabled = false; // Initialize text as hidden

        //TextMeshPro.text = ""; // Initialize text as empty
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Person"))
        {
            Debug.Log("Entered Scene for text");
            TextMeshPro.enabled = true; // Show the text when entering the trigger zone
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Person"))
        {
            Debug.Log("Exited Scene for text");
            TextMeshPro.enabled = false; // Hide the text when exiting the trigger zone
        }
    }
}
