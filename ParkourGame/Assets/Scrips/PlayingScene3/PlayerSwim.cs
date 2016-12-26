
using UnityEngine;


public class PlayerSwim : MonoBehaviour
{

    public float swimVelocity;
    public GoodsReward goodsReward;
    public AudioClip coinAudio;
    public AudioClip jumpAudio;
    public AudioClip dieAudio;


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
            AudioController.Instance.PlayEfx(jumpAudio);

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


    public void SwimStart()
    {
        if (!isDead)
        {
            isSwim = true;
            AudioController.Instance.PlayEfx(jumpAudio);
            if (isSwim)
            {
                Vector2 velocity = rb.velocity;
                velocity.y = swimVelocity;
                rb.velocity = velocity;
                // print(velocity.y);
            }

        }
    }

    public void SwimEnd()
    {
        isSwim = false;
        Vector2 velocity = rb.velocity;
        velocity.y = 0;
        rb.velocity = velocity;
    }



    private void OnTriggerEnter2D(Collider2D other)
    {

       

        if(other.name=="Boundary" ||other.CompareTag("Bubble"))
            return;

        if (other.name == "DeathTrigger")
        {
            Debug.Log("DIE");
            isDead = true;
            GameOver();
            return;
        }



        switch (other.tag)
        {
            case "Boom":
                GameOver();
                break;
            case "Coin1":
                AudioController.Instance.PlayPropSource(coinAudio);
                GameController3.Instance.ChangeScore(goodsReward.coin1);
                break;
            case "Coin2":
                AudioController.Instance.PlayPropSource(coinAudio);
                GameController3.Instance.ChangeScore(goodsReward.coin2);
                break;
            case "Coin3":
                AudioController.Instance.PlayPropSource(coinAudio);
                GameController3.Instance.ChangeScore(goodsReward.coin3);
                break;

        }
        Destroy(other.gameObject);
    }


    void GameOver()
    {
        AudioController.Instance.StopBgMusic();
        AudioController.Instance.PlayDieSource(dieAudio);
        GameController3.Instance.GameOver();
    }
}
