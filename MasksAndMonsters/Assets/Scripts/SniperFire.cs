using System.Collections;
using UnityEngine;

public class SniperFire : MonoBehaviour
{
    private InputManager input;
    private Transform trans;
    public SpriteRenderer sniper;
    public SpriteRenderer handGun;
    bool canFire = true;
    private SoundManager sound;
    public GameObject fire;

    void Start()
    {
        input = FindFirstObjectByType<InputManager>();
        trans = transform;
        sound = FindFirstObjectByType<SoundManager>();

    }

    // Update is called once per frame
    void Update()
    {
        if (input.attackAction.WasPressedThisFrame() && canFire)
        {
            if (sniper.enabled)
            {
                StartCoroutine(Fire_Sniper());
            }
            else
            {
                StartCoroutine(Fire_HandGun());
            }
        }
    }

    private IEnumerator Fire_Sniper()
    {
        canFire = false;
        float elapsed = 0;
        float dur = 0.06f;
        float z = -25;
        float fireZ = -40;
        float curZ = z;

        Instantiate(fire, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);
        sound.PlaySound(sound.gunshot_Sniper);

        while (trans.position.z > fireZ)
        {
            curZ = Mathf.Lerp(z, fireZ, elapsed / dur);
            transform.position = new Vector3(trans.position.x, trans.position.y, curZ);
            elapsed += Time.deltaTime;
            yield return null;
        }
        transform.position = new Vector3(trans.position.x, trans.position.y, fireZ);

        curZ = fireZ;
        elapsed = 0;

        while (trans.position.z < z)
        {
            curZ = Mathf.Lerp(fireZ, z, elapsed / dur);
            transform.position = new Vector3(trans.position.x, trans.position.y, curZ);
            elapsed += Time.deltaTime;
            yield return null;
        }
        transform.position = new Vector3(trans.position.x, trans.position.y, z);

        yield return new WaitForSeconds(0.6f);
        canFire = true;
    }

    private IEnumerator Fire_HandGun()
    {
        canFire = false;
        float elapsed = 0;
        float dur = 0.06f;
        float z = -20;
        float fireZ = -30;
        float curZ = z;
        sound.PlaySound(sound.gunshot_Handgun);
        Instantiate(fire, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);
        handGun.transform.GetChild(0).gameObject.SetActive(true);
        while (trans.position.z > fireZ)
        {
            curZ = Mathf.Lerp(z, fireZ, elapsed / dur);
            transform.position = new Vector3(trans.position.x, trans.position.y, curZ);
            elapsed += Time.deltaTime;
            yield return null;
        }
        transform.position = new Vector3(trans.position.x, trans.position.y, fireZ);

        curZ = fireZ;
        elapsed = 0;
        handGun.transform.GetChild(0).gameObject.SetActive(false);

        while (trans.position.z < z)
        {
            curZ = Mathf.Lerp(fireZ, z, elapsed / dur);
            transform.position = new Vector3(trans.position.x, trans.position.y, curZ);
            elapsed += Time.deltaTime;
            yield return null;
        }
        transform.position = new Vector3(trans.position.x, trans.position.y, z);

        yield return new WaitForSeconds(0.6f);
        canFire = true;
    }
}
