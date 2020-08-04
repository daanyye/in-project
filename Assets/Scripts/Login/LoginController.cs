using SimpleJSON;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class LoginController : MonoBehaviour
{
    private readonly string apiUrl = "http://localhost:5000/";
    public TMP_InputField username;
    public TMP_InputField password;
    public GameObject loginInterface;
    public GameObject loadingProgress;
    public Text loadingProgressText;

    private void Start()
    {
        loginInterface.SetActive(true);
        loadingProgress.SetActive(false);
        loadingProgressText.text = "";
    }

    public void onLoginClick()
    {
        loginInterface.SetActive(false);
        loadingProgress.SetActive(true);
        StartCoroutine(requestLogin());
    }

    private void resetAfterError()
    {
        loginInterface.SetActive(true);
        loadingProgress.SetActive(false);
        loadingProgressText.text = "";
    }

    private IEnumerator requestLogin()
    {
        loadingProgressText.text = "Authenticating...";
        string requestUrl = apiUrl + "game?";
        requestUrl += "username=" + username.text + "&password=" + password.text;

        UnityWebRequest loginRequest = UnityWebRequest.Get(requestUrl);
        yield return loginRequest.SendWebRequest();

        if (loginRequest.isNetworkError || loginRequest.isHttpError)
        {
            resetAfterError();
            yield break;
        }

        JSONNode responseText = JSON.Parse(loginRequest.downloadHandler.text);
        if (responseText["successful"] == true)
        {
            PlayerPrefs.SetString("userid", responseText["userid"]);
            PlayerPrefs.SetString("firstLogin", responseText["firstLogin"].ToString());
            StartCoroutine(requestProcessFoodentries());
        }
        else
        {
            resetAfterError();
            yield break;
        }
    }

    private IEnumerator requestProcessFoodentries()
    {
        loadingProgressText.text = "Processing Foodentries...";
        string requestUrl = apiUrl + "game?";
        requestUrl += "userid=" + PlayerPrefs.GetString("userid") + "&getType=" + "processFoodEntries";

        UnityWebRequest processFoodentriesRequest = UnityWebRequest.Get(requestUrl);
        yield return processFoodentriesRequest.SendWebRequest();

        if (processFoodentriesRequest.isNetworkError || processFoodentriesRequest.isHttpError)
        {
            resetAfterError();
            yield break;
        }

        JSONNode responseText = JSON.Parse(processFoodentriesRequest.downloadHandler.text);
        StartCoroutine(processQuestsRequest());
    }

    private IEnumerator processQuestsRequest()
    {
        loadingProgressText.text = "Processing Quests...";
        string requestUrl = apiUrl + "game?";
        requestUrl += "userid=" + PlayerPrefs.GetString("userid") + "&getType=" + "processQuests";

        UnityWebRequest messageRequest = UnityWebRequest.Get(requestUrl);
        yield return messageRequest.SendWebRequest();

        if (messageRequest.isNetworkError || messageRequest.isHttpError)
        {
            resetAfterError();
            yield break;
        }
        StartCoroutine(requestLevelProgress());
    }

    private IEnumerator requestLevelProgress()
    {
        loadingProgressText.text = "Loading Levelprogress...";
        string requestUrl = apiUrl + "game?";
        requestUrl += "userid=" + PlayerPrefs.GetString("userid") + "&getType=" + "levelProgress";

        UnityWebRequest levelProgressRequest = UnityWebRequest.Get(requestUrl);
        yield return levelProgressRequest.SendWebRequest();

        if (levelProgressRequest.isNetworkError || levelProgressRequest.isHttpError)
        {
            resetAfterError();
            yield break;
        }

        JSONNode responseText = JSON.Parse(levelProgressRequest.downloadHandler.text);
        PlayerPrefs.SetString("levelProgress", responseText["levelprogress"]);
        StartCoroutine(requestCharacterPoints());
    }

    private IEnumerator requestCharacterPoints()
    {
        loadingProgressText.text = "Loading Characterpoints...";
        string requestUrl = apiUrl + "game?";
        requestUrl += "userid=" + PlayerPrefs.GetString("userid") + "&getType=" + "characterPoints";

        UnityWebRequest characterAttributesRequest = UnityWebRequest.Get(requestUrl);
        yield return characterAttributesRequest.SendWebRequest();

        if (characterAttributesRequest.isNetworkError || characterAttributesRequest.isHttpError)
        {
            resetAfterError();
            yield break;
        }

        JSONNode responseText = JSON.Parse(characterAttributesRequest.downloadHandler.text);

        PlayerPrefs.SetInt("atkNumber", responseText["atkNumber"].AsInt);
        PlayerPrefs.SetInt("hpNumber", responseText["hpNumber"].AsInt);
        PlayerPrefs.SetInt("defNumber", responseText["defNumber"].AsInt);
        PlayerPrefs.SetInt("freePointsATK", responseText["freePointsATK"].AsInt);
        PlayerPrefs.SetInt("freePointsHP", responseText["freePointsHP"].AsInt);
        PlayerPrefs.SetInt("freePointsDEF", responseText["freePointsDEF"].AsInt);
        StartCoroutine(requestCharacterSelection());
    }

    private IEnumerator requestCharacterSelection()
    {
        loadingProgressText.text = "Loading Characters...";
        string requestUrl = apiUrl + "game?";
        requestUrl += "userid=" + PlayerPrefs.GetString("userid") + "&getType=" + "currentCharacter";

        UnityWebRequest characterSelectionRequest = UnityWebRequest.Get(requestUrl);
        yield return characterSelectionRequest.SendWebRequest();

        if (characterSelectionRequest.isNetworkError || characterSelectionRequest.isHttpError)
        {
            resetAfterError();
            yield break;
        }

        JSONNode responseText = JSON.Parse(characterSelectionRequest.downloadHandler.text);

        PlayerPrefs.SetString("selectedCharacter", responseText["currentCharacter"]);
        StartCoroutine(requestSettings());
    }

    private IEnumerator requestSettings()
    {
        loadingProgressText.text = "Loading Settings...";
        string requestUrl = apiUrl + "game?";
        requestUrl += "userid=" + PlayerPrefs.GetString("userid") + "&getType=" + "optionValues";

        UnityWebRequest settingsRequest = UnityWebRequest.Get(requestUrl);
        yield return settingsRequest.SendWebRequest();

        if (settingsRequest.isNetworkError || settingsRequest.isHttpError)
        {
            resetAfterError();
            yield break;
        }

        JSONNode responseText = JSON.Parse(settingsRequest.downloadHandler.text);

        PlayerPrefs.SetFloat("musicVolume", float.Parse(responseText["musicvolume"]));
        PlayerPrefs.SetFloat("soundEffectVolume", float.Parse(responseText["soundvolume"]));
        PlayerPrefs.SetFloat("gameSoundEffectVolume", float.Parse(responseText["gamesoundvolume"]));
        StartCoroutine(requestMessages());
    }

    private IEnumerator requestMessages()
    {
        loadingProgressText.text = "Loading Messages...";
        string requestUrl = apiUrl + "game?";
        requestUrl += "userid=" + PlayerPrefs.GetString("userid") + "&getType=" + "messageQueue";

        UnityWebRequest messageRequest = UnityWebRequest.Get(requestUrl);
        yield return messageRequest.SendWebRequest();

        if (messageRequest.isNetworkError || messageRequest.isHttpError)
        {
            resetAfterError();
            yield break;
        }

        JSONNode responseText = JSON.Parse(messageRequest.downloadHandler.text);

        PlayerPrefs.SetString("messages", responseText);
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }
}