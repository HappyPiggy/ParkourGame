using UnityEngine;
using System.Collections;

public class PlayerFly : MonoBehaviour {

    public float flyVelocity;
    public GoodsReward goodsReward;

    [HideInInspector]
    public bool isDead;


    private Rigidbody2D rb;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        isDead = false;

        //初始停留
        Vector2 velocity = rb.velocity;
        velocity.y = flyVelocity;
        rb.velocity = velocity;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z) && !isDead)
        {
                Vector2 velocity = rb.velocity;
                velocity.y = flyVelocity;
                rb.velocity = velocity;

        }
    }



    private void OnTriggerEnter2D(Collider2D other)
    {



        if (other.name == "Boundary" || other.CompareTag("Bubble"))
            return;

        if (other.name == "DeathTrigger")
        {
            Debug.Log("DIE");
            isDead = true;
            GameController4.Instance.GameOver();
            return;
        }



        switch (other.tag)
        {
            case "Boom":
                
                GameController4.Instance.GameOver();
                break;
            case "Coin1":
                GameController4.Instance.ChangeScore(goodsReward.coin1);
                break;
            case "Coin2":
                GameController4.Instance.ChangeScore(goodsReward.coin2);
                break;
            case "Coin3":
                GameController4.Instance.ChangeScore(goodsReward.coin3);
                break;

        }
        Destroy(other.gameObject);
    }
}
