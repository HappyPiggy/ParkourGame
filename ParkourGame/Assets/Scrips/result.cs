
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class result : MonoBehaviour
{

    public Text resDistance;
    public Text resScore;

    public Text resCoin;
    public Text resDia;

    public bool isRecord=true;



	void Update () {
        if (GameController2.Instance.player.isDead&& isRecord)
	    {
            
	        resDistance.text = GameController2.Instance.distanceText.text;
	        resScore.text = GameController2.Instance.scoreText.text;

            resCoin.text = (int.Parse(GameController2.Instance.scoreText.text) * 2.5f + int.Parse(GameController2.Instance.distanceText.text)).ToString();
            resDia.text = Random.Range(1,10).ToString();

            isRecord = false;
	    }
        else if(GameController3.Instance.player.isDead&& isRecord)
        {
            resDistance.text = GameController3.Instance.distanceText.text;
	        resScore.text = GameController3.Instance.scoreText.text;

            resCoin.text = (int.Parse(GameController3.Instance.scoreText.text) * 2.5f + int.Parse(GameController3.Instance.distanceText.text)).ToString();
            resDia.text = Random.Range(1,10).ToString();

            isRecord = false; 
        }
        else if (GameController4.Instance.player.isDead && isRecord)
        {
            resDistance.text = GameController4.Instance.distanceText.text;
            resScore.text = GameController4.Instance.scoreText.text;

            resCoin.text = (int.Parse(GameController4.Instance.scoreText.text) * 2.5f + int.Parse(GameController4.Instance.distanceText.text)).ToString();
            resDia.text = Random.Range(1, 10).ToString();

            isRecord = false;
        }

	}
}
