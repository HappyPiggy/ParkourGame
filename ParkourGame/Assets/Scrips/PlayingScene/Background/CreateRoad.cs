using UnityEngine;
using System.Collections;

public class CreateRoad : MonoBehaviour
{

    public void CreateRandomRoad(GameObject[] Maps, Vector3 targetPos, float roadOffset)
    {
       // Debug.Log("生成路的值" + GameController2.Instance.bossRoadGenerate);

        if (!GameController2.Instance.bossRoadGenerate)
        {
            int randomIndex = Random.Range(1, Maps.Length);
            Instantiate(Maps[randomIndex], targetPos + new Vector3(roadOffset, 0, 0), Quaternion.identity);
        }
        else
        {
            Instantiate(Maps[0], targetPos+new Vector3(2,3,0) , Quaternion.identity);
        }
        
      //  Instantiate(Maps[1], targetPos + new Vector3(roadOffset, 0, 0), Quaternion.identity);
    }

}
  
