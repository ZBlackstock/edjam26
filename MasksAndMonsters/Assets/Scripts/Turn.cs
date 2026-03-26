using UnityEngine;

public class Turn : MonoBehaviour
{
    private Animator anim;
    private SoundManager sound;

    void Awake()
    {
        anim = GetComponent<Animator>();
        sound = FindFirstObjectByType<SoundManager>();
    }

    public void TurnPlayer(int current, int destination)
    {
        anim.SetInteger("current", current);
        anim.SetInteger("destination", destination);
        anim.SetTrigger("turn");
    }
}
