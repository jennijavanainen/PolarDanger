﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class VolumeSlider : MonoBehaviour
{   
    public AudioMixer mixer;
    public void SetVolume(float volume)
    {
        mixer.SetFloat("volume", volume);
    }
}
