using UnityEngine;
using System.Collections;

public class AudioController : MonoBehaviour
{


    public AudioSource bgSource;
    public AudioSource dieSource;
    public AudioSource efxSource;
    public AudioSource propSource;
    public AudioSource clickSource;




    #region 单例模式
    private static AudioController _instance;


    public static AudioController Instance
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

    public  void PlayEfx(AudioClip clip)
    {
        efxSource.clip = clip;
        efxSource.Play();
    }

    public void PlayClick()
    {
        clickSource.Play();
    }

    public void PlayDieSource(AudioClip clip)
    {
        dieSource.clip = clip;
        dieSource.Play();
    }

    public  void PlayPropSource(AudioClip clip)
    {
        dieSource.clip = clip;
        dieSource.Play();
    }

    public void StopBgMusic()
    {
        bgSource.Stop();
    }

    public void PlayBgMusic()
    {
        bgSource.Play();
    }

}
