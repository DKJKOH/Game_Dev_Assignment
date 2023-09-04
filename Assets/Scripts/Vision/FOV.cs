using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FOV : MonoBehaviour
{
    // View radius of player
    public float viewRadius;

    // Player's vision cone angle
    [Range(0,360)]
    public float viewAngle;

    // Enemy and obstacle masks
    public LayerMask targetMask;
    public LayerMask ObstacleMask;

    // Lists which contains current visible targets from current object
    [HideInInspector]
    public List<Transform> targetsVisible = new List<Transform>();

    private void Start()
    {
        // Starts the find targets with delay
        StartCoroutine("FindTargets_Delay", .2f);
    }
    /* Function which finds targets (with delay) */
    IEnumerator FindTargets_Delay(float delay)
    {
        while(true)
        {
            // Wait for few seconds before continuing with code after this line
            yield return new WaitForSeconds(delay);

            findVisibleTargets();
        }
    }


    /* Function which finds targets in current object's view cone */
    void findVisibleTargets()
    {
        // Clears the list of visible targets
        targetsVisible.Clear();

        // Basically detects if the enemy is within the player CIRCLE (Not the cone)
        //Collider[] targetsWithinVision = Physics.OverlapSphere(transform.position, viewRadius, targetMask);

        // A 2D circle checking if there circle overlaps with any rigidbody2D
        Collider2D[] targetsWithinVision = Physics2D.OverlapCircleAll(transform.position, viewRadius, targetMask);

        for (int i = 0; i < targetsWithinVision.Length; ++i)
        {
            // Retrieve target details
            Transform target = targetsWithinVision[i].transform;

            // Retrieve direction to target
            Vector3 directionToTarget = (target.position - transform.position).normalized;

            //Debug.Log(Vector3.Angle(transform.up, directionToTarget));

            // If target is within the view angle
            if (Vector3.Angle(transform.up, directionToTarget) < viewAngle / 2)
            {
                // Retrieve distance from current object to target
                float distanceToTarget = Vector3.Distance(transform.position, target.position);

                //// Raycast to check if there are obstacles from player to target
                //if (!Physics.Raycast(new Vector3(transform.position.x, transform.position.y, 0), directionToTarget, distanceToTarget, ObstacleMask))
                //{
                //    // Add the current target into the list
                //    targetsVisible.Add(target);

                //    // If there are no obstacles to target

                //    // Insert code here
                //}

                // Raycast to check if there are obstacles from player to target
                if (!Physics2D.Raycast(transform.position, directionToTarget, distanceToTarget, ObstacleMask))
                {
                    // Add the current target into the list
                    targetsVisible.Add(target);

                    // If there are no obstacles to target

                    // Insert code here
                }
            }
        }
    }


    public Vector3 DirectionFromAngle(float angleDegrees, bool isAngleGlobal)
    {
        // Convert angles to local angles instead
        if (!isAngleGlobal)
        {
            // This portion rotates the arc lines towards the mouse position
            angleDegrees -= transform.eulerAngles.z;
        }

        return new Vector3(Mathf.Sin(angleDegrees * Mathf.Deg2Rad), Mathf.Cos(angleDegrees * Mathf.Deg2Rad),0);
    }
}
