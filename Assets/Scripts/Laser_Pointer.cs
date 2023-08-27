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
    float laserDist = 10;

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
        Vector2 laserObjectDir = laserObject.up;

        // Create raycast from laser object in the forward direction of the laser object
        RaycastHit2D rayHit = Physics2D.Raycast(laserObjectPos, laserObjectDir, laserDist);

        // Update the Line Renderer's end position to the hit point
        lineRenderer.SetPosition(0, laserObjectPos);

        // This if statement is to bypass error "object reference not set to an instance of an object"
        if (rayHit.collider == null)
        {
            // Default to laserDist value from object
            lineRenderer.SetPosition(1, laserObjectPos + laserObjectDir * laserDist);
        }
        // If raycast hits wall
        else if (rayHit.collider.tag == "Wall" || rayHit.collider.tag == "Person")
        {
            // Update the Line Renderer's end position to the hit point
            lineRenderer.SetPosition(1, rayHit.point);
        }
        else
        {

            // Default to laserDist value from object
            lineRenderer.SetPosition(1, laserObjectPos + laserObjectDir * laserDist);
        }
    }
}
