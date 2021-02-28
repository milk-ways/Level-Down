using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SettingSave : MonoBehaviour
{
    public static SettingSave instance;

    public SettingController setting;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(this.gameObject);

        LoadSetting();
    }

    public void SaveMusic(float volume)
    {
        PlayerPrefs.SetFloat("MusicVolume", volume);
    }

    public void SaveSFX(float volume)
    {
        PlayerPrefs.SetFloat("SFXVolume", volume);
    }

    public void LoadSetting()
    {
        setting.SetBGMusicVolume(PlayerPrefs.GetFloat("MusicVolume", 0));
        setting.SetBGMVal(PlayerPrefs.GetFloat("MusicVolume", 0));
        setting.SetSFXVolume(PlayerPrefs.GetFloat("SFXVolume", 0));
        setting.SetSFXVal(PlayerPrefs.GetFloat("SFXVolume", 0));
    }
}
