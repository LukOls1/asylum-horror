using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(FieldOfView))]

public class FieldOfViewEditor : Editor
{
    private void OnSceneGUI()
    {
        FieldOfView fov = (FieldOfView)target;
        Handles.color = Color.white;
        Handles.DrawWireArc(fov.GhostHead.position, Vector3.up, Vector3.forward, 360, fov.Radius);

        Vector3 viewAngle01 = DirectionFromAngle(fov.GhostHead.eulerAngles.y, -fov.Angle / 2);
        Vector3 viewAngle02 = DirectionFromAngle(fov.GhostHead.eulerAngles.y, fov.Angle / 2);

        Handles.color = Color.red;
        Handles.DrawLine(fov.GhostHead.position, fov.GhostHead.position + viewAngle01 * fov.Radius);
        Handles.DrawLine(fov.GhostHead.position, fov.GhostHead.position + viewAngle02 * fov.Radius);

        if(fov.PlayerSeen)
        {
            Handles.color = Color.green;
            Handles.DrawLine(fov.GhostHead.position, fov.Player.transform.position);
        }
    }

    private Vector3 DirectionFromAngle(float eulerY, float angleInDegrees)
    {
        angleInDegrees += eulerY;

        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }
}
