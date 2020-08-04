using UnityEngine;

public class ParticleSystemHandler : MonoBehaviour
{
    void Start()
    {
        Destroy(gameObject, 2f);
    }
}
