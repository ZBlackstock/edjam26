using UnityEngine;

public class CameraTransition : MonoBehaviour
{
    InputManager input;
    [SerializeField] private Transform[] TPs;

    private void Awake()
    {
        input = FindFirstObjectByType<InputManager>();
    }

    void Start()
    {
        
    }

    void Update()
    {
        if(input.moveLeftAction.WasPressedThisFrame() || input.moveRightAction.WasCompletedThisFrame())
        {
            print("Room");
        }
        else if (input.moveUpAction.WasPressedThisFrame())
        {
            print("Building");
        }
        else if (input.moveDownAction.WasPressedThisFrame())
        {
            print("Street");
        }
    }
}
