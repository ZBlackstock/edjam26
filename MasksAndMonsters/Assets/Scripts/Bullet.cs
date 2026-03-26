using UnityEngine;

public class Bullet : MonoBehaviour
{
    SoundManager sound;
    void Start()
    {
        Destroy(gameObject, 0.1f);
        sound = FindFirstObjectByType<SoundManager>();
        sound.StopSound(sound.death);
    }
}
