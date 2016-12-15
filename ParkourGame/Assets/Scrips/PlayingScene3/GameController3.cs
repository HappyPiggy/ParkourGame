
using UnityEngine;

using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController3 : MonoBehaviour {




    public GameObject[] Maps;
    public GameObject[] Characters;
    public float roadOffset;
    public GameObject relivePanel;
    public Text scoreText;
    public Text distanceText;
    public int bgSpeedAcceration;//默认是8
    public float defaultSpeed; //默认是2
    public Transform playerPos;

    public Text resDistance;
    public Text resScore;

    public Text resCoin;
    public Text resDia;

    public bool isRecord = true;


    [HideInInspector]
    public bool isPause;


    private float distance;
    private float distanceSpeed;
    private TextDisplay textDisplayer;
    private float Timer = 0;
    private CreateRoad2 createRoader;
    private int characterIndex;
    private GameObject currentPlayer;
         [HideInInspector]
    public PlayerSwim player;
    private int score;
    private int isSound = 1;


    void Start()
    {
        Time.timeScale = 1;
        isPause = false;
        textDisplayer = new TextDisplay();
        createRoader = new CreateRoad2();
        UpdateSpeed();

        characterIndex = PlayerPrefs.GetInt("playerIndex", 0);

        //生成主角
        currentPlayer = Instantiate(Characters[characterIndex], playerPos.transform.position, playerPos.transform.rotation) as GameObject;
        player = currentPlayer.GetComponent<PlayerSwim>();

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

    #region 单例模式
    private static GameController3 _instance;


    public static GameController3 Instance
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

    void Update()
    {
        if (!isPause)
        {
            ChangeDistace();
            AutoChangeSpeed();
        }

        GetRecord();
    }

    private void GetRecord()
    {
        if (player.isDead && isRecord)
        {

            resDistance.text = distance.ToString();
            resScore.text = score.ToString();

            resCoin.text = (score * 3f + distance * 2f).ToString();
            resDia.text = Random.Range(1, 10).ToString();

            isRecord = false;
        }
    }

    public void ChangeScore(int changeScore)
    {
        score += changeScore;
        textDisplayer.UpdateScore(scoreText, score);
    }


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
      //  bgSpeed = defaultSpeed * 4;
        distanceSpeed = defaultSpeed * 30;
        // scrollSpeed   = defaultSpeed * 2.5f;
    }


    public void PlayerSwimStart()
    {
        player.SwimStart();
    }

    public void PlayerSwimEnd()
    {
        player.SwimEnd();
    }

    public void ChangeDistace()
    {

        distance += Mathf.Ceil(Time.deltaTime * distanceSpeed);
        //print(distance);
        textDisplayer.UpdateDistance(distanceText, distance);
    }

    public void GameOver()
    {
        player.isDead = true;
        Invoke("WaitGameOver", 0.5f);
    }

    public void WaitGameOver()
    {
        relivePanel.SetActive(true);
        isPause = true;
        Time.timeScale = 0;
    }


    public void OnReliveButtonPress()
    {
        AudioController.Instance.PlayClick();
        AudioController.Instance.PlayBgMusic();
       // relivePanel.SetActive(false);
        currentPlayer.transform.position = new Vector3(playerPos.transform.position.x, playerPos.transform.position.y , 0);
        currentPlayer.GetComponent<Rigidbody2D>().velocity = new Vector3(0, 1, 0);
        isRecord = true;
        isPause = false;
        //player.transform.position = new Vector3(player.transform.position.x, 2.5f, player.transform.position.z);
        //player.GetComponent<Rigidbody2D>().velocity = new Vector3(0, 4, 0);
        player.isDead = false;
        Time.timeScale = 1;

    }

    public void ReturnMainScene()
    {
        AudioController.Instance.PlayClick();
        SceneManager.LoadScene("MainScene");
    }

    public void PauseGame()
    {
        AudioController.Instance.PlayClick();
        isPause = true;
        Time.timeScale = 0;
    }

    public void ReturnGame()
    {
        AudioController.Instance.PlayClick();
        isPause = false;
        Time.timeScale = 1;
    }

    public void RestartGame()
    {
        AudioController.Instance.PlayClick();
        SceneManager.LoadScene("PlayingScene3");
        Time.timeScale = 1;
    }

    public void RandomCreateRoad(Vector3 targetPos)
    {
        createRoader.CreateRandomRoad(Maps, targetPos, roadOffset);
    }


    public void PlayClickSound()
    {
        AudioController.Instance.PlayClick();
    }
}
