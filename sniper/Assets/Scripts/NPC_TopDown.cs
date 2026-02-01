using System.Collections;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class NPC_TopDown : MonoBehaviour
{
    Rigidbody2D rb;
    public float moveSpeed = 200;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        StartCoroutine(NPC_Move());
    }

    float targetZ = 0;
    float startZ = 0;
    float duration = 0;
    float timer = 0;

    float x, y;

    private IEnumerator NPC_Move()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(1, 3));

            x = Random.Range(40f, 70);
            y = Random.Range(-9f, 10);

            timer = 0;
            duration = Random.Range(1f, 5);

            while (timer < duration)
            {
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(x, y, 0), moveSpeed * Time.deltaTime);

                timer += Time.deltaTime;
                yield return null;
            }
        }
    }
}
