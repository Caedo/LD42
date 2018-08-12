using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioScript : MonoBehaviour
{

    public AudioMixer mixer;

    public void SetMasterVolume(float level)
    {
        mixer.SetFloat("MasterVolume", (-80.0f * Mathf.Pow(Mathf.Abs(level), 6)));
    }

    public void SetMusicVolume(float level)
    {
        mixer.SetFloat("MusicVolume", (-80.0f * Mathf.Pow(Mathf.Abs(level), 6)));
    }

    public void SetEffetcsVolume(float level)
    {
        mixer.SetFloat("EffetcsVolume", (-80.0f * Mathf.Pow(Mathf.Abs(level), 6)));
    }
}