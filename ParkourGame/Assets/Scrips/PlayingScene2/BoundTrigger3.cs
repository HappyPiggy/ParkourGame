

using UnityEngine;


public class BoundTrigger3 : MonoBehaviour {


    void OnTriggerEnter2D(Collider2D other)
    {

        if (other.CompareTag("Player") || other.CompareTag("Border"))
            return;


        if (other.name == "First")
        {
            //计算后继Road的位置
            //   print(other.transform.parent.name);
            float localTargetPos = other.transform.parent.FindChild("Spawn").transform.localPosition.x;
            float selfPos = other.transform.position.x;
            float target = localTargetPos + selfPos;

            Vector3 targetPos = new Vector3(target, 0, 0);
            // Debug.Log(other.transform.parent.GetComponent<RectTransform>().position.x);
            // print(targetPos);
            GameController4.Instance.RandomCreateRoad(targetPos);
        }




    }
}
