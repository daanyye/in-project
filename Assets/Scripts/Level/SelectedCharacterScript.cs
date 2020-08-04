using UnityEngine;
using UnityEngine.UI;

public class SelectedCharacterScript : MonoBehaviour
{
    public GameObject[] playerCharacters;
    public Sprite[] playerPictures;
    public GameObject playerPicture;
    public LevelManager levelManager;

    [HideInInspector]
    public GameObject selectedCharacter;

    void Awake()
    {
        PlayerPrefs.SetString("selectedCharacter", "samurai");
        checkCharacterSelection();
    }

    private void checkCharacterSelection()
    {
        switch(PlayerPrefs.GetString("selectedCharacter"))
        {
            case "knight":
                instantiateCharacter(0);
                setPlayerPicture(0);
                break;
            case "samurai":
                instantiateCharacter(1);
                setPlayerPicture(1);
                break;
            default:
                break;
        }
    }

    private void instantiateCharacter(int index)
    {
        selectedCharacter = Instantiate(playerCharacters[index]);
        selectedCharacter.transform.position = levelManager.spawnPoint.transform.position;
    }

    private void setPlayerPicture(int index)
    {
        playerPicture.GetComponent<Image>().sprite = playerPictures[index];
    }
}
