using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manages audio playback for background music and sound effects.
/// Handles volume control and persistence of audio settings using PlayerPrefs.
/// Implements singleton pattern for global audio access.
/// </summary>
public class SoundManager : MonoBehaviour
{
    /// <summary>
    /// Singleton instance of the SoundManager.
    /// Provides global access to audio functionality.
    /// </summary>
    public static SoundManager Instance;
    
    /// <summary>
    /// Audio source component for background music playback.
    /// </summary>
    public AudioSource musicAudioSource;
    
    /// <summary>
    /// Audio source component for sound effects playback.
    /// </summary>
    public AudioSource sfxAudioSource;
    
    /// <summary>
    /// Music volume level (0-100).
    /// </summary>
    public int musicLevel = 0;
    
    /// <summary>
    /// Sound effects volume level (0-100).
    /// </summary>
    public int sfxLevel = 0;
    /// <summary>
    /// Initializes the singleton instance and loads saved audio settings.
    /// Ensures only one SoundManager exists in the scene.
    /// </summary>
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
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
    }
    
    /// <summary>
    /// Updates and persists audio volume settings.
    /// Converts integer volume levels (0-100) to Unity's float volume range (0-1).
    /// </summary>
    /// <param name="musicLevel">Music volume level (0-100).</param>
    /// <param name="sfxlevel">Sound effects volume level (0-100).</param>
    public void StoreSoundData(int musicLevel, int sfxlevel)
    {
        musicAudioSource.volume = musicLevel / 100f;
        sfxAudioSource.volume = sfxlevel / 100f;
        PlayerPrefs.SetInt("musiclevel", musicLevel);
        PlayerPrefs.SetInt("sfx", sfxlevel);
    }
}
