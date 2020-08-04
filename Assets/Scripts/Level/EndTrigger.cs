using UnityEngine;

public class EndTrigger : MonoBehaviour
{
    public LevelManager levelManager;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        levelManager.CompleteLevel();
    }
}