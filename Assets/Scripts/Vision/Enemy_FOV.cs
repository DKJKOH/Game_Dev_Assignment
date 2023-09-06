using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;


public class Enemy_FOV : MonoBehaviour
{

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

    public float meshResolution;

    public MeshFilter mesh_view_filter;
    Mesh mesh_view;

    private void Start()
    {
        mesh_view = new Mesh();
        mesh_view.name = "Mesh View";

        // Assign viewmesh to viewmeshfilter
        mesh_view_filter.mesh = mesh_view;

        // Starts the find targets with delay
        StartCoroutine("FindTargets_Delay", .2f);
    }

    private void LateUpdate()
    {
        // Draw rays from object's field of view
        drawFOV();
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

                    // If there are no obstacles to target

                    // Insert code here
                }
            }
        }
    }
    /* Actually draws the player's field of view */
    void drawFOV()
    {
        // Think of it as ray count 
        int countStep = Mathf.RoundToInt(viewAngle * meshResolution);

        // Calculate angle between the rays from vision cone
        float stepAngleSize = viewAngle / countStep;

        // Stores informations on all rays emitted
        List<Vector3> viewPoints = new List<Vector3>();

        for (int i = 0; i <= countStep; ++i)
        {
            // Get angle which the ray will be cast (About the z axis and facing towards positive y direction)
            float angle = -transform.eulerAngles.z - viewAngle / 2 + stepAngleSize * i;

            // Information on current ray cast
            ViewCastInformation newViewCast = ViewCast(angle);

            // Adds current ray information into list
            viewPoints.Add(newViewCast.point);

            // Debug, draw line
            //Debug.DrawLine(transform.position, transform.position + DirectionFromAngle(angle, true) * viewRadius, Color.green);
        }

        // Number of vertexes for all triangles
        int vertexCount = viewPoints.Count + 1;

        // Store information on vertices
        Vector3[] vertices = new Vector3[vertexCount];

        // Get total number of triangles
        int[] triangles = new int[(vertexCount - 2) * 3];

        // Set the first vertice to 0
        vertices[0] = Vector3.zero;

        // Loop through the vertices and save the triangle information
        for (int i = 0; i < vertexCount - 1; i++)
        {
            // Set vertice (i + 1) so that it does not override the origin point
            vertices[i + 1] = transform.InverseTransformPoint(viewPoints[i]);
            if (i < vertexCount - 2)
            {
                // Set first vertex of each triangle
                triangles[i * 3] = 0;

                // Set second vertex of each triangle
                triangles[i * 3 + 1] = i + 1;

                // Set last vertex of each triangle
                triangles[i * 3 + 2] = i + 2;
            }

        }
        // Clear the view mesh (if used before)
        mesh_view.Clear();

        // Set the viewmash vertices and triangles
        mesh_view.vertices = vertices;
        mesh_view.triangles = triangles;

        mesh_view.RecalculateNormals();
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

    ViewCastInformation ViewCast(float globalAngle)
    {
        Vector3 direction = DirectionFromAngle(globalAngle, true);
        RaycastHit2D hit;


        hit = Physics2D.Raycast(transform.position, direction, viewRadius, ObstacleMask);

        // If 2d ray hits the wall
        if (hit.collider != null && hit.collider.tag == "Wall")
        {
            // Cast line until it hits the object
            return new ViewCastInformation(true, hit.point, hit.distance, globalAngle);
        }
        else
        {
            // Cast the line naturally
            return new ViewCastInformation(false, transform.position + direction * viewRadius, viewRadius, globalAngle);
        }
    }


    public struct ViewCastInformation
    {
        // Did ray hit something?
        public bool hit;
        // Where did the ray hit
        public Vector3 point;
        // Distance from origin to hit ray
        public float distance;
        // Angle from origin to hit
        public float angle;

        // Set up constructor for view cast information
        public ViewCastInformation(bool _hit, Vector3 _point, float _distance, float _angle)
        {
            // Assign the values set by user
            hit = _hit;
            point = _point;
            distance = _distance;
            angle = _angle;
        }
    }
}
