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

        FindFirstObjectByType<SniperFire>().enabled = false;
        FindFirstObjectByType<SniperMovement>().enabled = false;
    }
    private void ParticleMove()
    {
        transform.position = new Vector3(10000, 10000, 0);
        FindAnyObjectByType<FadeBlack>().visible = true;
    }
    private void ResetGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
