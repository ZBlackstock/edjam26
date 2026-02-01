using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

[CreateAssetMenu(fileName = "NewScriptableObjectScript", menuName = "Scriptable Objects/NewScriptableObjectScript")]
public class AiMeet : ScriptableObject
{
    public float walkSpeed;
    public AiController target;
    private int MoveDir;
    float thisAI;
    private Animator anim;

    float OtherAI;
    public void Enter(AiController ai,AiController _target)
    {
        walkSpeed = ai.WalkSpeed;
        target = _target;
        thisAI = ai.transform.position.x;
        anim = ai.GetComponent<Animator>();
        OtherAI = target.transform.position.x;

    }

    public void PairedUp(AiController ai)
    {
        getDirection(ai);

        if (distance(ai) > 3) 
        {
            Move(ai);
        }
        else
        {
            ai.ChangeState(AiController.State.loitering);
        }
        
       
    }

    private void Move(AiController ai)
    {
        anim.SetInteger("action", 0);
        ai.transform.position += new Vector3(walkSpeed * MoveDir * Time.deltaTime, 0, 0);
        Debug.Log("travelling as "+ ai.name + " " + new Vector3(walkSpeed * MoveDir * Time.deltaTime, 0, 0));
    }

    private void getDirection(AiController ai)
    {
        Vector3 scaleX = new Vector3(0, 0, 0);
        scaleX = ai.transform.localScale;


        if (thisAI <= OtherAI)
        {
            MoveDir = 1;
            scaleX.x = 0.3f;
            ai.transform.localScale = scaleX;
        }
        else
        {
            MoveDir = -1;
            scaleX.x = -0.3f;
            ai.transform.localScale = scaleX;
        }
        Debug.Log("AI "+ai.name+ " going direction of travel to other ai = " + MoveDir + " target X = " + OtherAI + " startloc X = " + thisAI);
    }

    private float distance(AiController ai)
    {
        Debug.Log("distance to between targets = " + Mathf.Abs(ai.transform.position.x - target.transform.position.x));
        return Mathf.Abs(ai.transform.position.x - target.transform.position.x);

    }
}
