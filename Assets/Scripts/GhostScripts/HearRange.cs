using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HearRange : MonoBehaviour
{
    public float radius;

    private int soundMakerLayers;

    private void Start()
    {
        GameEvents.current.onSoundMake += CheckSoundInRange;
    }

    private void Update()
    {

    }
    private void CheckSoundInRange()
    {
        Debug.Log("soundcheck");
    }
}
