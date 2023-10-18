using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{
    [SerializeField] public float radius;
    [SerializeField] public float angle;

    private GameObject player;

    private LayerMask targetMask;
    private LayerMask obstructionMask;

    public bool playerSeen = false;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine(FOVRoutine());
    }

    private IEnumerator FOVRoutine()
    {
        WaitForSeconds wait = new WaitForSeconds(0.2f);

        while(true)
        {
            yield return wait;
            FieldOfViewCheck();
        }
    }

    private void FieldOfViewCheck()
    {
        Collider[] rangeCheck = Physics.OverlapSphere(transform.position, radius);

        if (rangeCheck.Length != 0)
        {
            Transform target = rangeCheck[0].transform;
            Vector3 direction = (target.position - transform.position).normalized;

            if (Vector3.Angle(transform.forward, direction) < angle / 2)
            {
                float distance = Vector3.Distance(transform.position, target.position);
                if (!Physics.Raycast(transform.position, direction, distance, obstructionMask))
                {
                    playerSeen = true;
                }
                else playerSeen = false;
            }
        }
        else if (playerSeen) playerSeen = false;
    }


}
