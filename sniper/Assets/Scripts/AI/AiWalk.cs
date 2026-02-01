using System.Xml;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.UIElements;

[CreateAssetMenu(fileName = "AiWalk", menuName = "Scriptable Objects/AiWalk")]
public class AiWalk : ScriptableObject
{
    private Animator anim;
    public float getTarget;
    private float walkSpeed;
    Vector3 scaleX = new Vector3(0,0,0);

    private int moveDir = 1;
    public void Enter(AiController ai)
    {
        walkSpeed = ai.WalkSpeed;
        anim = ai.GetComponent<Animator>();
        scaleX = ai.transform.localScale;

        getTarget = Random.Range(-82f, 0f);

    }
    public LayerMask mask;
    public void WalkUp(AiController ai)
    {
        Vector2 direction = new Vector2(moveDir, 0);
        float distance = walkSpeed * Time.deltaTime;

        ContactFilter2D filter = new ContactFilter2D();
        filter.SetLayerMask(LayerMask.GetMask("Wall")); // only detect walls
        filter.useTriggers = false;

        RaycastHit2D[] results = new RaycastHit2D[1];
        Physics2D.Raycast(ai.transform.position, direction, filter, results, distance);

        if (ai.transform.position.x >= getTarget - 1 && ai.transform.position.x <= getTarget + 1)
        {
            EndState(ai);
        }
        else if (results[0].collider == null)
        {
            Move(ai);
        }
        else
        {
            FlipAI(ai);
            EndState(ai);
        }

        if (getTarget > ai.transform.position.x) // to the right
        {
            scaleX.x = 0.3f;
            ai.transform.localScale = scaleX;
            moveDir = 1;
        }
        else
        {
            scaleX.x = -0.3f;
            ai.transform.localScale = scaleX;
            moveDir = -1;
        }
    }

    private void Move(AiController ai)
    {
        anim.SetInteger("action", 0);
        ai.transform.position += new Vector3(walkSpeed * moveDir * Time.deltaTime, 0, 0);
    }

    private void FlipAI(AiController ai)
    {
        Vector3 scale = ai.transform.localScale;
        scale.x = scale.x * -1;
        ai.transform.localScale = scale;
        moveDir *= -1;
    }

    private void EndState(AiController ai)
    {

        int NextState = Random.Range(0, 101);

        if (NextState >= 0 && NextState <= 20)
        {
            ai.ChangeState(AiController.State.walking);
            Debug.Log("change set by walk " + ai.name);
        }
        else if (NextState >= 21 && NextState <= 60)
        {
            ai.ChangeState(AiController.State.loitering);
            Debug.Log("change set by walk " + ai.name);
        }
        else if (NextState >= 61 && NextState <= 100)
        {
            ai.ChangeState(AiController.State.sociliasing);
            Debug.Log("change set by walk" + ai.name);
        }
    }
}
