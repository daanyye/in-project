using UnityEngine;
using UnityEngine.UI;

public class ResetScrollBarScript : MonoBehaviour
{
    public Scrollbar scrollBar;
    void Start()
    {
        scrollBar.value = 1;
    }
}
