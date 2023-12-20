using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MasterAudioSlider : MonoBehaviour
{
    private Slider slider;
    void Start()
    {
        slider = transform.GetComponent<Slider>();
        slider.value = AudioListener.volume;
    }
    public void ChangeMasterVolume()
    {
        AudioListener.volume = slider.value;
    }
}
