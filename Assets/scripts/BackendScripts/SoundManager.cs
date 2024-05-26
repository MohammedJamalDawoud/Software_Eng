using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance; // this is the static instance of the SoundManager Class
    public AudioSource musicAudioSource; // this is the audio source component
    public AudioSource sfxAudioSource; // this is the button click sound
    public int musicLevel = 0;
    public int sfxLevel = 0;
    private void Awake()
    {
        if (Instance == null) // if the instance of the SoundManager is null then assign this to the instance
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject); // if the instance of the SoundManager is not null then destroy this object
        }
        if (PlayerPrefs.HasKey("musiclevel"))
        {
            musicAudioSource.volume = PlayerPrefs.GetInt("musiclevel") / 100f;
        }
        if (PlayerPrefs.HasKey("sfx"))
        {
            sfxAudioSource.volume = PlayerPrefs.GetInt("sfx") / 100f;
        }
        musicLevel = PlayerPrefs.GetInt("musiclevel");
        sfxLevel = PlayerPrefs.GetInt("sfx");
        // Debug.Log("Music Level: " + PlayerPrefs.GetInt("musiclevel") + " SFX Level: " + PlayerPrefs.GetInt("sfx"));
    }
    public void StoreSoundData(int musicLevel, int sfxlevel)
    {
        musicAudioSource.volume = musicLevel / 100f;
        sfxAudioSource.volume = sfxlevel / 100f;
        PlayerPrefs.SetInt("musiclevel", musicLevel);
        PlayerPrefs.SetInt("sfx", sfxlevel);
    }
}
