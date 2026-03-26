using UnityEngine;

public class BulletDetect : MonoBehaviour
{
    public GameObject DeathFX_Innocent;
    public GameObject DeathFX_Enemy;
    public bool isEnemy;

    public void IsEnemy()
    {
        isEnemy = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet"))
        {
            Die();
        }
    }

    private void Die()
    {
        if (isEnemy)
        {
            if (DeathFX_Enemy != null)
            {
                Instantiate(DeathFX_Enemy, transform.position, Quaternion.identity);
            }
        }
        else
        {
            if (DeathFX_Innocent != null)
            {
                Instantiate(DeathFX_Innocent, transform.position, Quaternion.identity);
            }
        }
        Destroy(gameObject);
    }
}
