using UnityEngine;
using UnityEngine.UI;
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

        GameObject cam = FindFirstObjectByType<SniperMovement>().gameObject;
        cam.GetComponent<SniperMovement>().youFailed.SetActive(true);
        cam.GetComponent<SniperFire>().enabled = false;
        cam.GetComponent<SniperMovement>().enabled = false;

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
