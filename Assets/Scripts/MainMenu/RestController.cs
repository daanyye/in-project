using SimpleJSON;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class RestController : MonoBehaviour
{
    public GameObject attributeContent;
    public GameObject settingsContent;
    public Text[] questDescriptions;
    public Text[] questProgress;
    public Image[] questProgressBar;
    public Text[] questReward;

    private readonly string apiUrl = "http://localhost:5000/";

    private void Awake()
    {
        StartCoroutine(getQuestProgress());
    }

    public void onApplyAttributesClick()
    {
        StartCoroutine(executeAttributePoints());
    }

    public void onApplySettingsClick()
    {
        StartCoroutine(executeSettings());
    }

    public void onApplyNewCharacter(string character)
    {
        StartCoroutine(executeCharacterSelection(character));
    }

    private IEnumerator getQuestProgress()
    {
        string requestUrl = apiUrl + "game?";
        requestUrl += "userid=" + PlayerPrefs.GetString("userid") + "&getType=" + "questProgress";

        UnityWebRequest messageRequest = UnityWebRequest.Get(requestUrl);
        yield return messageRequest.SendWebRequest();

        if (messageRequest.isNetworkError || messageRequest.isHttpError)
        {
            yield break;
        }

        JSONNode responseText = JSON.Parse(messageRequest.downloadHandler.text);

        for (int i = 0; i < responseText.Count; i++)
        {
            if (responseText[i]["done"])
            {
                questDescriptions[i].text = responseText[i]["text"];
                questProgress[i].text = "Done";
                questProgressBar[i].fillAmount = 1f;
                questReward[i].text = "";
            }
            else
            {
                questDescriptions[i].text = responseText[i]["text"];
                questProgress[i].text = responseText[i]["streak"] + " / " + responseText[i]["goal"];
                questProgressBar[i].fillAmount = responseText[i]["streak"].AsFloat / responseText[i]["goal"].AsFloat;
                questReward[i].text = "+" + responseText[i]["amount"] + " " + responseText[i]["addTo"];
            }
        }
    }

    private IEnumerator executeAttributePoints()
    {
        AttributeScript tempScript = attributeContent.GetComponent<AttributeScript>();

        string requestUrl = apiUrl + "game?";
        requestUrl += "userid=" + PlayerPrefs.GetString("userid") +
                      "&atkNumber=" + tempScript.atkNumber.text +
                      "&hpNumber=" + tempScript.hpNumber.text +
                      "&defNumber=" + tempScript.defNumber.text +
                      "&freePointsATK=" + tempScript.freePointsToUseOnATK.text +
                      "&freePointsHP=" + tempScript.freePointsToUseOnHP.text +
                      "&freePointsDEF=" + tempScript.freePointsToUseOnDEF.text;

        UnityWebRequest attributePointsRequest = UnityWebRequest.Put(requestUrl, "put-request");
        yield return attributePointsRequest.SendWebRequest();

        if (attributePointsRequest.isNetworkError || attributePointsRequest.isHttpError)
            yield break;

        JSONNode responseText = JSON.Parse(attributePointsRequest.downloadHandler.text);
        if (responseText == "success")
            Debug.Log("Pointchange was successful");
    }

    private IEnumerator executeSettings()
    {
        SettingsScript tempScript = settingsContent.GetComponent<SettingsScript>();

        string requestUrl = apiUrl + "game?";
        requestUrl += "userid=" + PlayerPrefs.GetString("userid") +
                      "&musicvolume=" + tempScript.musicVolumeSlider.value.ToString() +
                      "&soundvolume=" + tempScript.soundEffectSlider.value.ToString() +
                      "&gamesoundvolume=" + tempScript.gameSoundEffectSlider.value.ToString();

        UnityWebRequest settingsRequest = UnityWebRequest.Put(requestUrl, "put-request");
        yield return settingsRequest.SendWebRequest();

        if (settingsRequest.isNetworkError || settingsRequest.isHttpError)
            yield break;

        JSONNode responseText = JSON.Parse(settingsRequest.downloadHandler.text);
        if (responseText == "success")
            Debug.Log("Settingschange was successful");
    }

    private IEnumerator executeCharacterSelection(string character)
    {
        string requestUrl = apiUrl + "game?";
        requestUrl += "userid=" + PlayerPrefs.GetString("userid") +
                      "&character=" + character;

        UnityWebRequest settingsRequest = UnityWebRequest.Put(requestUrl, "put-request");
        yield return settingsRequest.SendWebRequest();

        if (settingsRequest.isNetworkError || settingsRequest.isHttpError)
            yield break;

        JSONNode responseText = JSON.Parse(settingsRequest.downloadHandler.text);
        if (responseText == "success")
            Debug.Log("Characterchange was successful");
    }


}
