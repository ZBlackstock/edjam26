using System.Collections;
using System.Dynamic;
using UnityEngine;

public class AiController : MonoBehaviour
{
    public Sprite[] masks = new Sprite[10];
    public SpriteRenderer sr;
    int spriteIndex;
    [SerializeField] public int FloorID;
    [SerializeField] public float WalkSpeed = 2f;
    float waitTimer = 0;
    public enum State
    {
        walking,
        loitering,
        sociliasing,
        attack,
        paired
    }

    private void Start()
    {
        spriteIndex = Random.Range(0, 10);
        sr.sprite = masks[spriteIndex];
        waitTimer = Random.Range(5, 15);
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

        isEnemy = Random.Range(1, 3) == 1;

        if (isEnemy)
        {
            GetComponent<BulletDetect>().IsEnemy();

        }
    }
    // Update is called once per frame

    void Update()
    {
        waitTimer -= Time.deltaTime;
        if (isEnemy)
        {
            if (Vector2.Distance(sr.transform.position, Camera.main.transform.position) < 2 || waitTimer < 0)
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

        if (waitTimer > 0)
        {
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

        FindFirstObjectByType<TopDown_EnemySpawner>().SpawnEnemy(spriteIndex);
        Destroy(gameObject);
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
