using System.Collections;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "AiLoiter", menuName = "Scriptable Objects/AiWalk")]
public class AiLoiter : ScriptableObject
{
    float loiterTimer;
    float loiterDuration;
    int loiterPhase;
    public void Enter (AiController ai)
    {
        loiterPhase = 0;
        loiterTimer = 0f;
    }

    public void LoiterUP(AiController ai)
    {
        loiterTimer -= Time.deltaTime;

        switch (loiterPhase)
        {
            case 0: // Start loiter
                ai.GetComponent<Animator>().SetInteger("action", Random.Range(3, 5));

                loiterTimer = 2f;
                loiterPhase = 1;
                break;

            case 1: // Stop animation, wait
                if (loiterTimer > 0f) return;

                ai.GetComponent<Animator>().SetInteger("action", -1);

                loiterDuration = Random.Range(2f, 6f);
                loiterTimer = loiterDuration;
                loiterPhase = 2;
                break;

            case 2: // Decide next state
                if (loiterTimer > 0f) return;

                Debug.Log("goal reached");

                int NextState = Random.Range(0, 101);

                if (NextState <= 60)
                {
                    ai.ChangeState(AiController.State.walking);
                    Debug.Log("change set by loiter " + ai.name);
                }
                else
                {
                    ai.ChangeState(AiController.State.sociliasing);
                    Debug.Log("change set by loiter " + ai.name);
                }

                loiterPhase = 0; // reset for next time
                break;
        }
    }

}
