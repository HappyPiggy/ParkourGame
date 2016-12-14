using UnityEngine;
using System.Collections;

public class PlayerFly : MonoBehaviour {

    public float flyVelocity;
    public GoodsReward goodsReward;

    [HideInInspector]
    public bool isDead;

    public AudioClip coinAudio;
    public AudioClip jumpAudio;
    public AudioClip dieAudio;


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
            //播放跳的音效
            AudioController.Instance.PlayEfx(jumpAudio);
                Vector2 velocity = rb.velocity;
                velocity.y = flyVelocity;
                rb.velocity = velocity;

        }
    }


    public void Fly()
    {
        //播放跳的音效
        AudioController.Instance.PlayEfx(jumpAudio);
        Vector2 velocity = rb.velocity;
        velocity.y = flyVelocity;
        rb.velocity = velocity;
    }


    private void OnTriggerEnter2D(Collider2D other)
    {



        if (other.name == "Boundary" || other.CompareTag("Bubble"))
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
                GameController4.Instance.ChangeScore(goodsReward.coin1);
                break;
            case "Coin2":
                AudioController.Instance.PlayPropSource(coinAudio);
                GameController4.Instance.ChangeScore(goodsReward.coin2);
                break;
            case "Coin3":
                AudioController.Instance.PlayPropSource(coinAudio);
                GameController4.Instance.ChangeScore(goodsReward.coin3);
                break;

        }
        Destroy(other.gameObject);
    }


    void GameOver()
    {
        AudioController.Instance.StopBgMusic();
        AudioController.Instance.PlayDieSource(dieAudio);
        GameController4.Instance.GameOver();
    }
}
