using UnityEngine;

public class FloatingPointHandler : MonoBehaviour
{ 
    void Start()
    {
        Destroy(gameObject, 1f);
        transform.localPosition += new Vector3(0f, 0.5f, 0f);
    }
}
