using System.Dynamic;
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
        attack,
        paired
    }


    public State state;

    private AiWalk aiWalk;
    private AiLoiter aiLoiter;
    public AiSoc aiSoc;
    private AiAttack aiAttack;
    private AiMeet aiMeet;

    
    private void Awake()
    {
        state = State.loitering;
        aiWalk =  new AiWalk();
        aiLoiter = new AiLoiter();
        aiSoc = new AiSoc();
        aiAttack = new AiAttack();
        aiMeet = new AiMeet();
        aiSoc.Enter(this);
    }
    // Update is called once per frame
    void Update()
    {
        switch(state)
        {
            case State.walking: aiWalk.WalkUp(this); break;
            case State.loitering: aiLoiter.LoiterUP(this); break;
            case State.sociliasing: aiSoc.SocUp(this); break;
            case State.paired: aiMeet.PairedUp(this); break;
        }
    }

    public void ChangeState(State NewState)
    {
        Debug.Log("changing state of " + this.name+ " to "+ NewState);
        state = NewState;
        switch (state)
        {
            case State.walking: aiWalk.Enter(this); break;
            case State.loitering: aiLoiter.Enter(this); break;
            case State.sociliasing: aiSoc.Enter(this); break;
            case State.paired: aiMeet.Enter(this,aiSoc.TargetAi); break;
        }
    }

}
