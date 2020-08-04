using UnityEngine;

public class GameManager : MonoBehaviour
{
    public readonly int maxStages = 3;
    public readonly int maxLevel = 9;

    public string getProgress()
    {
        return PlayerPrefs.GetString("levelProgress");
    }
}
