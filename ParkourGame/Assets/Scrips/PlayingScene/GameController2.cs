using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController2 : MonoBehaviour {

    public GameObject[] Maps;
    public float roadOffset;

    public int bgSpeedAcceration;//默认是8
    public float defaultSpeed; //默认是2



    public Text scoreText;
    public Text distanceText;
    public GameObject relivePanel;
    public GameObject directionButtons;
    public Player player;
    public Boss boss;
   // public GameObject bossobj;
   public GameObject bossTimeSlider;    
 //   public Boss boss;
  //  public Transform bossHomePos;
    public int firstBossDistance;
    public int bossDistance;
   // public int bossRoadDistance;

    [HideInInspector]
    public int[] correctKeys;
    [HideInInspector]
    public int[] playerKeys;
    [HideInInspector]
    public GameObject[] keysObj;
    [HideInInspector]
    public float bgSpeed;
    [HideInInspector]
    public bool isPause;
    [HideInInspector]
    public  bool bossRoadGenerate;
    [HideInInspector]
    public GameObject tempBossObj;

   

    private int score;
    private float distance;
    private float Timer = 0;
    private float distanceSpeed;
    
    private bool isRepeat;
    private int NextBossPos;
    private bool bossIsAppear;


    
    

    private CreateRoad createRoader;
    private TextDisplay textDisplayer;

    #region 单例模式
    private static GameController2 _instance;


    public static GameController2 Instance
    {
        get
        {
            return _instance;
        }
    }

    void Awake()
    {
        _instance = this;
    }
    #endregion


    void Start()
    {
        bossRoadGenerate = false;
        NextBossPos    = firstBossDistance;
        score          = 0;
        distance       = 0;
        isPause        = false;
        bossIsAppear    = false;
        createRoader   = new CreateRoad();
        textDisplayer  = new TextDisplay();
        Time.timeScale = 1;
        bossTimeSlider.GetComponent<Slider>().maxValue = player.attackTime;
        bossTimeSlider.GetComponent<Slider>().value = player.attackTime;
        UpdateSpeed();
        InitBossSettings();
    }


    void Update()
    {
    

        if (!isPause)
        {
            BossAppear();
            ChangeDistace();
            AutoChangeSpeed();
        }

    //  Debug.Log("实际值"+bossRoadGenerate);

        
    }

    #region 按钮事件

     public void PauseGame()
    {
        isPause        = true;
        Time.timeScale = 0;
    }

    public void ReturnGame()
    {
        isPause        = false;
        Time.timeScale = 1;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("PlayingScene");
        Time.timeScale = 1;
    }

    public void ReturnMainScene()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void GameOver()
    {
        player.isDead = true;
        directionButtons.SetActive(false);
        Invoke("WaitGameOver", 0.5f);
    }

    public void WaitGameOver()
    {
        relivePanel.SetActive(true);
        isPause = true;
        player.startTimer = false;
        Time.timeScale = 0;
    }

    public void OnReliveButtonPress()
    {
        player.Timer = 0;
        isPause = false;
        player.transform.position = new Vector3(player.transform.position.x, 5, player.transform.position.z);
        player.GetComponent<Rigidbody2D>().velocity = new Vector3(0, 10, 0);
        player.isDead = false;
        player.onGround = 1;
        Time.timeScale = 1;

        //如果boss存在 就重新开始计时
        if (tempBossObj != null)
        {
            directionButtons.SetActive(true);
            player.startTimer = true;
           // Debug.Log("startTimer开始计时了！！");
        }
            

       // bossTimerImg.GetComponent<Slider>().value = player.attackTime;
    }

    #endregion

    #region UI相关
  

    public void ChangeDistace()
    {

        distance += Mathf.Ceil(Time.deltaTime * distanceSpeed) ;
        textDisplayer.UpdateDistance(distanceText, distance);
    }

    public void ChangeScore(int changeScore)
    {
        score += changeScore;
        textDisplayer.UpdateScore(scoreText, score);
    }
    #endregion

    #region 距离相关
    private void AutoChangeSpeed()
    {
        Timer += Time.deltaTime;
        // double time = Math.Ceiling(Timer);
        if (defaultSpeed <= 4.0f)
        {
            if (bgSpeedAcceration == (int)Timer)
            {
                defaultSpeed += 0.05f;
                Timer = 0;
            }
        }

        UpdateSpeed();
    }

    void UpdateSpeed()
    {
        bgSpeed = defaultSpeed * 4;
        distanceSpeed = defaultSpeed * 30;
        // scrollSpeed   = defaultSpeed * 2.5f;
    }

    public void RandomCreateRoad(Vector3 targetPos)
    {
        createRoader.CreateRandomRoad(Maps, targetPos, roadOffset);
    }


 
    #endregion

    #region Boss相关


    public void SetRoadGenerate(bool bl)
    {
        bossRoadGenerate = bl;
    }

    public void BossDamage()
    {
        tempBossObj.GetComponent<Boss>().isDamage = true;

    }

    public void BossOver()
    {
        directionButtons.SetActive(false);
        bossTimeSlider.SetActive(false);
        tempBossObj.GetComponent<Boss>().isDie = true;
        NextBossPos = (int)distance + (int)(bossDistance * 0.5f * defaultSpeed);
        isRepeat = false;
       // bossRoadGenerate = false;
        player.startTimer = false;
       // Debug.Log(player.startTimer);
       
    }

    void BossAppear()
    {
        if (distance >= NextBossPos - 1000 && !isRepeat)
        {
         //   Debug.Log(NextBossPos - 1000);
            bossRoadGenerate = true;
        }


        if (distance >= NextBossPos && !isRepeat)
        {
           // Debug.Log("test");
            bossIsAppear = true;
        }


        if (bossIsAppear)
        {
            bossIsAppear = false;
            isRepeat = true;
            InitBossSettings();
            tempBossObj = Instantiate(boss.gameObject, boss.homePos.position,boss.homePos.rotation) as GameObject;
            bossTimeSlider.SetActive(true);
           // tempBossObj.GetComponent<Boss>().isDie = false;
           // tempBossObj.GetComponent<Boss>().timeSliderIsAppear = true;
            directionButtons.SetActive(true);
        }
    }

    void InitBossSettings()
    {
        //初始化键位和boss物体
        correctKeys = new int[10];
        playerKeys = new int[10];
        keysObj = new GameObject[10];


    }
    #endregion
}
