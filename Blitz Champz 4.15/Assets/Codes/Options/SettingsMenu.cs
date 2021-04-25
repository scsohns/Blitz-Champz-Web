using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{

    public AudioMixer audioMixer;
    public Slider musicSlider;
    public Slider effectSlider;

    void Start()
    {
        musicSlider.value = PlayerPrefs.GetFloat("MusicVol", 0.75f);
        effectSlider.value = PlayerPrefs.GetFloat("SetEffectVolume", 0.5f);
    }
    
    public void SetVolume(float musicValue)
    {
        audioMixer.SetFloat("MusicVol", Mathf.Log10(musicValue) * 20);
        PlayerPrefs.SetFloat("MusicVol", musicValue);
    }

    public void SetEffectVolume(float effectValue)
    {
        audioMixer.SetFloat("EffectVol", Mathf.Log10(effectValue) * 20);
        PlayerPrefs.SetFloat("EffectVol", effectValue);
    }

    public void doExitGame()
    {
        Application.Quit();
    }
}

