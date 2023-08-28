using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;

public class Laser_Pointer : MonoBehaviour
{
    // ISSUE, THE LASER POINTS TOWARDS CURSOR TOO MUCH, i WANT IT TO POINT STRAIGHT AHEAD

    // Attach laser object here
    [SerializeField]
    Transform laserObject;

    // Attach Gun object (Parent of laser) here
    [SerializeField]
    Transform GunObject;

    // Set your laser distance here
    [SerializeField]
    int laserDist = 10;

    // Set camera which follows user here
    [SerializeField]
    Camera mainCamera;

    // Renders Line
    private LineRenderer lineRenderer;

    // Start is called before the first frame update
    void Start()
    {
        // Retrieve line render component in object
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.enabled = true;
        lineRenderer.useWorldSpace = true;
    }

    // Update is called once per frame
    void Update()
    {
        //---------------------------------- Draw Laser -------------------------------------------------------//
        // Get laser's position and the forward direction (y direction in our case)
        Vector2 laserObjectPos = laserObject.position;

        // Retrieve mouse position (pixel coordinates)
        Vector2 mousePosition = Input.mousePosition;
        // Convert pixel coordinates into world coordinates
        Vector3 mousePositionWorldCoordinates = mainCamera.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, mainCamera.transform.position.y - laserObject.position.y));

        // Ensures that the mouse position z position is the same as the laser object z position
        mousePositionWorldCoordinates.z = laserObject.position.z;

        // Calculate laser's direction
        //Vector3 laserDirection = mousePositionWorldCoordinates - laserObject.position;

        Vector3 laserDirection = laserObject.transform.up;

        Vector3 laserDirectionNormalized = laserDirection.normalized;

        // Set laser's starting point
        lineRenderer.SetPosition(0, laserObject.position);

        RaycastHit2D hit = Physics2D.Raycast(laserObjectPos, laserDirection, laserDist);
        // If laser hits something
        if (hit.collider != null && hit.collider.tag != "Bullet")
        {
            // Set the end position of laser to be the location of the hit place
            lineRenderer.SetPosition(1, hit.point);
        }
        else
        {


            // Default to laserDist value from object
            //lineRenderer.SetPosition(1, laserObject.position + laserDirection.normalized * laserDist);


            // Test Test
            lineRenderer.SetPosition(1, laserObject.position + laserDirectionNormalized * laserDist);
        }
    }
}
