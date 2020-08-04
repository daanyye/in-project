using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public GameObject completeLevelUI;
    public RestProgressController restProgressController;
    public GameObject spawnPoint;

    public void CompleteLevel()
    {
        string nextLevel = getNextLevel();
        restProgressController.startOverrideProgress(nextLevel);
        PlayerPrefs.SetString("levelProgress", nextLevel);
        completeLevelUI.SetActive(true);
    }

    private string getNextLevel()
    {
        string currentLevel = SceneManager.GetActiveScene().name;
        int lineIndex = currentLevel.IndexOf("-");

        string mainLevelNumber = currentLevel.Substring(0, lineIndex);
        string partLevelNumber = currentLevel.Substring(lineIndex + 1);

        if (partLevelNumber == "9")
            return (int.Parse(mainLevelNumber) + 1).ToString() + "-1";

        return mainLevelNumber + "-" + (int.Parse(partLevelNumber) + 1).ToString();
    }
}
