using UnityEngine;

public class Bullet : MonoBehaviour
{
    void Start()
    {
        Destroy(gameObject, 0.15f);
    }
}
