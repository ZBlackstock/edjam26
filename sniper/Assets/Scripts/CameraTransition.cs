using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class CameraTransition : MonoBehaviour
{
    InputManager input;
    private Transform currentTP;
    [SerializeField] private Transform[] TPs; // 0 - building, 1 - street, 2 - room
    [SerializeField] private SpriteRenderer sniper;
    [SerializeField] private SpriteRenderer handgun;
    [SerializeField] private GameObject postProcessing;

    private void Awake()
    {
        input = FindFirstObjectByType<InputManager>();
    }

    void Start()
    {
        currentTP = TPs[0]; // Set to aim sniper at building on start
    }

    void Update()
    {
        if (input.moveUpAction.WasPressedThisFrame())
        {
            print("Building");
            if (currentTP != TPs[0])
            {
                SetAim(TPs[0], true);
            }
        }
        else if (input.moveDownAction.WasPressedThisFrame())
        {
            print("Street");
            if (currentTP != TPs[1])
            {
                SetAim(TPs[1], true);
            }
        }
        else if (input.moveLeftAction.WasPressedThisFrame() || input.moveRightAction.WasCompletedThisFrame())
        {
            print("Room");
            if (currentTP != TPs[2])
            {
                SetAim(TPs[2], false);
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
