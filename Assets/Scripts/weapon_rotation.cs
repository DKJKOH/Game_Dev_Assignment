using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weapon_rotation : MonoBehaviour
{
    // Save the rotation value for future use
    private float yRotationValue;

    void Start()
    {
        // Save the user set rotation value
        yRotationValue = transform.rotation.eulerAngles.y;
    }

    // Update is called once per frame
    void Update()
    {
        // Get current mouse position 
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // Calculate the direction from playermodel to mouse
        Vector3 directionToMouse = mousePos - transform.position;

        // Calculate the angle to rotate the playermodel towards the mouse
        float rotationAngle = Mathf.Atan2(directionToMouse.y, directionToMouse.x) * Mathf.Rad2Deg;

        // Rotate weapon (tilt it according to inital set degrees)
        transform.rotation = Quaternion.Euler(0, yRotationValue, rotationAngle);
    }
}
