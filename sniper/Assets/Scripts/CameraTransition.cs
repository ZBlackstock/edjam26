using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class CameraTransition : MonoBehaviour
{
    InputManager input;
    private Transform currentTP;
    private int currentTP_Index;
    [SerializeField] private Transform[] TPs; // 0 - building, 1 - street, 2 - room
    [SerializeField] private SpriteRenderer sniper;
    [SerializeField] private SpriteRenderer handgun;
    [SerializeField] private GameObject postProcessing;
    public Turn turn;
    private SoundManager sound;

    private void Awake()
    {
        input = FindFirstObjectByType<InputManager>();
        sound = FindFirstObjectByType<SoundManager>();
    }

    void Start()
    {
        currentTP = TPs[0]; // Set to aim sniper at building on start
        sound.PlaySound(sound.woosh_ToBuilding);
        sound.PlaySoundLoop(sound.ambience_Building, 1);
        sound.PlaySoundLoop(sound.ambience_Party, 1);
        sound.PlaySoundLoop(sound.wind_Building, 1);
    }

    void Update()
    {
        if (input.moveUpAction.WasPressedThisFrame())
        {
            print("Building");
            if (currentTP != TPs[0])
            {
                SetAim(TPs[0], true);
                turn.TurnPlayer(currentTP_Index, 0);
                currentTP_Index = 0;
                sound.PlaySound(sound.woosh_ToBuilding);
                sound.PlaySoundLoop(sound.ambience_Building, 1);
                sound.PlaySoundLoop(sound.ambience_Party, 1);
                sound.PlaySoundLoop(sound.wind_Building, 1);

                sound.StopSound(sound.ambience_Street);
                sound.StopSound(sound.wind_Street);

                sound.StopSound(sound.ambience_Room);
            }
        }
        else if (input.moveDownAction.WasPressedThisFrame())
        {
            print("Street");
            if (currentTP != TPs[1])
            {
                SetAim(TPs[1], true);
                turn.TurnPlayer(currentTP_Index, 1);
                currentTP_Index = 1;
                sound.PlaySound(sound.woosh_ToStreet);
                sound.PlaySoundLoop(sound.ambience_Street, 1);
                sound.PlaySoundLoop(sound.wind_Street, 1);


                sound.StopSound(sound.ambience_Building);
                sound.StopSound(sound.ambience_Party);
                sound.StopSound(sound.wind_Building);

                sound.StopSound(sound.ambience_Room);
            }
        }
        else if (input.moveLeftAction.WasPressedThisFrame() || input.moveRightAction.WasCompletedThisFrame())
        {
            if (currentTP != TPs[2])
            {
                print("Room");
                SetAim(TPs[2], false);
                turn.TurnPlayer(currentTP_Index, 2);
                currentTP_Index = 2;
                sound.PlaySound(sound.woosh_ToRoom);
                sound.PlaySoundLoop(sound.ambience_Room, 1);

                sound.StopSound(sound.ambience_Street);
                sound.StopSound(sound.wind_Street);

                sound.StopSound(sound.ambience_Building);
                sound.StopSound(sound.ambience_Party);
                sound.StopSound(sound.wind_Building);
            }
            else
            {
                SetAim(TPs[0], true);
                turn.TurnPlayer(currentTP_Index, 0);
                currentTP_Index = 0;
                sound.PlaySound(sound.woosh_ToBuilding);
                sound.PlaySoundLoop(sound.ambience_Building, 1);
                sound.PlaySoundLoop(sound.ambience_Party, 1);
                sound.PlaySoundLoop(sound.wind_Building, 1);

                sound.StopSound(sound.ambience_Street);
                sound.StopSound(sound.wind_Street);

                sound.StopSound(sound.ambience_Room);
            }
        }
    }

    private void SetAim(Transform tp, bool useSniper)
    {
        currentTP = tp;
        transform.position = currentTP.position;

        sniper.enabled = useSniper;
        handgun.enabled = !useSniper;
        postProcessing.SetActive(useSniper);
    }
}
