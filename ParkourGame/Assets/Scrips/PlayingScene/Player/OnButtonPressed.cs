

using UnityEngine;
using UnityEngine.EventSystems;

 
public class OnButtonPressed : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerExitHandler
{

    // 按钮是否是按下状态  
    private bool isSlider = false;
    private bool isRepeat = false;


    void Update()
    {
        // 如果按钮是被按下状态  
        if (isSlider)
        {

                GameController2.Instance.PlayerSliderStart();
        }
        else
        {
            if (!isRepeat)
            {
                GameController2.Instance.PlayerSliderEnd();
                isRepeat = true;
            }
            
        }

    }



    // 当按钮被按下后系统自动调用此方法  
    public void OnPointerDown(PointerEventData eventData)
    {
        AudioController.Instance.PlayClick();
        isSlider = true;
        isRepeat = false;
    }

    // 当按钮抬起的时候自动调用此方法  
    public void OnPointerUp(PointerEventData eventData)
    {
        isSlider = false;
    }

    // 当鼠标从按钮上离开的时候自动调用此方法  
    public void OnPointerExit(PointerEventData eventData)
    {
        isSlider = false;
    }
}
