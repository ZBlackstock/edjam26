using System.Data.Common;
using UnityEngine;

[CreateAssetMenu(fileName = "AiSoc", menuName = "Scriptable Objects/AiSoc")]
public class AiSoc : ScriptableObject
{
    public bool isPaired = false;
    public int FloorID = 0;
    public AiController TargetAi;
    public AiController PossibleAi;
    public float searchDuration = 6f;
    public float searchRadius = 100;

    private float timer;
    public void Enter(AiController ai)
    {
        timer = searchDuration;
        isPaired = false;
        TargetAi = null;
        FloorID = ai.FloorID;
        TargetAi = null;
}

    public void SocUp(AiController ai)
    {
        ai.GetComponent<Animator>().SetInteger("action", -1);
        Debug.Log("looking for partner "+ai.name);
        timer -= Time.deltaTime;

        Collider2D[] hits = Physics2D.OverlapCircleAll(
            ai.transform.position,
            searchRadius
            );
        foreach (var hit in hits)
        {

            AiController other = hit.GetComponent<AiController>();

            if (other == null) 
            {

                continue;
            }

            if (other == ai) continue;
            // Conditions

            if (other.FloorID != FloorID) continue;

            if (other.state != ai.state) continue;
            if (other.aiSoc.isPaired != false) continue;
            if (other == ai) continue;

            Debug.Log("foundPartner: "+ai.name +" "+hit.name);
            // Pair up
            isPaired = true;
            TargetAi = other;

            other.aiSoc.isPaired = true;
            other.aiSoc.TargetAi = ai;


            // Optional: face each other / stop movement
            ai.ChangeState(AiController.State.paired);
            other.ChangeState(AiController.State.paired);

            return;
        }

        // Time expired ? fallback
        if (timer <= 0f)
        {
            Debug.Log("Failed to find partner " + ai.name);
            ai.ChangeState(AiController.State.loitering);
            Debug.Log("change set by soc" + ai.name);
        }
    }

}
