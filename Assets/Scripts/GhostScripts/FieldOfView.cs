using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{
    public float Radius;
    public float Angle;

    public Transform GhostHead;
    public GameObject Player;

    [SerializeField] private LayerMask targetMask;
    [SerializeField] private LayerMask obstructionMask;

    public Vector3 LastPlayerSeenPosition;
    public bool PlayerSeen = false;

    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
    }
    private void Update()
    {
        FieldOfViewCheck();
    }

    private void FieldOfViewCheck()
    {
        Collider[] rangeCheck = Physics.OverlapSphere(GhostHead.position, Radius, targetMask);

        if (rangeCheck.Length != 0)
        {
            Transform target = rangeCheck[0].transform;
            Vector3 direction = (target.position - GhostHead.position).normalized;

            if (Vector3.Angle(transform.forward, direction) < Angle / 2)
            {
                float distance = Vector3.Distance(GhostHead.position, target.position);
                if (!Physics.Raycast(transform.position, direction, distance, obstructionMask))
                {
                    PlayerSeen = true;
                    LastPlayerSeenPosition = Player.transform.position;
                }
                else PlayerSeen = false;
            }
        }
        else if (PlayerSeen) PlayerSeen = false;
    }


}
