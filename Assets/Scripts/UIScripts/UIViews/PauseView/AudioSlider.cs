using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioSlider : MonoBehaviour
{
    private Slider slider;
    [SerializeField] private List<AudioSource> audioSourceList;
    private List<float> maxVolumes;

    private void Start()
    {
        slider = transform.GetComponent<Slider>();
        SetMaxVolume();
        
    }
    public void SetMaxVolume()
    {
        maxVolumes = new List<float>();
        foreach (AudioSource source in audioSourceList)
        {
            float maxVal = source.volume / slider.value;
            maxVolumes.Add(maxVal);
        }
    }
    public void ChangeVolume()
    {
        for (int i = 0; i < audioSourceList.Count; i++)
        {
            audioSourceList[i].volume = maxVolumes[i] * slider.value;
        }
    }    
}
