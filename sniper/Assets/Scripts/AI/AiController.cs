using System.Collections;
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

    public bool isEnemy;
    private void Awake()
    {
        state = State.loitering;
        aiWalk = new AiWalk();
        aiLoiter = new AiLoiter();
        aiSoc = new AiSoc();
        aiAttack = new AiAttack();
        aiMeet = new AiMeet();
        aiSoc.Enter(this);

        isEnemy = Random.Range(1, 5) == 1;
    }
    // Update is called once per frame
    void Update()
    {
        if (isEnemy)
        {

            if (2 < Vector2.Distance(transform.position, Camera.main.transform.position))
            {
                if (!attackSequenceStarted)
                {
                    StartCoroutine(WaitThenMaybeAttack());
                }
            }
        }


        switch (state)
        {
            case State.walking: aiWalk.WalkUp(this); break;
            case State.loitering: aiLoiter.LoiterUP(this); break;
            case State.sociliasing: aiSoc.SocUp(this); break;
            case State.paired: aiMeet.PairedUp(this); break;
        }
    }
    bool attackSequenceStarted;
    private IEnumerator WaitThenMaybeAttack()
    {
        attackSequenceStarted = true;
        float time = Random.Range(0.5f, 3f);
        float timer = 0;

        while (timer < time)
        {
            timer += Time.deltaTime;
            if (3 > Vector2.Distance(transform.position, Camera.main.transform.position))
            {
                StopCoroutine(WaitThenMaybeAttack());
            }
            yield return null;
        }

    }

    public void ChangeState(State NewState)
    {
        Debug.Log("changing state of " + this.name + " to " + NewState);
        state = NewState;
        switch (state)
        {
            case State.walking: aiWalk.Enter(this); break;
            case State.loitering: aiLoiter.Enter(this); break;
            case State.sociliasing: aiSoc.Enter(this); break;
            case State.paired: aiMeet.Enter(this, aiSoc.TargetAi); break;
        }
    }

}
