using UnityEngine;

public class FadeBlack : MonoBehaviour
{
    public bool visible;
    private Animator anim;
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetBool("visible", visible);
    }
}
