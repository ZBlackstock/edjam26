using UnityEngine;

public class Enemy_DoorReached : MonoBehaviour
{
    private SoundManager sound;
    void Start()
    {
        sound = FindFirstObjectByType<SoundManager>();
        sound.PlaySound(sound.enemy_enterBuilding);
        Invoke(nameof(ParticleMove), 0.2f);
        Destroy(gameObject, 2);
    }

    private void ParticleMove()
    {
        transform.position = new Vector3(10000, 10000, 0);
    }
}
