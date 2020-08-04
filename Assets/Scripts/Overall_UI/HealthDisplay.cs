using UnityEngine;
using UnityEngine.UI;

public class HealthDisplay : MonoBehaviour
{
    public Image healthBar;
    public SelectedCharacterScript selectedCharacterScript;
    public Text healthText;

    void Update()
    {
        healthBar.fillAmount = selectedCharacterScript.selectedCharacter.GetComponent<Player>().currentHealth / selectedCharacterScript.selectedCharacter.GetComponent<Player>().maxHealth;
        healthText.text = selectedCharacterScript.selectedCharacter.GetComponent<Player>().currentHealth.ToString() + " / " + selectedCharacterScript.selectedCharacter.GetComponent<Player>().maxHealth.ToString();
    }
}
