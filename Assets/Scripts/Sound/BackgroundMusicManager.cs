using UnityEngine;

public class BackgroundMusicManager : MonoBehaviour
{
    private void Awake()
    {
        GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat("musicVolume");
    }
}
