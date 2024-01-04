using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventSoundMaker : MonoBehaviour
{
    public delegate void SoundMakerHandler(Transform objectTransform);
    public static event SoundMakerHandler SoundMakerTransform;

    public void SendSoundMakerTransform(Transform objectTransform)
    {
        SoundMakerTransform(objectTransform);
    }
}
