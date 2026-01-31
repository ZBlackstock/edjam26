using UnityEditor.UIElements;
using UnityEngine;

public class AiController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    [SerializeField] public int FloorID;
    [SerializeField] public float WalkSpeed = 2f;
    public enum State
    {
        walking,
        loitering,
        sociliasing,
        Attack
    }


    private State state;

    private AiWalk aiWalk;
    private AiLoiter aiLoiter;
    private AiSoc aiSoc;
    private AiAttack aiAttack;

    private void Awake()
    {
        aiWalk = new AiWalk();
        aiLoiter = new AiLoiter();
        aiSoc = new AiSoc();
        aiAttack = new AiAttack();
        aiWalk.Enter(this);
    }
    // Update is called once per frame
    void Update()
    {
        print(state);
        switch(state)
        {
            case State.walking: aiWalk.WalkUp(this); break;
            case State.loitering: break;
        }
    }

    public void ChangeState(State NewState)
    {
        state = NewState;
        switch (state)
        {
            case State.walking: aiWalk.Enter(this); break;
            case State.loitering: aiLoiter.Enter(this); break;
            case State.sociliasing: aiLoiter.Enter(this); break;
        }
    }

}
