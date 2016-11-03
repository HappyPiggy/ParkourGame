using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TextDisplay : MonoBehaviour {


    public void UpdateScore(Text scoreText, int score)
    {
        scoreText.text = score + "";
    }

    public void UpdateDistance(Text distanceText, float distance)
    {
        distanceText.text = distance + "";
    }
    
}
