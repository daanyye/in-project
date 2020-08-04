using UnityEngine;
using UnityEngine.UI;

public class LevelSelector : MonoBehaviour
{
    public GameObject openButton;
    public GameObject playedButton;
    public GameObject lockedButton;

    public GameObject openBossButton;
    public GameObject playedBossButton;
    public GameObject lockedBossButton;

    public GameObject[] levelArray;
    public GameObject[] levelArea;
    public GameObject[] bossLevelArea;
    public Text[] levelText;

    public GameManager gameManager;

    bool lockedButtons = false;

    void Start()
    {
        initializeButtonList();
    }

    void initializeButtonList()
    {
        for (int i = 0; i < gameManager.maxStages; i++)
        {
            levelText[i].text = "Level " + (i + 1);

            for (int j = 0; j < gameManager.maxLevel; j++)
            {
                var level = "" + (i + 1) + "-" + (j + 1);

                if (level != gameManager.getProgress() && !lockedButtons)
                {
                    if (checkForBossButton(j))
                        instantiateLockedOrPlayedBossButton(playedBossButton, bossLevelArea[i]);
                    else
                        instantiateLockedOrPlayedButton(playedButton, levelArea[i]);
                }
                else if (!lockedButtons)
                {
                    if (checkForBossButton(j))
                    {
                        instantiateOpenBossButton(openBossButton, bossLevelArea[i], level);
                        lockedButtons = true;
                    }

                    else
                    {
                        instantiateOpenButton(openButton, levelArea[i], level);
                        lockedButtons = true;
                    }
                }
                else if (lockedButtons)
                {
                    if (checkForBossButton(j))
                        instantiateLockedOrPlayedBossButton(lockedBossButton, bossLevelArea[i]);
                    else
                        instantiateLockedOrPlayedButton(lockedButton, levelArea[i]);
                }
            }
        }
    }

    void changeLevel(string level)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(level);
    }

    void instantiateOpenButton(GameObject button, GameObject levelArea, string level)
    {
        var openButton = Instantiate(button) as GameObject;
        openButton.GetComponentInChildren<Text>().text = level;
        openButton.GetComponent<Button>().onClick.AddListener(() => changeLevel(level));
        openButton.transform.SetParent(levelArea.transform, false);
    }

    void instantiateLockedOrPlayedButton(GameObject button, GameObject levelArea)
    {
        var lopButton = Instantiate(button) as GameObject;
        lopButton.transform.SetParent(levelArea.transform, false);
    }

    void instantiateOpenBossButton(GameObject button, GameObject bossLevelArea, string level)
    {
        var openBossButton = Instantiate(button) as GameObject;
        openBossButton.GetComponentInChildren<Text>().text = level;
        openBossButton.GetComponent<Button>().onClick.AddListener(() => changeLevel(level));
        openBossButton.transform.SetParent(bossLevelArea.transform, false);
    }

    void instantiateLockedOrPlayedBossButton(GameObject button, GameObject bossLevelArea)
    {
        var lopBossButton = Instantiate(button) as GameObject;
        lopBossButton.transform.SetParent(bossLevelArea.transform, false);
    }

    bool checkForBossButton(int index)
    {
        if (index + 1 == gameManager.maxLevel)
            return true;
        return false;
    }
}
