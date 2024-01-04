
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HearRange : MonoBehaviour
{
    public float Radius = 10f;
    public Vector3 LastHearedSoundPosition;
    public bool SoundHeard = false;

    private float soundResetDistance = 2f;

    private void Start()
    {
        EventSoundMaker.SoundMakerTransform += CheckSoundInRange;
        //EventSoundMaker[] soundMakers = FindObjectsOfType<EventSoundMaker>();
        //foreach (EventSoundMaker soundMaker in soundMakers)
        //{
        //    soundMaker.SoundMakerTransform += CheckSoundInRange;
        //}
    }
    private void Update()
    {
        ResetSoundHeard();
    }
    private void CheckSoundInRange(Transform soundMakerTransform)
    {
      if(Vector3.Distance(transform.position, soundMakerTransform.position) <= Radius)
        {
            LastHearedSoundPosition = soundMakerTransform.position;
            SoundHeard = true;
        }
    }
    private void ResetSoundHeard()
    {
        if(Vector3.Distance(transform.position, LastHearedSoundPosition) <= soundResetDistance) 
        {
            SoundHeard = false;
        }
    }
}
