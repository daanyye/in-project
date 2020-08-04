using SimpleJSON;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class RestProgressController : MonoBehaviour
{
    private readonly string apiUrl = "http://localhost:5000/";

    public void startOverrideProgress(string level)
    {
        StartCoroutine(overrideProgress(level));
    }

    public IEnumerator overrideProgress(string level)
    {
        string requestUrl = apiUrl + "game?";
        requestUrl += "userid=" + PlayerPrefs.GetString("userid") + "&levelprogress=" + level;

        UnityWebRequest changeLevelProgressRequest = UnityWebRequest.Put(requestUrl, "change levelprogress");
        yield return changeLevelProgressRequest.SendWebRequest();

        if (changeLevelProgressRequest.isNetworkError || changeLevelProgressRequest.isHttpError)
            yield break;

        JSONNode responseText = JSON.Parse(changeLevelProgressRequest.downloadHandler.text);
        if (responseText == "success")
        {
            Debug.Log("Level was changed in Database!");
        }
    }
}
