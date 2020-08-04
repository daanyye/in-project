using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelCompleteScript : MonoBehaviour
{
    public void LoadLevelSelector()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
