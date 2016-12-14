using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class StartScene : MonoBehaviour
{

    public AudioClip click;

    public void OnAccountLoginButtonClick()
    {
        AudioController.Instance.PlayEfx(click);
        SceneManager.LoadScene("MainScene");

    }

    public void OnVisitorButtonClick()
    {
        AudioController.Instance.PlayEfx(click);
    }
}
