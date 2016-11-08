using UnityEngine;
using System.Collections;
using Microsoft.Win32;

public class PlayerSwim : MonoBehaviour
{

    public float swimVelocity;


    [HideInInspector]
    public bool isDead;


    private Rigidbody2D rb;
    private bool isSwim;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        isDead = false;
        // isSwim = true;
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Z) &&!isDead)
        {
            isSwim = true;

            if (isSwim)
            {
                Vector2 velocity = rb.velocity;
                velocity.y = swimVelocity;
                rb.velocity = velocity;
               // print(velocity.y);
            }
         
        }


        //放开游泳按键后 速度为0 使得惯性变小
        if (Input.GetKeyUp(KeyCode.Z))
        {
            isSwim = false;
            Vector2 velocity = rb.velocity;
            velocity.y = 0;
            rb.velocity = velocity;
        }

        
    }



    private void OnTriggerEnter2D(Collider2D other)
    {
        
        if(other.name=="Boundary")
            return;

        if (other.name == "DeathTrigger")
        {
            Debug.Log("DIE");
            isDead = true;
            GameController3.Instance.GameOver();
        }



        switch (other.tag)
        {
            case "Boom":
                GameController3.Instance.GameOver();
                break;

        }
        Destroy(other.gameObject);
    }
}
