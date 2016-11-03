using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Boss : MonoBehaviour {

    public GameObject[] bossKeys;
    public Transform[] keySpawns;
   // public GameObject bossTimeSlider; 

    public Transform targetPos;
    public Transform homePos;
  //  public GameObject bossTimeSlider;  

    public float smoothing;

    [HideInInspector]
    public bool isDie;
    [HideInInspector]
    public bool isDamage;
    [HideInInspector]
    public bool timeSliderIsAppear;

    //[HideInInspector]
   // public  int[] correctKeys;

    private Rigidbody2D rb;
    private Slider timeSlider;
    private Animator animator;


    //生成boss按键

    //boss出现

    //boss掉血效果  x:9.5~11.5 y:0.5~1


    void Start()
    {
        isDie     = false;
        isDamage  = false;
        rb        = GetComponent<Rigidbody2D>();
        timeSlider = GameController2.Instance.bossTimeSlider.GetComponent<Slider>();
        animator = GetComponent<Animator>();
        timeSliderIsAppear = true;
        GenerateRandomKeys();
    }

    void Update()
    {
       // BossAppear();
       
      //  GetDamage();

        if (isDamage)
        {
            isDamage = false;
            GetDamage();
        }

        if (!isDie)
        {

            BossAppear();
       
        }
        else {

            //播放死亡动画
            animator.SetBool("Die", isDie);
          //  isDamage = true;
            BossDisappear();
            Invoke("DestorySelf", 2);
        }

    }
    private void DestorySelf()
    {
        Destroy(gameObject);
    }

    public void GetDamage()
    {
       // Debug.Log("test");
        //播放受伤动画
        animator.SetTrigger("GetHurt");
        rb.MovePosition(Vector2.Lerp(transform.position, transform.position + new Vector3(10, -10, 0), smoothing * Time.deltaTime));
    }



    public void BossAppear()
    {
        //倒计时器和boss移动到指定位置
        timeSlider.value = GameController2.Instance.player.attackTime - GameController2.Instance.player.Timer;
        rb.MovePosition(Vector2.Lerp(transform.position, targetPos.position, smoothing * Time.deltaTime));

        if (Mathf.Abs(transform.position.x - targetPos.position.x) <= 1.5f) 
        {
            GameController2.Instance.player.startTimer = true;
           // Debug.Log("111");
        }

    }

    public void BossDisappear()
    {
        rb.MovePosition(Vector2.Lerp(transform.position, homePos.position, smoothing * Time.deltaTime));
       // bossAlive = false;
    }

  public  void GenerateRandomKeys()
    {
        int i = 0;
        foreach (Transform keySpawn in keySpawns)
        {
            int randomIndex = Random.Range(0, bossKeys.Length);
            GameObject go = Instantiate(bossKeys[randomIndex], keySpawn.position, keySpawn.rotation) as GameObject;
            go.transform.SetParent(transform);
            GameController2.Instance.keysObj[i] = go;
            GameController2.Instance.correctKeys[i] = randomIndex;
            i++;

        }
    }
}
