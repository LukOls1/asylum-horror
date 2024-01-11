using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    [SerializeField] private Slider masterVolumeSlider;
    [SerializeField] private float fadeDuration = 5f;

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

    [Header("Music Clips")]

    public AudioClip alertMusic;
    public AudioClip chaseMusic;

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
        AudioListener.volume = masterVolumeSlider.value;
       // musicSource.clip = alertMusic;
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
    public IEnumerator SoundFadeOut()
    {
        if (musicSource.isPlaying)
        {
           // double lenght = (double)musicSource.clip.samples / musicSource.clip.frequency;
           // yield return new WaitForSecondsRealtime((float)lenght - fadeDuration);

            float time = 0;
            float originalVolume = musicSource.volume;
            while (time < fadeDuration)
            {
                time += Time.deltaTime;
                musicSource.volume = Mathf.Lerp(originalVolume, 0, time / fadeDuration);
                yield return null;
            }
            musicSource.Stop();
            musicSource.volume = originalVolume;
            yield break;
        }
    }
    private IEnumerator TriggerOnEnd(AudioSource source, int tipsListIndex)
    {
        yield return new WaitForSeconds(source.clip.length);
        EndOfClip?.Invoke(tipsListIndex);
    }
}
