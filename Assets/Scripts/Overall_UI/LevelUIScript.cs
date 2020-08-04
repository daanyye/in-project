using UnityEngine;

public class LevelUIScript : MonoBehaviour
{
    public GameObject deathScreen;
    public GameObject fadeIn;

    private bool fadeInDone = false;

    private void Start()
    {
        deathScreen.SetActive(false);
        fadeIn.SetActive(true);
    }

    private void Update()
    {
        if(Time.time > 3 && !fadeInDone)
        {
            fadeIn.SetActive(false);
            fadeInDone = true;
        }
    }

    public void onGoToMenuClick()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }

    public void onRestartClick()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }

    public void showDeathScreen()
    {
        deathScreen.SetActive(true);
    }
}
