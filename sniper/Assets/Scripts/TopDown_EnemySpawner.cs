using UnityEngine;

public class TopDown_EnemySpawner : MonoBehaviour
{
    public bool spawn;
    public GameObject enemy;

    private void Update()
    {
        if (spawn)
        {
            SpawnEnemy();
            spawn = false;
        }
    }

    public void SpawnEnemy()
    {
       float x = Random.Range(40f, 70);
       float y = Random.Range(4f, 10);

        Instantiate(enemy, new Vector2(x, y), Quaternion.identity);
    }
}
