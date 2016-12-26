
using UnityEngine;
using System.Collections;

public class BoundTrigger : MonoBehaviour {


    void OnTriggerExit2D(Collider2D other)
    {

        if (other.CompareTag("Player"))
            return;

        Transform parentTransform = null;
        Transform currentTransform = other.transform.parent;
        while (currentTransform != null)
        {
            parentTransform = currentTransform;
            currentTransform = currentTransform.parent;
           
        }

        if (parentTransform != null)
            Destroy(parentTransform.gameObject);

        //Debug.Log(parentTransform.name);
       

        Destroy(other.gameObject);
        

    }



    void OnTriggerEnter2D(Collider2D other)
    {

        if (other.name=="FirstRoad")
        {
            //计算后继Road的位置

            float localTargetPos = other.transform.parent.FindChild("Spawn").transform.localPosition.x;
            float selfPos = other.transform.position.x;
            float sizeOffset = other.transform.lossyScale.x * 2.6f;
            float target = localTargetPos + sizeOffset + selfPos;

           // float selfSize = other.transform.parent.GetComponent<RectTransform>().sizeDelta.x;
           // float selfPos = other.transform.parent.GetComponent<RectTransform>().position.x;
            Vector3 targetPos = new Vector3(target, -3f, 0);
          // Debug.Log(other.transform.parent.GetComponent<RectTransform>().position.x);
            GameController2.Instance.RandomCreateRoad(targetPos);
        }
        else if (other.name == "BOSSROAD")//else if (other.name == "BossRoad(Clone)")
        {

            Vector3 targetPos = new Vector3(other.transform.parent.position.x+12, -3, 0);
            if (!GameController2.Instance.bossRoadGenerate)
                targetPos = new Vector3(other.transform.parent.position.x + 20, -3, 0);
           // Debug.Log(targetPos);
            GameController2.Instance.RandomCreateRoad(targetPos);


        }

    

    }

}


