using UnityEngine;
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

    public void Test()
    {
        print("start script");
        AudioController.Instance.PlayClick();
    }
}
