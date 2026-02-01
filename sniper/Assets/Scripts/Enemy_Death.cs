using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemy_Death : MonoBehaviour
{
    private SoundManager sound;
    void Start()
    {
        sound = FindFirstObjectByType<SoundManager>();
     //   sound.PlaySound(sound.enemy_death);
        Invoke(nameof(ParticleMove), 0.2f);
        Destroy(gameObject, 2);
    }

    private void ParticleMove()
    {
        transform.position = new Vector3(10000, 10000, 0);
    }
}
