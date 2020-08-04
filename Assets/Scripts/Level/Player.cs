using UnityEngine;

public class Player : MonoBehaviour
{
    public float maxHealth;
    public float currentHealth;
    void Start()
    {
        maxHealth = PlayerPrefs.GetInt("healthPoints");
        currentHealth = maxHealth;
        PlayerPrefs.SetString("PlayerDead", "false");
    }
}
