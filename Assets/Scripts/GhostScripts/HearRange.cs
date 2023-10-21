
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HearRange : MonoBehaviour
{
    public float radius = 10f;
    public Vector3 lastHearedSoundPosition;
    public bool soundHeard = false;

    private void Start()
    {
        EventSoundMaker[] soundMakers = FindObjectsOfType<EventSoundMaker>();
        foreach (EventSoundMaker soundMaker in soundMakers)
        {
            soundMaker.SoundMakerTransform += CheckSoundInRange;
        }
    }
    private void CheckSoundInRange(Transform soundMakerTransform)
    {
      if(Vector3.Distance(transform.position, soundMakerTransform.position) <= radius)
        {
            lastHearedSoundPosition = soundMakerTransform.position;
            soundHeard = true;
        }
    }
}
