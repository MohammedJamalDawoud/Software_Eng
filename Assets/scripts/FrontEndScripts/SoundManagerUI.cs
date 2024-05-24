using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SoundManagerUI : MonoBehaviour
{
    [Header("Music Level")]
    public Slider musicSlider;
    public float musicLevel = 0f;
    public TMP_Text musicLevelText;
    [Header("SFX Level")]
    public Slider sfxSlider;
    public float sfxLevel = 0f;
    public TMP_Text sfxLevelText;

    private void Start()
    {

    }

    public void OnMusicLevelChanged()
    {
        float value = musicSlider.value;
        musicLevel = value;
        musicLevelText.text = ((int)(value * 100)).ToString("00") + "%";
    }

    public void OnSFXLevelChanged()
    {
        float value = sfxSlider.value;
        sfxLevel = value;
        sfxLevelText.text = ((int)(value * 100)).ToString("00") + "%";
    }
}
