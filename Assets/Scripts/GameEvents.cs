using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEvents : MonoBehaviour
{
    public static GameEvents current;

    private void Awake()
    {
        if (current == null)
        {
            current = this;
        }
        else Destroy(gameObject);
    }

    public event Action onSoundMake;
    public void SoundTrigger()
    {
        if(onSoundMake != null)
        {
            onSoundMake();
        }
    }
}
