using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioSliders : MonoBehaviour
{
    public Slider sfxSlider;
    public Slider musicSlider;

    // Start is called before the first frame update
    void Start()
    {
        musicSlider.value = AudioManager.Instance.musicVolume;
        musicSlider.onValueChanged.AddListener((value) => AudioManager.Instance.SetMusicVolume(value));

        sfxSlider.value = AudioManager.Instance.sfxVolume;
        sfxSlider.onValueChanged.AddListener((value) => AudioManager.Instance.SetSFXVolume(value));

    }

}
