using UnityEngine;

public class BulletDetect : MonoBehaviour
{
    public GameObject DeathFX;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet"))
        {
            Die();
        }
    }

    private void Die()
    {
        Instantiate(DeathFX, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
