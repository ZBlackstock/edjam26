using System.Collections;
using UnityEngine;

public class BuildingNPCSpawner : MonoBehaviour
{
    public GameObject[] spawnpoints;
    public GameObject NPC;
    void Start()
    {
        StartCoroutine(SpawnNPCs());
    }

    IEnumerator SpawnNPCs()
    {
        float timer = 0;
        float waitTime = 0;

        while (true)
        {
            timer = 0;
            waitTime = Random.Range(1,30);
            while(timer < waitTime)
            {
                timer += Time.deltaTime;
                yield return null;
            }
            Instantiate(NPC, spawnpoints[Random.Range(0, spawnpoints.Length)].transform.position, Quaternion.identity);
        }
    }
}
