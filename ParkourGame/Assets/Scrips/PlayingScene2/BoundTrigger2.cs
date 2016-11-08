using UnityEngine;
using System.Collections;

public class BoundTrigger2 : MonoBehaviour {



    void OnTriggerEnter2D(Collider2D other)
    {

        if (other.CompareTag("Player") || other.CompareTag("Border"))
            return;

        print(other.name);
        if (other.name == "First")
        {
            //计算后继Road的位置
           // print("test");
            float localTargetPos = other.transform.parent.FindChild("Spawn").transform.localPosition.x;
            float selfPos = other.transform.position.x;
            float target = localTargetPos +  selfPos;

            Vector3 targetPos = new Vector3(target, 0, 0);
            // Debug.Log(other.transform.parent.GetComponent<RectTransform>().position.x);
            GameController3.Instance.RandomCreateRoad(targetPos);
        }




    }
}
