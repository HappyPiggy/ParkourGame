using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController3 : MonoBehaviour {




    public GameObject[] Maps;
    public float roadOffset;
    public PlayerSwim player;
    public GameObject relivePanel;
    public Text distanceText;
    public int bgSpeedAcceration;//默认是8
    public float defaultSpeed; //默认是2

    [HideInInspector]
    public bool isPause;

    private float distance;
    private float distanceSpeed;
    private TextDisplay textDisplayer;
    private float Timer = 0;
    private CreateRoad2 createRoader;


    void Start()
    {
        isPause = false;
        textDisplayer = new TextDisplay();
        createRoader = new CreateRoad2();
        UpdateSpeed();
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
       // relivePanel.SetActive(false);
        isPause = false;
        player.transform.position = new Vector3(player.transform.position.x, 2.5f, player.transform.position.z);
        player.GetComponent<Rigidbody2D>().velocity = new Vector3(0, 4, 0);
        player.isDead = false;
        Time.timeScale = 1;

    }

    public void ReturnMainScene()
    {
        SceneManager.LoadScene("MainScene");
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

    public void RandomCreateRoad(Vector3 targetPos)
    {
        createRoader.CreateRandomRoad(Maps, targetPos, roadOffset);
    }
}
