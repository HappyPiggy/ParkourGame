using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController2 : MonoBehaviour {

    public GameObject[] Maps;
    public GameObject[] Characters;
    public Transform playerPos;
    public float roadOffset;

    public int bgSpeedAcceration;//默认是8
    public float defaultSpeed; //默认是2



    public Text scoreText;
    public Text distanceText;
    public GameObject relivePanel;
    public GameObject directionButtons;
    
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
     [HideInInspector]
    public Player player;


     public Text resDistance;
     public Text resScore;

     public Text resCoin;
     public Text resDia;

     public bool isRecord = true;

    private int score;
    private float distance;
    private float Timer = 0;
    private float distanceSpeed;
    
    private bool isRepeat;
    private int NextBossPos;
    private bool bossIsAppear;
    private int characterIndex;
    private GameObject currentPlayer;
    private int isSound = 1;

    
    

    private CreateRoad createRoader;
    private TextDisplay textDisplayer;

    private string playerIndex;

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

       
    //   print(playerIndex);

        
        //确定选择的角色
         characterIndex = PlayerPrefs.GetInt("playerIndex", 0);

        //  characterIndex = 3;

        //生成主角
       currentPlayer= Instantiate(Characters[characterIndex], playerPos.transform.position, playerPos.transform.rotation) as GameObject;


        //生成主角脚本
        player = currentPlayer.GetComponent<Player>();
        bossTimeSlider.GetComponent<Slider>().maxValue = player.attackTime;
        bossTimeSlider.GetComponent<Slider>().value = player.attackTime;
        UpdateSpeed();
        InitBossSettings();

        isSound = PlayerPrefs.GetInt("isSound", 1);

        //判断音乐有没有打开
        if (isSound == 1)
        {
            GameObject.Find("Main Camera").GetComponent<AudioListener>().enabled = true;
        }
        else
        {

            GameObject.Find("Main Camera").GetComponent<AudioListener>().enabled = false;
        }

    }


    void Update()
    {
    

        if (!isPause)
        {
            BossAppear();
            ChangeDistace();
            AutoChangeSpeed();
        }


        //获得奖励结果
        //GetRecord();
        
    }


  

    private void GetRecord()
    {   
        if (player.isDead && isRecord)
        {

            resDistance.text = distance.ToString();
            resScore.text =score.ToString();

            resCoin.text = (score*3f + distance*2f).ToString();
            resDia.text = Random.Range(1, 10).ToString();
;
            isRecord = false;
        }
    }

    #region 按钮事件

    public void PlayerJump()
    {
        player.Jump();
    }

    public void PlayerSliderStart()
    {
        player.SliderStart();
    }

    public void PlayerSliderEnd()
    {
        player.SliderEnd();
    }
     public void PauseGame()
    {

        AudioController.Instance.PlayClick();
        isPause        = true;
        Time.timeScale = 0;
    }

    public void ReturnGame()
    {
        AudioController.Instance.PlayClick();
        isPause        = false;
        Time.timeScale = 1;
    }

    public void RestartGame()
    {
        AudioController.Instance.PlayClick();
        SceneManager.LoadScene("PlayingScene");
        Time.timeScale = 1;
    }

    public void ReturnMainScene()
    {
        AudioController.Instance.PlayClick();
        SceneManager.LoadScene("MainScene");
    }

    public void GameOver()
    {
        player.isDead = true;
        GetRecord();
        directionButtons.SetActive(false);
        Invoke("WaitGameOver", 0.5f);
    }

    public void WaitGameOver()
    {
        relivePanel.SetActive(true);
        isPause = true;
        player.startTimer = false;
      //  Destroy(currentPlayer);
        Time.timeScale = 0;
    }

    public void OnReliveButtonPress()
    {

        AudioController.Instance.PlayClick();
       AudioController.Instance.PlayBgMusic();
        //currentPlayer =Instantiate(Characters[characterIndex], playerPos.transform.position, playerPos.transform.rotation) as GameObject;
        //player = currentPlayer.GetComponent<Player>();
        currentPlayer.transform.position = new Vector3(playerPos.transform.position.x,playerPos.transform.position.y+3,0);
        currentPlayer.GetComponent<Rigidbody2D>().velocity = new Vector3(0, 10, 0);
      //  print(playerPos.transform.position);
        player.Timer = 0;
        isRecord = true;
        isPause = false;
       // player.transform.position = new Vector3(player.transform.position.x, 5, player.transform.position.z);
       // Instantiate(Characters[characterIndex], playerPos.transform.position, playerPos.transform.rotation);
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
    public void EnterKey()
    {
        AudioController.Instance.PlayClick();
    }

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


    public void OnLeftPress()
    {
       // print("test");

       playerKeys[player.currentKey] = 0;
       player.isTouch = true;
    }

    public void OnRightPress()
    {
        playerKeys[player.currentKey] = 1;
        player.isTouch = true;
    }

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
