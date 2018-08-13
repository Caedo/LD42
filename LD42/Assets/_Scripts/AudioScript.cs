using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioScript : MonoBehaviour
{

    public Slider m_MasterSlider;
    public Slider m_MusicSlider;
    public Slider m_EffectsSlider;

    public AudioMixer mixer;
    
    private void Start() {
        float masterVolume = PlayerPrefs.GetFloat("MasterVolume", m_MasterSlider.maxValue);
        float musicVolume = PlayerPrefs.GetFloat("MusicVolume", m_MusicSlider.maxValue);
        float effectsVolume = PlayerPrefs.GetFloat("EffectsVolume", m_EffectsSlider.maxValue);
			
        m_MasterSlider.value = masterVolume;
        m_MusicSlider.value = musicVolume;
        m_EffectsSlider.value = effectsVolume;

        SetMasterVolume(masterVolume);
        SetMusicVolume(musicVolume);
        SetEffetcsVolume(effectsVolume);
    }

    public void SetMasterVolume(float level)
    {
        mixer.SetFloat("MasterVolume", (-80.0f * Mathf.Pow(Mathf.Abs(level), 6)));
        PlayerPrefs.SetFloat("MasterVolume", level);
        PlayerPrefs.Save();
    }

    public void SetMusicVolume(float level)
    {
        mixer.SetFloat("MusicVolume", (-80.0f * Mathf.Pow(Mathf.Abs(level), 6)));
        PlayerPrefs.SetFloat("MusicVolume", level);
        PlayerPrefs.Save();
    }

    public void SetEffetcsVolume(float level)
    {
        mixer.SetFloat("EffectsVolume", (-80.0f * Mathf.Pow(Mathf.Abs(level), 6)));
        PlayerPrefs.SetFloat("EffectsVolume", level);
        PlayerPrefs.Save();
    }
}