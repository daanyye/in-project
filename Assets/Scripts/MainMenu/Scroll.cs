using UnityEngine;

public class Scroll : MonoBehaviour
{
    public float speed = 0.02f;
    public Camera camera;

    void Update()
    {
        Vector2 offset = new Vector2(Time.time * speed, 0);
        GetComponent<Renderer>().material.mainTextureOffset = offset;
        gameObject.transform.localScale = new Vector3((camera.orthographicSize * camera.aspect) * 2, camera.orthographicSize * 2, 1);
    }

    //(camera.orthographicSize*camera.aspect). (horiz)
    //        camera.orthographicSize (vert)

}
