using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;


public class Enemy_FOV : MonoBehaviour
{
    public GameObject gun;

    // View radius of player
    public float viewRadius;

    // Player's vision cone angle
    [Range(0, 360)]
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
        while (true)
        {
            // Wait for few seconds before continuing with code after this line
            yield return new WaitForSeconds(delay);

            findVisibleTargets();
        }
    }

    private void Update()
    {
        if (targetsVisible.Count > 0)
        {
            for (int i = 0; i < targetsVisible.Count; i++)
            {
                // Get the player's current position
                Transform transform_target = targetsVisible[i];

                // Retrieve the normalized vector (Direction) from enemy to player no
                Vector3 directionToTarget = (transform_target.position - transform.position).normalized;
                Vector3 weaponDirectionToTarget = (transform_target.position - gun.transform.position).normalized;


                // Calculate the angle to rotate the enemy towards the seen player
                float rotationAngle = Mathf.Atan2(directionToTarget.y, directionToTarget.x) * Mathf.Rad2Deg - 90f;

                float WeaponRotationAngle = Mathf.Atan2(weaponDirectionToTarget.y, weaponDirectionToTarget.x) * Mathf.Rad2Deg;

                // Rotate Enemy
                transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, rotationAngle));

                gun.transform.rotation = Quaternion.Euler(new Vector3(0f, 0, WeaponRotationAngle));

            }
        }
    }

    /* Function which finds targets in current object's view cone */
    void findVisibleTargets()
    {
        // Clears the list of visible targets
        targetsVisible.Clear();

        // Basically detects if the enemy is within the player CIRCLE (Not the cone)
        Collider2D[] targetsWithinVision = Physics2D.OverlapCircleAll(transform.position, viewRadius, targetMask);

        for (int i = 0; i < targetsWithinVision.Length; ++i)
        {
            // Retrieve target details
            Transform target = targetsWithinVision[i].transform;

            // Retrieve direction to target
            Vector3 directionToTarget = (target.position - transform.position).normalized;

            // If target is within the view angle
            if (Vector3.Angle(transform.up, directionToTarget) < viewAngle / 2)
            {
                // Retrieve distance from current object to target
                float distanceToTarget = Vector3.Distance(transform.position, target.position);

                // Raycast to check if there are obstacles from player to target
                if (!Physics2D.Raycast(transform.position, directionToTarget, distanceToTarget, ObstacleMask))
                {
                    // Add the current target into the list
                    targetsVisible.Add(target);
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

        return new Vector3(Mathf.Sin(angleDegrees * Mathf.Deg2Rad), Mathf.Cos(angleDegrees * Mathf.Deg2Rad), 0);
    }
}
