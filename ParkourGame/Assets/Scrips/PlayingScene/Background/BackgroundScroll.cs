

using UnityEngine;


public class BackgroundScroll : MonoBehaviour {

  //  public float scrollSpeed;
    private float tileSize;
    private Vector3 startPosition;
    public float scrollSpeed;

    void Start()
    {
       // scrollSpeed   = 5;
        tileSize      = 20;
        startPosition = transform.position;
    }

    void Update()
    {
        float newPosition = Mathf.Repeat(Time.time * scrollSpeed, tileSize);
        //  Debug.Log(newPosition);
        transform.position = startPosition + Vector3.left * newPosition;
    }
}
