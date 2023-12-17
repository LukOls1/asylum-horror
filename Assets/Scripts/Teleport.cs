using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    public Vector3 teleportTo;
    public Quaternion rotateTo;
    public void TeleportCharacter()
    {
        Debug.Log("teleport");
        transform.position = teleportTo;
        transform.rotation = rotateTo;
    }
}
