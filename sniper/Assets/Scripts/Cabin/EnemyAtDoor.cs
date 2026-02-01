using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyAtDoor : MonoBehaviour
{
    bool isDoorOpen = false;
    [SerializeField] GameObject door;
    float timer = 5;
    float currentTime = 0;

    float timeTillKill = 0;
    float killTime = 2;
    SoundManager sound;

    void Start()
    {
        sound = FindFirstObjectByType<SoundManager>();
        door = GameObject.Find("ShedDoor_0");
        isDoorOpen = door.transform.GetComponent<Animator>().GetBool("Door Open");
        if (!isDoorOpen)
        {
            StartCoroutine(TimerThing());
        }
        else
        {
            transform.GetComponent<BoxCollider2D>().enabled = true;
            sound.PlaySound(sound.death);
        }
    }

    void Update()
    {
        if (currentTime >= timer && isDoorOpen != true)
        {
            door.transform.GetComponent<Animator>().SetBool("Door Open", true);
            sound.PlaySound(sound.door_creak);
            isDoorOpen = true;
            transform.GetComponent<BoxCollider2D>().enabled = true;
            sound.PlaySound(sound.death);
        }
        else if (timeTillKill < killTime && isDoorOpen)
        {
            timeTillKill += Time.deltaTime;
        }
        else if(timeTillKill >= killTime)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    IEnumerator TimerThing()
    {
        while (currentTime < timer && !isDoorOpen)
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
