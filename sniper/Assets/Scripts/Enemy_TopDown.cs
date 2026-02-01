using System.Collections;
using UnityEngine;

public class Enemy_TopDown : MonoBehaviour
{
    public Vector3 targetPos;
    public GameObject FX_DoorReached;

    public float moveSpeed = 200;
    float duration = 0;
    float timer = 0;
    Vector3 startPos;
    SoundManager sound;

    private void Awake()
    {
        sound = FindFirstObjectByType<SoundManager>();
    }

    void Start()
    {
        StartCoroutine(NPC_Move());
        sound.PlaySound(sound.enemy_outside0);
        sound.PlaySound(sound.enemy_outside1);
    }

    private IEnumerator NPC_Move()
    {
        timer = 0;
        startPos = transform.position;

        duration = Random.Range(3f, 6);

        while (!Mathf.Approximately(transform.position.x, targetPos.x) && !Mathf.Approximately(transform.position.y, targetPos.y))
        {
            transform.position = new Vector3(Mathf.Lerp(startPos.x, targetPos.x, timer / duration),
                Mathf.Lerp(startPos.y, targetPos.y, timer / duration), 0);

            Vector2 direction = targetPos - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
            transform.rotation = Quaternion.Euler(0f, 0f, angle);

            timer += Time.deltaTime;

            if (timer > duration)
            {
                break;
            }
            yield return null;
        }
        Instantiate(FX_DoorReached, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
