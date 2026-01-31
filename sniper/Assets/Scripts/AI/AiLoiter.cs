using System.Collections;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "AiLoiter", menuName = "Scriptable Objects/AiWalk")]
public class AiLoiter : ScriptableObject
{

    public void Enter (AiController ai)
    {
        ai.StartCoroutine(DoThing(ai));
    }

    private IEnumerator DoThing(AiController ai)
    {
        ai.GetComponent<Animator>().SetInteger("action", Random.Range(3, 5));
        yield return new WaitForSeconds(0.1f);
        ai.GetComponent<Animator>().SetInteger("action", -1);
        yield return new WaitForSeconds(Random.Range(2f, 6));

        Debug.Log("goal reached");
        int NextState = Random.Range(0, 101);

        if (NextState >= 0 && NextState <= 60)
        {
            ai.ChangeState(AiController.State.walking);
        }
        else if (NextState >= 61 && NextState <= 100)
        {
            ai.ChangeState(AiController.State.sociliasing);

        }
    }

}
