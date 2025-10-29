using UnityEngine;
using UnityEngine.Audio;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.Networking;
using TMPro;
using Unity.Netcode;

public class AudioManager : MonoBehaviour
{

    public Sound[] sounds;

    public static AudioManager instance;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        foreach(Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    private void Start()
    {
        Play("Menu (Unlooped)");
        StartCoroutine("PlayMenuTheme");
    }

    IEnumerator PlayMenuTheme()
    {
        Debug.Log("Playing Menu Theme");
        yield return new WaitForSeconds(78.5f);
        Play("Menu (Looped)");
    }

    IEnumerator PlayTutorialTheme()
    {
        Debug.Log("Playing Tutorial Theme");
        yield return new WaitForSeconds(127.8f);
        Play("Tutorial (Looped)");
    }

    IEnumerator PlayCardGalleryTheme()
    {
        Debug.Log("Playing Card Gallery Theme");
        yield return new WaitForSeconds(90.7f);
        Play("Card Gallery (Looped)");
    }

    IEnumerator PlayFightTheme()
    {
        Debug.Log("Playing Fight Theme");
        yield return new WaitForSeconds(134.5f);
        Play("Card Gallery (Looped)");
    }

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if(s == null)
        {
            return;
        }
        Debug.Log("Playing Sound");
        s.source.Play();
    }

    public void StopPlaying(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            return;
        }
        s.source.Stop();
    }

}
