using UnityEngine;

public class EndTrigger : MonoBehaviour
{
    public LevelManager levelManager;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(FindObjectsOfType<Enemy>().Length == 0)
            levelManager.CompleteLevel();
    }
}