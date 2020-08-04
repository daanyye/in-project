using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsScript : MonoBehaviour
{
    public GameObject backgroundMusicManager;
    public GameObject soundEffectManager;
    public GameObject gameSoundEffectManager;
    public Slider musicVolumeSlider;
    public Slider soundEffectSlider;
    public Slider gameSoundEffectSlider;

    private void Start()
    {
        initializeMusicVolume();
        initializeSoundEffectVolume();
        initializeGameSoundEffectVolume();
    }

    private void initializeMusicVolume()
    {
        musicVolumeSlider.value = PlayerPrefs.GetFloat("musicVolume");
    }

    private void initializeSoundEffectVolume()
    {
        soundEffectSlider.value = PlayerPrefs.GetFloat("soundEffectVolume");
    }

    private void initializeGameSoundEffectVolume()
    {
        gameSoundEffectSlider.value = PlayerPrefs.GetFloat("gameSoundEffectVolume");
    }

    public void onMusicVolumeSliderValueChange()
    {
        backgroundMusicManager.GetComponent<AudioSource>().volume = musicVolumeSlider.value;
        PlayerPrefs.SetFloat("musicVolume", musicVolumeSlider.value);
    }

    public void onSoundEffectSliderValueChange()
    {
        foreach (var sound in soundEffectManager.GetComponent<SoundEffectManager>().sounds)
        {
            sound.source.volume = soundEffectSlider.value;
        }
        PlayerPrefs.SetFloat("soundEffectVolume", soundEffectSlider.value);
    }

    public void onGameSoundEffectVolumeSliderValueChange()
    {
        foreach (var sound in gameSoundEffectManager.GetComponent<GameSoundEffectManager>().sounds)
        {
            sound.source.volume = gameSoundEffectSlider.value;
        }
        PlayerPrefs.SetFloat("gameSoundEffectVolume", gameSoundEffectSlider.value);
    }

    public void playTestSoundEffect()
    {
        FindObjectOfType<SoundEffectManager>().PlaySound("clickMenuButton");
    }

    public void playTestGameSoundEffect()
    {
        FindObjectOfType<GameSoundEffectManager>().PlaySound("playerSwordSwing");
    }
}
