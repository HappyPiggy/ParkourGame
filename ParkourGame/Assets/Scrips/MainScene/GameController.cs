﻿using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{

    public GameObject dailyMission;
    public GameObject achieveMission;
    public GameObject storyPanel;
    public GameObject stylePanel;
    public GameObject elementPanel;
    public GameObject coinShop;
    public GameObject DiamondShop;
    public GameObject CakeShop;

    public GameObject[] enoughList;
    public GameObject[] menuItemList;
    public GameObject[] panels;
    public GameObject MainPanels;
    public GameObject  CenterAndDown;
    public GameObject ReadyPanel1;


    public GameObject[] CharactersList;
    public GameObject headIcon;
    public AudioClip click;

    private string playerIndex="2";
    private int LEVEL=0;

    private static GameController _instance;
    private int isSound = 1;

    public static GameController Instance
    {
        get { return _instance; }
    }


    void Start()
    {
        playerIndex = PlayerPrefs.GetInt("playerIndex", 0).ToString();
        isSound=PlayerPrefs.GetInt("isSound", 1);

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




    public void SoundSetting()
    {


        if (GameObject.Find("bgx").GetComponent<Toggle>().isOn)
        {
           //打开音效
            
            GameObject.Find("Main Camera").GetComponent<AudioListener>().enabled = true;
            PlayerPrefs.SetInt("isSound", 1);
        }
        else
        {
            //关闭音效
            GameObject.Find("Main Camera").GetComponent<AudioListener>().enabled = false;
            PlayerPrefs.SetInt("isSound", 0);
        }
       
    }

    #region UI界面设置


    public void RefreshCharacterSelected()
    {

        AudioController.Instance.PlayEfx(click);
       
        int selectd = PlayerPrefs.GetInt("playerIndex", 0);
        print(selectd);

        CharactersList[selectd].transform.Find("0").gameObject.SetActive(false);
        CharactersList[selectd].transform.Find("1").gameObject.SetActive(true);

        for (int i = 0; i < 4; i++)
        {
            if (i == selectd)
                continue;
            CharactersList[i].transform.Find("0").gameObject.SetActive(true);
            CharactersList[i].transform.Find("1").gameObject.SetActive(false);
        }

    }

   public void OnCharacterPanelCloseBtnClick()
    {
        AudioController.Instance.PlayEfx(click);
       switch (int.Parse(playerIndex))
       {
           case 0:
               headIcon.GetComponent<Image>().overrideSprite = Resources.Load("gui", typeof(Sprite)) as Sprite;
               break;
           case 1:
               headIcon.GetComponent<Image>().overrideSprite = Resources.Load("hu", typeof(Sprite)) as Sprite;
               break;
           case 2:
               headIcon.GetComponent<Image>().overrideSprite = Resources.Load("tu", typeof(Sprite)) as Sprite;
               break;
           case 3:
               headIcon.GetComponent<Image>().overrideSprite = Resources.Load("ya", typeof(Sprite)) as Sprite;
               break;

       }
    }


    public void OnTortoiseBtnClick()
    {
        AudioController.Instance.PlayEfx(click);

        //传值使用 记录所选择
        int currentIndex = 0;
        playerIndex = currentIndex.ToString();
        CharactersList[currentIndex].transform.Find("0").gameObject.SetActive(false);
        CharactersList[currentIndex].transform.Find("1").gameObject.SetActive(true);

        for (int i = 0; i < 4; i++)
        {
            if(i==currentIndex)
                continue;
            CharactersList[i].transform.Find("0").gameObject.SetActive(true);
            CharactersList[i].transform.Find("1").gameObject.SetActive(false);
        }
        PlayerPrefs.SetInt("playerIndex", currentIndex);
    }

    public void OnFoxBtnClick()
    {
        AudioController.Instance.PlayEfx(click);

        int currentIndex = 1;
        playerIndex = currentIndex.ToString();
        CharactersList[currentIndex].transform.Find("0").gameObject.SetActive(false);
        CharactersList[currentIndex].transform.Find("1").gameObject.SetActive(true);

        for (int i = 0; i < 4; i++)
        {
            if (i == currentIndex)
                continue;
            CharactersList[i].transform.Find("0").gameObject.SetActive(true);
            CharactersList[i].transform.Find("1").gameObject.SetActive(false);
        }
        PlayerPrefs.SetInt("playerIndex", currentIndex);
    }

    public void OnRabbitBtnClick()
    {
        AudioController.Instance.PlayEfx(click);

        int currentIndex = 2;
        playerIndex = currentIndex.ToString();
        CharactersList[currentIndex].transform.Find("0").gameObject.SetActive(false);
        CharactersList[currentIndex].transform.Find("1").gameObject.SetActive(true);

        for (int i = 0; i < 4; i++)
        {
            if (i == currentIndex)
                continue;
            CharactersList[i].transform.Find("0").gameObject.SetActive(true);
            CharactersList[i].transform.Find("1").gameObject.SetActive(false);
        }
        PlayerPrefs.SetInt("playerIndex", currentIndex);
    }

    public void OnDuckBtnClick()
    {
        AudioController.Instance.PlayEfx(click);

        int currentIndex = 3;
        playerIndex = currentIndex.ToString();
        CharactersList[currentIndex].transform.Find("0").gameObject.SetActive(false);
        CharactersList[currentIndex].transform.Find("1").gameObject.SetActive(true);

        for (int i = 0; i < 4; i++)
        {
            if (i == currentIndex)
                continue;
            CharactersList[i].transform.Find("0").gameObject.SetActive(true);
            CharactersList[i].transform.Find("1").gameObject.SetActive(false);
        }
        PlayerPrefs.SetInt("playerIndex", currentIndex);
    }

    public void DailyPanel()
    {
        AudioController.Instance.PlayEfx(click);
        dailyMission.SetActive(true);
        achieveMission.SetActive(false);
    }

    public void AchievePanel()
    {
        AudioController.Instance.PlayEfx(click);
        dailyMission.SetActive(false);
        achieveMission.SetActive(true);
    }

    public void DisplayStory()
    {
        AudioController.Instance.PlayEfx(click);
        storyPanel.SetActive(true);
        stylePanel.SetActive(false);
        elementPanel.SetActive(false);
    }

    public void DisplayStyle()
    {
        AudioController.Instance.PlayEfx(click);
        storyPanel.SetActive(false);
        stylePanel.SetActive(true);
        elementPanel.SetActive(false);
    }

    public void DisplayElement()
    {
        AudioController.Instance.PlayEfx(click);
        storyPanel.SetActive(false);
        stylePanel.SetActive(false);
        elementPanel.SetActive(true);
    }

    public void DisplayCoinShop()
    {
        AudioController.Instance.PlayEfx(click);
        coinShop.SetActive(true);
        DiamondShop.SetActive(false);
        CakeShop.SetActive(false);
    }

    public void DisplayDiamondShop()
    {
        AudioController.Instance.PlayEfx(click);
        coinShop.SetActive(false);
        DiamondShop.SetActive(true);
        CakeShop.SetActive(false);
    }

    public void DisplayCakeShop()
    {
        AudioController.Instance.PlayEfx(click);
        coinShop.SetActive(false);
        DiamondShop.SetActive(false);
        CakeShop.SetActive(true);
    }

    public void OnClosePanels()
    {
        for (int i = 0; i < panels.Length; i++)
            panels[i].SetActive(false);
        MainPanels.SetActive(true);

        //是个小bug..
       // SceneManager.LoadScene("MainScene");
        AudioController.Instance.PlayEfx(click);

    }

    public void OnOpenPanels()
    {

        if (isSound != 1)
        {
            GameObject.Find("bgx").GetComponent<Toggle>().isOn = false;
        }

        AudioController.Instance.PlayEfx(click);
        MainPanels.SetActive(false);

    }


    public void TESTFORAUDIO()
    {
        AudioController.Instance.PlayEfx(click);
    }



    #endregion

    #region UI数据交互

    //检测3种物品状态
    public void DiamondIsEnough()
    {
        //campare player.diamond to need to pay diamond
        //if player.diamond < current diamond
        enoughList[0].SetActive(true);
    }

    public void RMBIsEnough()
    {
       
        enoughList[1].SetActive(true);
    }

    public void CoinIsEnough()
    {
       
        enoughList[2].SetActive(true);
    }


    public void ClassicsMode()
    {
        LEVEL = 0;
    }

    public void SkyMode()
    {
        LEVEL = 1;
    }

    public void SeaMode()
    {
        LEVEL = 2;
    }

    public void StartGame()
    {

        AudioController.Instance.PlayEfx(click);
        PlayerPrefs.SetInt("playerIndex", int.Parse(playerIndex));

        switch (LEVEL)
        {
            case  0:
                SceneManager.LoadScene("PlayingScene");
                break;
            case 1:
                SceneManager.LoadScene("PlayingScene2");
                break;
            case 2:
                SceneManager.LoadScene("PlayingScene3");
                break;
        }
       

       
        
    }

    #endregion



}
