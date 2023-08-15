using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraFollow : MonoBehaviour
{
    // Allows user to select which target the camera is looking at
    [SerializeField] 
    Transform target;

    // This portion allows user to offset the camera manually
    [SerializeField] 
    Vector3 offset = new Vector3(0f, 0f, -10f);

    // Distance between target and camera
    private double distance;

    // How fast the camera follows the player model
    public float cameraSpeed = 0.1f;

    // Stores the camera's actual target position
    private Vector3 cameraTargetPosition;

    private void Update()
    {
        // Calculate distance between camera and target
        distance = Vector3.Distance(transform.position, target.position);
    }

    private void LateUpdate()
    {
            // Calculate the desired camera position
            cameraTargetPosition = target.position + offset;

            // Calculate the new position of the camera which is going towards the player's position
            Vector3 smoothedCameraPosition = Vector3.Lerp(transform.position, cameraTargetPosition, cameraSpeed * Time.deltaTime);

            // Transform the camera smoothly towards target position
            transform.position = smoothedCameraPosition;
    }
}
