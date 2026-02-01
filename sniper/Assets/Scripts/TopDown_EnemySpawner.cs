using UnityEngine;

public class TopDown_EnemySpawner : MonoBehaviour
{
    public bool spawn;
    public int number;
    public GameObject[] enemies;

    private void Update()
    {
        if (spawn)
        {
            SpawnEnemy(number);
            spawn = false;
        }
    }

    public void SpawnEnemy(int num)
    {
       float x = Random.Range(40f, 70);
       float y = Random.Range(4f, 10);

        Instantiate(enemies[num], new Vector2(x, y), Quaternion.identity);
    }
}
