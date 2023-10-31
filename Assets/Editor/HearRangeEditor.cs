using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof (HearRange))]
public class HearRangeEditor : Editor
{
    private void OnSceneGUI()
    {
        HearRange hr = (HearRange)target;
        Handles.color = Color.blue;
        Handles.DrawWireArc(hr.transform.position ,Vector3.up, Vector3.forward, 360, hr.Radius);
    }
}
