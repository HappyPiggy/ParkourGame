
using UnityEngine;


public class CreateRoad2 : MonoBehaviour {

    public void CreateRandomRoad(GameObject[] Maps, Vector3 targetPos, float roadOffset)
    {


        //if (!GameController2.Instance.bossRoadGenerate)
        //{
        //    int randomIndex = Random.Range(1, Maps.Length);
        //    Instantiate(Maps[randomIndex], targetPos + new Vector3(roadOffset, 0, 0), Quaternion.identity);
        //}
        //else
        //{
        //    Instantiate(Maps[0], targetPos + new Vector3(2, 3, 0), Quaternion.identity);
        //}


        int randomIndex = Random.Range(0, Maps.Length);
        Instantiate(Maps[randomIndex], targetPos + new Vector3(roadOffset, 0, 0), Quaternion.identity);

        //TODO
        //可能会有boss
    }
}
