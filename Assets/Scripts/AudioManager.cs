using UnityEngine.Audio;
using System;
using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public AudioMixerGroup mixerGroup;

    public Sound[] sounds;

    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

        foreach (Sound s in sounds)
        {
            if (s.clip == null)
            {
                Debug.LogWarning("Sound: " + s.name + " hat keinen Clip zugewiesen!");
                continue;
            }

            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.loop = s.loop;

            s.source.outputAudioMixerGroup = mixerGroup;

            Debug.Log("Sound: " + s.name + " wurde erfolgreich initialisiert.");
        }
    }

    public void Play(string sound)
    {
        Sound s = Array.Find(sounds, item => item.name == sound);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + sound + " not found in the AudioManager!");
            return;
        }

        if (s.source == null)
        {
            Debug.LogWarning("AudioSource for sound: " + sound + " is null!");
            return;
        }

        s.source.volume = s.volume * (1f + UnityEngine.Random.Range(-s.volumeVariance / 2f, s.volumeVariance / 2f));
        s.source.pitch = s.pitch * (1f + UnityEngine.Random.Range(-s.pitchVariance / 2f, s.pitchVariance / 2f));

        s.source.Play();
    }

    public void Stop(string sound)
    {
        Sound s = Array.Find(sounds, item => item.name == sound);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + sound + " not found in the AudioManager!");
            return;
        }

        if (s.source == null)
        {
            Debug.LogWarning("AudioSource for sound: " + sound + " is null!");
            return;
        }

        s.source.Stop();
    }

    public void FadeIn(string name, float duration)
    {
        StartCoroutine(FadeInCoroutine(name, duration));
    }

    public void FadeOut(string name, float duration)
    {
        StartCoroutine(FadeOutCoroutine(name, duration));
    }

    private IEnumerator FadeInCoroutine(string name, float duration)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            yield break;
        }

        s.source.volume = 0;
        s.source.Play();

        float targetVolume = s.volume;
        float startVolume = 0;

        for (float t = 0; t < duration; t += Time.deltaTime)
        {
            s.source.volume = Mathf.Lerp(startVolume, targetVolume, t / duration);
            yield return null;
        }
        s.source.volume = targetVolume;
    }

    private IEnumerator FadeOutCoroutine(string name, float duration)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            yield break;
        }

        float startVolume = s.source.volume;

        for (float t = 0; t < duration; t += Time.deltaTime)
        {
            s.source.volume = Mathf.Lerp(startVolume, 0, t / duration);
            yield return null;
        }
        s.source.Stop();
    }
}