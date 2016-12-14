using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
/// <summary>  
/// 脚本位置：UGUI按钮组件身上  
/// 脚本功能：实现按钮长按状态的判断  
/// 创建时间：2015年11月17日  
/// </summary>  

// 继承：按下，抬起和离开的三个接口  
public class OnButtonPressed2 : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerExitHandler
{

    // 按钮是否是按下状态  
    private bool isSwim = false;
    private bool isRepeat = false;


    void Update()
    {
        // 如果按钮是被按下状态  
        if (isSwim)
        {

            GameController3.Instance.PlayerSwimStart();
        }
        else
        {
            if (!isRepeat)
            {
                GameController3.Instance.PlayerSwimEnd();
                isRepeat = true;
            }

        }

    }



    // 当按钮被按下后系统自动调用此方法  
    public void OnPointerDown(PointerEventData eventData)
    {
        AudioController.Instance.PlayClick();
        isSwim = true;
        isRepeat = false;
    }

    // 当按钮抬起的时候自动调用此方法  
    public void OnPointerUp(PointerEventData eventData)
    {
        isSwim = false;
    }

    // 当鼠标从按钮上离开的时候自动调用此方法  
    public void OnPointerExit(PointerEventData eventData)
    {
        isSwim = false;
    }
}
