using UnityEngine;
using UnityEngine.SceneManagement;

public class NPC_Death_Innocent : MonoBehaviour
{
    private SoundManager sound;
    void Start()
    {
        sound = FindFirstObjectByType<SoundManager>();
        sound.PlaySound(sound.death);
        Invoke(nameof(ParticleMove), 0.2f);
        Invoke(nameof(ResetGame), 3f);
    }
    private void ParticleMove()
    {
        transform.position = new Vector3(10000, 10000, 0);
    }
    private void ResetGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
