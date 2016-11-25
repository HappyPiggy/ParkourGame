using UnityEngine;
using System.Collections;
using Microsoft.Win32;

public class PlayerSwim : MonoBehaviour
{

    public float swimVelocity;
    public GoodsReward goodsReward;


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

       

        if(other.name=="Boundary" ||other.CompareTag("Bubble"))
            return;

        if (other.name == "DeathTrigger")
        {
            Debug.Log("DIE");
            isDead = true;
            GameController3.Instance.GameOver();
            return;
        }



        switch (other.tag)
        {
            case "Boom":
                GameController3.Instance.GameOver();
                break;
            case "Coin1":
                GameController3.Instance.ChangeScore(goodsReward.coin1);
                break;
            case "Coin2":
                GameController3.Instance.ChangeScore(goodsReward.coin2);
                break;
            case "Coin3":
                GameController3.Instance.ChangeScore(goodsReward.coin3);
                break;

        }
        Destroy(other.gameObject);
    }
}
