using UnityEngine;
using System.Collections;

public class Bubble : MonoBehaviour
{

    public float bubbleSpeed;
    private Rigidbody2D rb;
    private float SpeedTimer;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Vector2 velocity = rb.velocity;
        velocity.y = bubbleSpeed;
        rb.velocity = velocity;

        //大小 0.2-0.5
        float currentScale = Random.Range(0.2f, 0.5f);
        transform.localScale=new Vector3(currentScale,currentScale,0);
    }

    void Update()
    {
        SpeedTimer += Time.deltaTime;

        //0.5s加速一次
        if (SpeedTimer >= 0.4f)
        {
            SpeedTimer = 0;
            Vector2 velocity = rb.velocity;
            velocity.y = velocity.y * 1.2f;
            rb.velocity = velocity;
        }

       
    }


    void OnTriggerExit2D(Collider2D other)
    {
        if(other.name=="UpBorder")
            Destroy(gameObject);
    }
}
