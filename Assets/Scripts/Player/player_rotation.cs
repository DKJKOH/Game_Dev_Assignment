using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_rotation : MonoBehaviour
{
    // Create private rigid body for player
    private Rigidbody2D player_rb;

    // Start is called before the first frame update
    void Start()
    {
        // Get player's rigid body
        player_rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // Get current mouse position 
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // Calculate the direction from playermodel to mouse
        Vector3 directionToMouse = mousePos - transform.position;

        // Calculate the angle to rotate the playermodel towards the mouse
        float rotationAngle = Mathf.Atan2(directionToMouse.y, directionToMouse.x) * Mathf.Rad2Deg - 90f;

        // Rotate player
        transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, rotationAngle));
    }
}
