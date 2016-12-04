using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController4 : MonoBehaviour {


    public GameObject[] Maps;
    public GameObject[] Characters;
    public float roadOffset;
    public int bgSpeedAcceration;//默认是8
    public float defaultSpeed; //默认是2
    public Text distanceText;
    public Text scoreText;
    public GameObject relivePanel;
    public Transform playerPos;
    public Text resDistance;
    public Text resScore;

    public Text resCoin;
    public Text resDia;
    public bool isRecord = true;

    private TextDisplay textDisplayer;
    private CreateRoad2 createRoader;


    [HideInInspector]
    public bool isPause;

    private PlayerFly player;
    private int characterIndex;
    private GameObject currentPlayer;
    private float distanceSpeed;
    private float distance;
    private float Timer = 0;
    private int score;

    #region 单例模式
    private static GameController4 _instance;


    public static GameController4 Instance
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
	
	void Start () {

        Time.timeScale = 1;
        textDisplayer = new TextDisplay();
        createRoader = new CreateRoad2();

        characterIndex = PlayerPrefs.GetInt("playerIndex", 0);

        //生成主角
        currentPlayer = Instantiate(Characters[characterIndex], playerPos.transform.position, playerPos.transform.rotation) as GameObject;
        player = currentPlayer.GetComponent<PlayerFly>();
        
	}

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


    public void ChangeScore(int changeScore)
    {
        score += changeScore;
        textDisplayer.UpdateScore(scoreText, score);
    }

    public void ChangeDistace()
    {

        distance += Mathf.Ceil(Time.deltaTime * distanceSpeed);
        //print(distance);
        textDisplayer.UpdateDistance(distanceText, distance);
    }

    public void RandomCreateRoad(Vector3 targetPos)
    {
        createRoader.CreateRandomRoad(Maps, targetPos, roadOffset);
    }

    public void GameOver()
    {
    
        
       // player.isDead = true;
        Invoke("WaitGameOver", 0.5f);
    }

    public void WaitGameOver()
    {
        relivePanel.SetActive(true);
        isPause = true;
        Time.timeScale = 0;
    }

    public void PauseGame()
    {
        isPause = true;
        Time.timeScale = 0;
    }

    public void ReturnGame()
    {
        isPause = false;
        Time.timeScale = 1;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("PlayingScene2");
        Time.timeScale = 1;
    }

    public void ReturnMainScene()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void OnReliveButtonPress()
    {
        // relivePanel.SetActive(false);
        currentPlayer.transform.position = new Vector3(playerPos.transform.position.x, playerPos.transform.position.y, 0);
        currentPlayer.GetComponent<Rigidbody2D>().velocity = new Vector3(0, 10, 0);
        isRecord = true;
        isPause = false;
        //player.transform.position = new Vector3(player.transform.position.x, 2.5f, player.transform.position.z);
        //player.GetComponent<Rigidbody2D>().velocity = new Vector3(0, 4, 0);
        player.isDead = false;
        Time.timeScale = 1;

    }
  
}
