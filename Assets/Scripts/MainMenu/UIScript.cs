using UnityEngine;
using UnityEngine.UI;

public class UIScript : MonoBehaviour
{
    public GameObject levelSelector;
    public GameObject attributePage;
    public GameObject characterSelector;
    public GameObject questsPage;
    public GameObject optionsMenu;
    public Image popUpFirstLogin;
    public Image popUpPoints;
    public Image faqPopUp;
    public Image selectedCharacterSelectButton;
    public Image selectedArmoryButton;
    public Image selectedLevelsButton;
    public Image selectedQuestsButton;
    public Image selectedOptionsButton;
    public Text popUpPointsText;

    void Awake()
    {
        checkForFirstLoginPopUp();
        checkForPointsPopUp();
    }
    void Start()
    {
        characterSelector.SetActive(false);
        attributePage.SetActive(false);
        levelSelector.SetActive(true);
        questsPage.SetActive(false);
        optionsMenu.SetActive(false);
        faqPopUp.gameObject.SetActive(false);
        selectedCharacterSelectButton.gameObject.SetActive(false);
        selectedArmoryButton.gameObject.SetActive(false);
        selectedLevelsButton.gameObject.SetActive(true);
        selectedQuestsButton.gameObject.SetActive(false);
        selectedOptionsButton.gameObject.SetActive(false);
    }

    public void onCharacterSelectClick()
    {
        FindObjectOfType<SoundEffectManager>().PlaySound("clickMenuButton");
        manageVisibleMenus(true, false, false, false, false);
        manageSelectedButtons(true, false, false, false, false);
    }
    
    public void onArmoryClick()
    {
        FindObjectOfType<SoundEffectManager>().PlaySound("clickMenuButton");
        manageVisibleMenus(false, false, true, false, false);
        manageSelectedButtons(false, true, false, false, false);
    }

    public void onQuestClick()
    {
        FindObjectOfType<SoundEffectManager>().PlaySound("clickMenuButton");
        manageVisibleMenus(false, true, false, false, false);
        manageSelectedButtons(false, false, true, false, false);
    }

    public void onPlaceHolderClick()
    {
        FindObjectOfType<SoundEffectManager>().PlaySound("clickMenuButton");
        manageVisibleMenus(false, false, false, true, false);
        manageSelectedButtons(false, false, false, true, false);
    }

    public void onSettingsClick()
    {
        FindObjectOfType<SoundEffectManager>().PlaySound("clickMenuButton");
        manageVisibleMenus(false, false, false, false, true);
        manageSelectedButtons(false, false, false, false, true);
    }

    public void onPopUpCloseClick()
    {
        popUpFirstLogin.gameObject.SetActive(false);
    }
    public void onPopUpPointsCloseClick()
    {
        popUpPoints.gameObject.SetActive(false);
    }

    public void onFaqPopClick()
    {
        faqPopUp.gameObject.SetActive(true);
    }

    public void onFaqPopCloseClick()
    {
        faqPopUp.gameObject.SetActive(false);
    }

    private void manageVisibleMenus(bool characterselector, bool levelselector, bool attributepage, bool questspage, bool options)
    {
        characterSelector.SetActive(characterselector);
        levelSelector.SetActive(levelselector);
        attributePage.SetActive(attributepage);
        questsPage.SetActive(questspage);
        optionsMenu.SetActive(options);
    }
    private void manageSelectedButtons(bool characterSelect, bool armory, bool quest, bool placeholder, bool options)
    {
        selectedCharacterSelectButton.gameObject.SetActive(characterSelect);
        selectedArmoryButton.gameObject.SetActive(armory);
        selectedLevelsButton.gameObject.SetActive(quest);
        selectedQuestsButton.gameObject.SetActive(placeholder);
        selectedOptionsButton.gameObject.SetActive(options);
    }

    private void checkForFirstLoginPopUp()
    {
        if (PlayerPrefs.GetString("firstLogin").Equals("true"))
        {
            popUpFirstLogin.gameObject.SetActive(true);
            PlayerPrefs.SetString("firstLogin", "false");
        }
        else
            popUpFirstLogin.gameObject.SetActive(false);
    }

    private void checkForPointsPopUp()
    {
        if (PlayerPrefs.GetString("messages").Equals(""))
            popUpPoints.gameObject.SetActive(false);
        else
        {
            popUpPoints.gameObject.SetActive(true);
            popUpPointsText.text = PlayerPrefs.GetString("messages");
            PlayerPrefs.SetString("messages", "");
        }
    }

    public void onLogoutClick()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Login");
        PlayerPrefs.DeleteAll();
    }
}
