using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyAtDoor : MonoBehaviour
{
    bool isDoorOpen = false;
    [SerializeField] GameObject door;
    float timer = 5;
    float currentTime = 0;
    SoundManager sound;

    void Start()
    {
        sound = FindFirstObjectByType<SoundManager>();
        isDoorOpen = door.transform.GetComponent<Animator>().GetBool("Door Open");
        if (!isDoorOpen)
        {
            StartCoroutine(TimerThing());
        }
    }

    void Update()
    {
        if(currentTime >= timer && isDoorOpen != true)
        {
            door.transform.GetComponent<Animator>().SetBool("Door Open", true);
            sound.PlaySound(sound.door_creak);
            isDoorOpen = true;
        }
    }

    IEnumerator TimerThing()
    {
        while (currentTime < timer)
        {
            if (!sound.IsSoundPlaying(sound.door_bash))
            {
                sound.PlaySound(sound.door_bash, 1);
            }
            currentTime += Time.deltaTime;
            yield return null;
        }
    }
}
