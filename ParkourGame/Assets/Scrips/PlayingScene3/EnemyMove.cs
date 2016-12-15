
using UnityEngine;


public class EnemyMove : MonoBehaviour
{

    public float moveSpeed;

    void FixedUpdate()
    {
        transform.Translate(Vector3.left * moveSpeed * Time.deltaTime, Space.World);
    }
}
