using UnityEngine;
using System.Collections;

public class BubbleController : MonoBehaviour
{
    public Vector3 spawnPosition;
    public float startTime;
    public float waitTime;
    public float nextTime;
    public int bubbleCount;

    public GameObject[] Bubbles;



    void Start()
    {
        StartCoroutine(SpawnBubbles());
    }

    IEnumerator SpawnBubbles()
    {

        yield return new WaitForSeconds(startTime);
        while (true)
        {
          //  print(spawnPosition.z);

            for (int i = 0; i < bubbleCount; i++)
            {
                GameObject bubble = Bubbles[Random.Range(0,Bubbles.Length)];
                Vector3 randomPos = new Vector3(Random.Range(spawnPosition.x-8, spawnPosition.x+8), spawnPosition.y, spawnPosition.z);
                Instantiate(bubble, randomPos, Quaternion.identity);
                yield return new WaitForSeconds(waitTime);
                
            }
           
            yield return new WaitForSeconds(nextTime);

        }



    }
}
