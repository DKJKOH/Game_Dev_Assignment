using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using static UnityEngine.GraphicsBuffer;

[CustomEditor(typeof(Enemy_FOV))]
public class Enemy_FOV_Editor : Editor
{
    private void OnSceneGUI()
    {
        Enemy_FOV fieldOfVision = (Enemy_FOV)target;

        // Draw the radius of fov
        Handles.color = Color.yellow;

        // Draws circle
        Handles.DrawWireArc(fieldOfVision.transform.position, Vector3.forward, Vector3.up, 360, fieldOfVision.viewRadius);

        // Player vision cone lines
        Vector3 viewCone1 = fieldOfVision.DirectionFromAngle(-fieldOfVision.viewAngle / 2, false);
        Vector3 viewCone2 = fieldOfVision.DirectionFromAngle(fieldOfVision.viewAngle / 2, false);
        Handles.DrawLine(fieldOfVision.transform.position, fieldOfVision.transform.position + viewCone1 * fieldOfVision.viewRadius);
        Handles.DrawLine(fieldOfVision.transform.position, fieldOfVision.transform.position + viewCone2 * fieldOfVision.viewRadius);

        // Change color line
        Handles.color = Color.red;
        // Iterate through the current targets seen by the object
        foreach (Transform seen_target in fieldOfVision.targetsVisible)
        {
            // Draw line from object to target
            Handles.DrawLine(fieldOfVision.transform.position, seen_target.position);
        }
    }
}
