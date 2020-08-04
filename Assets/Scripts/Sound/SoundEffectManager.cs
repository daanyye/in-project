using System;
using UnityEngine;

public class SoundEffectManager : MonoBehaviour
{
    public Sound[] sounds;

    private void Awake()
    {
        foreach (Sound sound in sounds)
        {
            sound.source = gameObject.AddComponent<AudioSource>();
            sound.source.clip = sound.clip;
            sound.source.volume = PlayerPrefs.GetFloat("soundEffectVolume");

            sound.source.pitch = 1;
            sound.source.loop = false;
            sound.source.playOnAwake = false;
        }
    }

    public void PlaySound(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
            return;
        s.source.Play();
    }
}
