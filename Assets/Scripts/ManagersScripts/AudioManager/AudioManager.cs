using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("Player Audio Sources")]

    public AudioSource musicSource;
    public AudioSource effectsSource;
    public AudioSource dialogueSource;

    [Header("Ghost Audio Sources")]

    public AudioSource ghostSounds;

    [Header("Ghost Audio Clips")]

    public AudioClip ghostIdleSound;
    public AudioClip ghostAlertedSound;
    public AudioClip ghostChaseSound;

    [Header("Dialogue Clips")]

    public AudioClip dialogueOne;
    public AudioClip dialogueTwo;
    public AudioClip dialogueThree;
    public AudioClip dialogueFour;
    public AudioClip dialogueFive;
    public AudioClip dialogueAndGhost;

    [Header("Effects Clips")]

    public AudioClip paperSound;
    public AudioClip keypadButton;
    public AudioClip correctCode;
    public AudioClip wrongCode;

    public static event Action<int> EndOfClip;
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

    public void PlayDialogue(AudioClip clip, int tipsListIndex)
    {
        StopAllCoroutines();
        dialogueSource.Stop();
        dialogueSource.clip = clip;
        dialogueSource.Play();
        StartCoroutine(TriggerOnEnd(dialogueSource, tipsListIndex));
    }
    public void PlaySound(AudioSource source, AudioClip clip)
    {
        source.PlayOneShot(clip);
    }
    public void PlayLoopSound(AudioSource source, AudioClip clip)
    {
        source.clip = clip;
        source.Play();
    }
    private IEnumerator TriggerOnEnd(AudioSource source, int tipsListIndex)
    {
        yield return new WaitForSeconds(source.clip.length);
        EndOfClip?.Invoke(tipsListIndex);
    }
}
