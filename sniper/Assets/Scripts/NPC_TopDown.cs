using System.Collections;
using System.Threading;
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

    float duration = 0;
    float timer = 0;
    private Vector3 targetPos;
    float x, y;
    Vector3 startPos;

    private IEnumerator NPC_Move()
    {
        while (true)
        {
            timer = 0;
            while (timer < Random.Range(2f, 5f))
            {
                timer += Time.deltaTime;
                yield return null;
            }

            x = Random.Range(40f, 70);
            y = Random.Range(-9f, 10);
            targetPos = new Vector3(x, y, 0);

            startPos = transform.position;

            timer = 0;
            duration = Random.Range(5f, 6);

            while (transform.position != targetPos)
            {
                transform.position = new Vector3(Mathf.Lerp(startPos.x, targetPos.x, timer / duration),
                    Mathf.Lerp(startPos.y, targetPos.y, timer / duration), 0);

                Vector2 direction = targetPos - transform.position;
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
                transform.rotation = Quaternion.Euler(0f, 0f, angle);

                timer += Time.fixedDeltaTime;
                yield return new WaitForFixedUpdate();
            }
        }
    }
}
