using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraControls : MonoBehaviour
{
    // Target object camera will look at
    public Transform target;

    // How fast camera moves 
    public float cameraSpeed;

    // Camera offset from target value
    public Vector2 offSetValue;

    private Vector3 offset;

    // Distacne between target and camera
    private double distance;

    // How far you want the target to go before camera starts to pan to target
    [SerializeField] double edgeDistance;



    // Start is called before the first frame update
    void Start()
    {
        // Maintain distance from scene during tracking
        offset = (Vector3)offSetValue;
        offset.y = transform.position.y - target.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        // Calculate distance between  camera and target
        distance = Vector3.Distance(transform.position, target.position);
    }
    void LateUpdate()
    {
        // If the target is too far away from camera
        if (distance > edgeDistance)
        {
            // Camera moves towards target
            transform.position = Vector3.MoveTowards(
                transform.position,
                target.position + offset,
                cameraSpeed * Time.deltaTime
            );
        }


    }
}
