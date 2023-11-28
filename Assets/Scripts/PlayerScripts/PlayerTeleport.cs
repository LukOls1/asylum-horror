using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTeleport : MonoBehaviour
{
    public Vector3 teleportTo;
    public Quaternion rotateTo;
    public void Teleport()
    {
        transform.position = teleportTo;
        transform.rotation = rotateTo;
    }
}
