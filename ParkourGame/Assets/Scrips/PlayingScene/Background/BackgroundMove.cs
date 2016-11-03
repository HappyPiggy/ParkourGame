using UnityEngine;
using System.Collections;

public class BackgroundMove : MonoBehaviour {

   // public float speed;

    void FixedUpdate()
    {
        transform.Translate(Vector3.left * GameController2.Instance.bgSpeed * Time.deltaTime, Space.World);

    }
}
