using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;

public class Laser_Pointer : MonoBehaviour
{
    // Renders Line
    private LineRenderer lineRenderer;

    // Stores cursor's mouse position in world value
    private Vector3 mouseWorldPosition;

    // Make reference to camera
    Camera cam;

    // Set your laser distance here
    [SerializeField]
    float laserDist = 10;

    // Attach lazer object here
    [SerializeField]
    Transform laserObject;




    // Start is called before the first frame update
    void Start()
    {
        // Retrieve line render component in object
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.enabled = true;
        lineRenderer.useWorldSpace = true;

        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        //---------------------------------- Retrieve mouse pos world value -------------------------------------------------------//

        // Retreive pixel position of mouse
        Vector3 mousePos = Input.mousePosition;

        // Increase z value of mouse pos to be above plane so that ScreenToWorldPoint works
        mousePos.z = cam.nearClipPlane;

        // Convert mouse pixel position into world positions
        mouseWorldPosition = cam.ScreenToWorldPoint(mousePos);


        //---------------------------------- Draw Laser -------------------------------------------------------//

        Vector2 laserObjectPos = laserObject.position;
        Vector2 laserObjectDir = laserObject.up;

        // Generate ray from laser object
        RaycastHit2D rayHit = Physics2D.Raycast(laserObjectPos, laserObjectDir, laserDist);


        // Update the Line Renderer's end position to the hit point
        lineRenderer.SetPosition(0, laserObjectPos);

        // If raycast does not hit anything
        if (rayHit.collider == null)
        {
            // Default to laserDist value from object
            lineRenderer.SetPosition(1, laserObjectPos + laserObjectDir * laserDist);
        }
        // If raycast hits wall
        else if (rayHit.collider.tag == "Wall")
        {
            print(rayHit.collider.tag);
            // Update the Line Renderer's end position to the hit point
            lineRenderer.SetPosition(1, rayHit.point);
        }
    }
}
