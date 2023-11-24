using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    //Player audio sources

    public AudioSource musicSource;
    public AudioSource effectsSource;
    public AudioSource dialogueSource;

    //Ghost audio sources

    //Dialogues clips

    public AudioClip dialogueOne;
    public AudioClip dialogueTwo;
    public AudioClip dialogueThree;
    public AudioClip dialogueFour;
    public AudioClip dialogueFive;


    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlaySound(AudioSource source, AudioClip clip)
    {
        source.PlayOneShot(clip);
    }
}
