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
            }
         
        }

        if (Input.GetKeyUp(KeyCode.Z))
        {
            isSwim = false;
        }

        
    }



    private void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.name == "DeathTrigger")
        {
            Debug.Log("DIE");
            isDead = true;
            GameController3.Instance.GameOver();
        }
    }
}
