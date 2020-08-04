using UnityEngine;
using UnityEngine.UI;

public class CharacterSelectionScript : MonoBehaviour
{
    public Image characterImage;
    public Sprite selectedBackground;
    public Sprite unselectedBackground;
    public Sprite[] characterImages;
    public GameObject restController;

    public GameObject[] slots;

    private void Awake()
    {
        checkSelectedCharacter();
    }

    public void onSlot1Click()
    {
        selectCharacter("knight", 0);
    }

    public void onSlot2Click()
    {
        selectCharacter("samurai", 1);
    }

    private void selectCharacter(string character, int characterImageIndex)
    {
        resetOtherSlots();
        restController.GetComponent<RestController>().onApplyNewCharacter(character);
        PlayerPrefs.SetString("selectedCharacter", character);
        characterImage.sprite = characterImages[characterImageIndex];
        slots[characterImageIndex].GetComponent<Image>().sprite = selectedBackground;
        slots[characterImageIndex].GetComponentInChildren<Button>().interactable = false;
    }

    private void resetOtherSlots()
    {
        foreach (var item in slots)
        {
            item.GetComponent<Image>().sprite = unselectedBackground;
            item.GetComponentInChildren<Button>().interactable = true;
        }
    }

    private void checkSelectedCharacter()
    {
        switch(PlayerPrefs.GetString("selectedCharacter"))
        {
            case "knight":
                setSelectedBackgroundAndCharacter(0);
                break;
            case "samurai":
                setSelectedBackgroundAndCharacter(1);
                break;
        }
    }

    private void setSelectedBackgroundAndCharacter(int characterImageIndex)
    {
        characterImage.sprite = characterImages[characterImageIndex];
        slots[characterImageIndex].GetComponent<Image>().sprite = selectedBackground;
        slots[characterImageIndex].GetComponentInChildren<Button>().interactable = false;
    }
}