using UnityEngine;
using System.Collections;
using UnityEditor;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[System.Serializable]
public class GoodsReward
{
	public int coin1, coin2, coin3;
}

public class Player : MonoBehaviour
{

	public float jumpVelocity;
	public int jumpTimes;
	public GoodsReward goodsReward;
    public float attackTime;

    public AudioClip coinAudio;
    public AudioClip jumpAudio;
    public AudioClip dieAudio;
    public AudioClip click;


    [HideInInspector]
    public bool isDead;
    [HideInInspector]
    public float Timer;
    [HideInInspector]
    public int onGround;
    [HideInInspector]
    public bool startTimer;
    [HideInInspector]
    public int currentKey;


	private Animator animator;
	private Rigidbody2D rigidbody2D;

    public bool isTouch;
    private bool isSlider;
    private bool isJump;
    private BoxCollider2D boxCollider;
    private Vector2 currentColliderSize;
    private Vector2 currentColliderOffset;
    private GameObject runEffect;


   



	// Use this for initialization
	void Start ()
	{
	    isSlider = false;
	    isJump = false;
        isTouch = false;
        startTimer = false;
        currentKey = 0;
		animator    = GetComponent<Animator>();
		rigidbody2D = GetComponent<Rigidbody2D>();
        boxCollider=GetComponent<BoxCollider2D>();
		onGround    = 1;
		isDead      = false;
        currentColliderOffset=new Vector2(boxCollider.offset.x,boxCollider.offset.y);
        currentColliderSize=new Vector2(boxCollider.size.x,boxCollider.size.y);

        runEffect = transform.FindChild("EffectPos").gameObject;
	    // attackTime = GameController2.Instance.bossTimeSlider.GetComponent<Slider>().maxValue;
	    //  Debug.Log(attackTime);


	}

	void Update()
	{
		//人物跳
		PlayerMovement();

        //攻击Boss
        AttackBoss();


	}






    void AttackBoss()
    {



    
        //在按对第1个键的时候就结束生成boss路 以免过长
        if (currentKey >= 1)
        {

            GameController2.Instance.SetRoadGenerate(false);
            //  GameController2.Instance.bossRoadGenerate = false;
            //Debug.Log("success!");
        }

        //Debug.Log("Player中的值:" + GameController2.Instance.bossRoadGenerate);

        if (currentKey >= 4)
        {
            GameController2.Instance.BossOver();
            currentKey = 0;
            Timer = 0;
        }




        //每按下一次按键 出发一次
        if (isTouch)
        {
            //对比正确顺序和Player顺序
            isTouch = false;
            if (GameController2.Instance.playerKeys[currentKey] == GameController2.Instance.correctKeys[currentKey])
            {
                if(currentKey<=2)
                     GameController2.Instance.BossDamage();
                Destroy(GameController2.Instance.keysObj[currentKey]);
                currentKey++;
            }
            else
            {
                Debug.Log("按错键了！");

                GameOver();
            }
        }






        //检测按下第一个按钮后的时间  boss完全出现后才开始计时
        if (GameController2.Instance.tempBossObj != null && startTimer)
        {
            if (!GameController2.Instance.tempBossObj.GetComponent<Boss>().isDie && !isDead)
            {
                Timer += Time.deltaTime;
              //  Debug.Log("test");
            }

            if (Timer >= attackTime && !isDead)
            {
                Debug.Log("时间到了！");

                GameOver();
            }
        }
        
    }


  //public  void OnLeftPress()
  //  {
  //    print("test");

  //      GameController2.Instance.playerKeys[currentKey] = 0;
  //      isTouch                                         =true;
  //  }

  //public void OnRightPress()
  //  {
  //      GameController2.Instance.playerKeys[currentKey] = 1;
  //      isTouch                                         =true;
  //  }
   public void Jump()
    {
        AudioController.Instance.PlayEfx(click);
        if ( onGround <= jumpTimes && !GameController2.Instance.isPause && !isJump)
        {
            //特效消除
            runEffect.SetActive(false);

            //播放跳的音效
            AudioController.Instance.PlayEfx(jumpAudio);

            isSlider = true;
            Vector2 velocity = rigidbody2D.velocity;
            velocity.y = jumpVelocity;
            rigidbody2D.velocity = velocity;
            if (onGround >= 2)
            {
                animator.SetTrigger("Jump");
            }
            onGround++;
        }
    }

   public void SliderStart()
    {
        if ( !GameController2.Instance.isPause && !isSlider)
        {
            //蹲的动画
            animator.SetBool("Slider", true);
            //  transform.Rotate(new Vector3(0, 0, 90));
            boxCollider.size = new Vector2(5, 1.6f);
            boxCollider.offset = new Vector2(0, -0.4f);

            //蹲的时候不能起跳
            isJump = true;

            //蹲的特效要改变位置
            runEffect.SetActive(false);

        }
    }


   public void SliderEnd()
   {
       animator.SetBool("Slider", false);

       boxCollider.size = currentColliderSize;
       boxCollider.offset = currentColliderOffset;

       isJump = false;

       runEffect.SetActive(true);
   }




   public void PlayerMovement()
   {
       if (Input.GetKeyDown(KeyCode.Z) && onGround <= jumpTimes && !GameController2.Instance.isPause && !isJump)
       {
           //特效消除
           runEffect.SetActive(false);
           AudioController.Instance.PlayEfx(jumpAudio);

           isSlider = true;
           Vector2 velocity = rigidbody2D.velocity;
           velocity.y = jumpVelocity;
           rigidbody2D.velocity = velocity;
           if (onGround >= 2)
           {
               animator.SetTrigger("Jump");
           }
           onGround++;
       }


       if (Input.GetKeyDown(KeyCode.X) && !GameController2.Instance.isPause && !isSlider)
       {

           //蹲的动画
           animator.SetBool("Slider", true);
           //  transform.Rotate(new Vector3(0, 0, 90));
           boxCollider.size = new Vector2(5, 1.6f);
           boxCollider.offset = new Vector2(0, -0.4f);

           //蹲的时候不能起跳
           isJump = true;

           //蹲的特效要改变位置
           runEffect.SetActive(false);

       }

       if (Input.GetKeyUp(KeyCode.X))
       {



           animator.SetBool("Slider", false);
           //transform.Rotate(new Vector3(0, 0, 0));
           //boxCollider.size = new Vector2(1.23f, 3.25f);
           //boxCollider.offset = new Vector2(0.92f,0);

           boxCollider.size = currentColliderSize;
           boxCollider.offset = currentColliderOffset;

           isJump = false;

           runEffect.SetActive(true);

       }
   }

	private void OnCollisionEnter2D(Collision2D col)
	{
		//如果Player碰到地面 则弹跳次数恢复
		if (col.collider.tag == "Road")
		{
			onGround = 1;
		}
        //碰到地时才可以下蹲
	    isSlider = false;

        if(!isJump)
            runEffect.SetActive(true);

	}






	void OnTriggerEnter2D(Collider2D other)
	{
        //Debug.Log(other.name);
        if (other.name == "DeathTrigger")
        {
           Debug.Log("DIE");
            isDead = true;

            GameOver();
        }


        if (other.CompareTag("Bound"))
            return;

   

		switch (other.tag)
		{
			case "Coin1":
                AudioController.Instance.PlayPropSource(coinAudio);
				GameController2.Instance.ChangeScore(goodsReward.coin1);
				break;
			case "Coin2":
                AudioController.Instance.PlayPropSource(coinAudio);
				GameController2.Instance.ChangeScore(goodsReward.coin2);
				break;
			case "Coin3":
                AudioController.Instance.PlayPropSource(coinAudio);
				GameController2.Instance.ChangeScore(goodsReward.coin3);
				break;
            case "Snake":
		        GameOver();
                break;
            case "Boom":
		        GameOver();
                break;

		}
		Destroy(other.gameObject);
	

	}

    void GameOver()
    {
        AudioController.Instance.StopBgMusic();
        AudioController.Instance.PlayDieSource(dieAudio);
        GameController2.Instance.GameOver();
    }


}
