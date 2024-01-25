using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using static UnityEngine.GraphicsBuffer;


public class Enemy_FOV : MonoBehaviour
{
    // Current weapon held by enemy
    public GameObject gun;

    public Pistol pistol_controller;

    // View radius of player
    public float viewRadius;

    // What kind of bullet would spawn
    [SerializeField]
    public GameObject bullet;

    // Player's vision cone angle
    [Range(0, 360)]
    public float viewAngle;

    // Enemy and obstacle masks
    public LayerMask targetMask;
    public LayerMask ObstacleMask;

    private Transform transform_target;
    private Vector3 directionToTarget;
    private Vector3 weaponDirectionToTarget;

    private Vector3 last_seen_position;
    private bool going_to_position;


    // Lists which contains current visible targets from current object
    [HideInInspector]
    public List<Transform> targetsVisible = new List<Transform>();


    // Shooting stuff
    public float timeBetweeenShots = 1;
    // Stores the time where the last shot was taken
    private float lastShotTime;

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
        // STUFF TO SOLVE: ENEMY STOPS FOLLOWING PLAYER IF THE WEAPON GETS STOLEN
        // Retrieve distance from current object to target
        float distanceToTarget = Vector3.Distance(transform.position, last_seen_position);

        // If enemies current position is at the last seen position, do not move the enemy
        if (last_seen_position.x != transform.position.x && last_seen_position.y != transform.position.y && going_to_position)
        {
            // Move enemy to player's last seen position
            directionToTarget = (last_seen_position - transform.position).normalized;

            // Calculate the angle to rotate the enemy towards the seen player
            float rotationAngle = Mathf.Atan2(directionToTarget.y, directionToTarget.x) * Mathf.Rad2Deg - 90f;

            // Rotate Enemy body
            transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, rotationAngle));


            // Move the player towards the target

            Vector3 force = directionToTarget * 1 * Time.timeScale;

            transform.gameObject.GetComponent<Rigidbody2D>().AddForce(force);

            GameObject hand = transform.GetChild(0).gameObject;

            // If enemy is holding a gun (to prepare if it gets destroyed)
            if (hand.transform.childCount != 0)
            {
                //Get weapon direction using weapon last seen direciton
                weaponDirectionToTarget = (last_seen_position - gun.transform.position).normalized;

                // Calculate angle on how much weapon needs to rotate
                float WeaponRotationAngle = Mathf.Atan2(weaponDirectionToTarget.y, weaponDirectionToTarget.x) * Mathf.Rad2Deg;

                // Rotate enemy gun
                gun.transform.rotation = Quaternion.Euler(new Vector3(0f, 0, WeaponRotationAngle));
            }
            else
            {
                Debug.Log("Not holding gun");
            }
        }
        // If enemy is at player's last seen position, do not move the enemy
        if( distanceToTarget < 1)
        {
            
            // Disable movement
            going_to_position = false;
        }


        if (targetsVisible.Count > 0)
        {
            for (int i = 0; i < targetsVisible.Count; i++)
            {
                // Get the player's current position
                transform_target = targetsVisible[i];

                // Save last seen position
                last_seen_position = transform_target.position;

                going_to_position = true;


                // Retrieve the normalized vector (Direction) from enemy to player no
                directionToTarget = (transform_target.position - transform.position).normalized;
                

                // Calculate the angle to rotate the enemy towards the seen player
                float rotationAngle = Mathf.Atan2(directionToTarget.y, directionToTarget.x) * Mathf.Rad2Deg - 90f;

                

                // Rotate Enemy
                transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, rotationAngle));



                // Move the enemy
                // Move the player towards the target
                transform.position += directionToTarget * 1 * Time.deltaTime;

                GameObject hand = transform.GetChild(0).gameObject;

                // If enemy has weapon
                if (hand.transform.childCount != 0)
                {
                    weaponDirectionToTarget = (transform_target.position - gun.transform.position).normalized;

                    float WeaponRotationAngle = Mathf.Atan2(weaponDirectionToTarget.y, weaponDirectionToTarget.x) * Mathf.Rad2Deg;



                    // Rotate the gun
                    gun.transform.rotation = Quaternion.Euler(new Vector3(0f, 0, WeaponRotationAngle));

                    // Trigger gun shoot (Fire weapon)
                    GameObject bullet_spawner = transform.Find("Hand-Enemy/Pistol/Weapon_Object/Bullet_Spawner").gameObject;

                    if (Time.time - lastShotTime >= timeBetweeenShots)
                    {
                        // Create bullet object
                        Instantiate(bullet, bullet_spawner.transform.position, bullet_spawner.transform.rotation);
                        // Animate firing
                        pistol_controller.GetComponent<Animator>().SetTrigger("shoot");

                        // Save the last shot time
                        lastShotTime = Time.time;
                    }
                }
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
