using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Rotation : MonoBehaviour
{
    // Initalize rigidbody, camera, ray, hitpoint
    Rigidbody rb;
    Camera cam;
    Ray mouseRay;
    Vector3 hitPoint;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        mouseRay = cam.ScreenPointToRay(Input.mousePosition);
        print(Input.mousePosition);
        RaycastHit hitInfo;

        // if raycast from mouse hits anything (dist 100f), save details on hitInfo
        if (Physics.Raycast(mouseRay, out hitInfo, 100f))
        {
            // Save the point of impact on hitPoint
            hitPoint = hitInfo.point;
        }


        // Take x and z values from hitPoint while take y value from player
        // Prevents player from looking up and down into ground
        Vector3 lookTarget = new Vector3(hitPoint.x, transform.position.y, hitPoint.z);

        // Player look at lookTarget
        transform.LookAt(lookTarget);

        // DEBUG DEBUG DEBUG
        Debug.DrawRay(cam.transform.position, lookTarget - cam.transform.position, Color.magenta);
    }
}
