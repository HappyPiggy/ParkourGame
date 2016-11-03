using UnityEngine;
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

    private static GameController _instance;

    public static GameController Instance
    {
        get { return _instance; }
    }
    

    #region UI界面设置
    public void DailyPanel()
    {
        dailyMission.SetActive(true);
        achieveMission.SetActive(false);
    }

    public void AchievePanel()
    {
        dailyMission.SetActive(false);
        achieveMission.SetActive(true);
    }

    public void DisplayStory()
    {
        storyPanel.SetActive(true);
        stylePanel.SetActive(false);
        elementPanel.SetActive(false);
    }

    public void DisplayStyle()
    {
        storyPanel.SetActive(false);
        stylePanel.SetActive(true);
        elementPanel.SetActive(false);
    }

    public void DisplayElement()
    {
        storyPanel.SetActive(false);
        stylePanel.SetActive(false);
        elementPanel.SetActive(true);
    }

    public void DisplayCoinShop()
    {
        coinShop.SetActive(true);
        DiamondShop.SetActive(false);
        CakeShop.SetActive(false);
    }

    public void DisplayDiamondShop()
    {
        coinShop.SetActive(false);
        DiamondShop.SetActive(true);
        CakeShop.SetActive(false);
    }

    public void DisplayCakeShop()
    {
        coinShop.SetActive(false);
        DiamondShop.SetActive(false);
        CakeShop.SetActive(true);
    }

    public void OnClosePanels()
    {
      //  for (int i = 0; i < panels.Length; i++)
       //     panels[i].SetActive(false);
      //  MainPanels.SetActive(true);
        SceneManager.LoadScene("MainScene");
    }

    public void OnOpenPanels()
    {
      
        MainPanels.SetActive(false);

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


    public void StartGame()
    {
        SceneManager.LoadScene("PlayingScene");
        
        
    }

    #endregion



}
