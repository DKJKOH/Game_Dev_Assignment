using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;

public class Laser_Pointer : MonoBehaviour
{
    // Attach laser object here
    [SerializeField]
    Transform laserObject;

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

        //Vector2 laserObjectDir = laserObject.up;

        // Retrieve mouse position (pixel coordinates)
        Vector2 mousePosition = Input.mousePosition;
        // Convert pixel coordinates into world coordinates
        Vector3 mousePositionWorldCoordinates = mainCamera.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, mainCamera.transform.position.y - laserObject.position.y));

        Vector3 laserDirection = mousePositionWorldCoordinates - laserObject.position;



        // Create raycast from laser object in the forward direction of the laser object
        //RaycastHit2D rayHit = Physics2D.Raycast(laserObjectPos, laserObjectDir, laserDist);


        // Debug show ray
        //Debug.DrawRay(laserObjectPos, laserObjectDir);

        // Stores details on what the ray hit
        RaycastHit rayHit;

        // Debug, shows ray
        Debug.DrawRay(laserObject.position, laserDirection);

        // Set starting position of laser (which is at weapon)
        lineRenderer.SetPosition(0, laserObject.position);

        // If ray hits something
        if (Physics.Raycast(laserObject.position, laserDirection, out rayHit, laserDist))
        {
            Debug.Log("Apple");
            // Set the laser end point
            lineRenderer.SetPosition(1, rayHit.point);
        }
        else
        {
            lineRenderer.SetPosition(1, laserObject.position + laserDirection.normalized * laserDist);
        }

        // Issue, you need to provide the direction of the ray as the second parameter, not the end point

        //    if (rayHit.collider != null)
        //    {
        //        // Update the Line Renderer's end position to the hit point
        //        lineRenderer.SetPosition(0, laserObjectPos);

        //        // If raycast hits wall or person
        //        if (rayHit.collider.tag == "Wall" || rayHit.collider.tag == "Person")
        //        {
        //            // Update the Line Renderer's end position to the hit point
        //            lineRenderer.SetPosition(1, rayHit.point);
        //        }
        //        else
        //        {
        //            // Update the Line Renderer's end position to the hit point
        //            lineRenderer.SetPosition(1, rayHit.point);
        //        }
        //    }
        //    else
        //    {
        //        // Update the Line Renderer's start position at the gun's coordinates
        //        lineRenderer.SetPosition(0, laserObjectPos);

        //        // Default to laserDist value from object
        //        lineRenderer.SetPosition(1, laserObjectPos + laserObjectDir * laserDist);
        //    }
    }
}
