using UnityEngine;

public class Turn : MonoBehaviour
{
    private Animator anim;
    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public void TurnPlayer(int current, int destination)
    {
        anim.SetInteger("current", current);
        anim.SetInteger("destination", destination);
        anim.SetTrigger("turn");
    }
}
