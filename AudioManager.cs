using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    // Äänilähteet sisältävä oliotaulukko
    public Sound[] sounds;

    public AudioMixerGroup audioMixerGroup;
    public static AudioManager instance;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        // Näyttää oliotaulukon kaikki äänilähteet
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            // Päivittää tehdyt säädöt Audio-komponenttiin
            s.source.volume = s.volume; 
            s.source.pitch = s.pitch;   
            s.source.loop = s.loop;
            s.source.outputAudioMixerGroup = audioMixerGroup;
        }
    }

    public void Start()
    {
        // Soitetaan pelin taustamusa
        Play("Taustamusa");
    }

    /// <summary>
    /// Soittaa halutun äänen.
    /// </summary>
    /// <param name="name"></param>
    public void Play(string name)
    {
        // Etsitään haluttu ääni
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
            return;
        // Soitetaan ääni
        s.source.Play();
    }

    public void StopAll()
    {
        for (int i = 0; i < sounds.Length; i++)
        {
            Sound[] s = Array.FindAll(sounds, Sound => Sound.name.Length > 1);

            StopPlay(s[i].name);
        }
    }

    public void StopPlay(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
            return;
        s.source.Stop();
    }
}
