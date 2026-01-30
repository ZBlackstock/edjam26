using System;
using Unity.Mathematics;
using UnityEngine.Animations;
//using System.Numerics;
//using UnityEditor.Rendering.LookDev;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.TextCore.Text;
using UnityEngine.Rendering.Universal;

public class InputController : MonoBehaviour
{
    //Input Actions Assets
    [SerializeField] private InputActionAsset PlayerInputs;

    //Action map names
    [SerializeField] private string playerActionMapName = "Player";

    //Action names
    [SerializeField] private string move = "Move";
    [SerializeField] private string look = "Look";
    [SerializeField] private string interact = "Interact";
    [SerializeField] private string cancel = "Cancel";
    [SerializeField] private string information = "Information";

    //Inputs
    private InputAction moveAction;
    private InputAction lookAction;
    public InputAction interactAction;
    public InputAction cancelAction;
    public InputAction informationAction;

    //Inputs
    public Vector2 MoveInput { get; private set; }
    public Vector2 CameraInput { get; private set; }
    public bool interactInput { get; private set; }
    public bool cancelInput { get; private set; }
    public bool informationInput { get; private set; }

    public static InputController Instance { get; private set; }

    private void Awake()
    {
        if (Instance == false)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        //set up the action variables
        moveAction = PlayerInputs.FindActionMap(playerActionMapName).FindAction(move);
        lookAction = PlayerInputs.FindActionMap(playerActionMapName).FindAction(look);
        interactAction = PlayerInputs.FindActionMap(playerActionMapName).FindAction(interact);
        cancelAction = PlayerInputs.FindActionMap(playerActionMapName).FindAction(cancel);
        informationAction = PlayerInputs.FindActionMap(playerActionMapName).FindAction(information);

        ReadPerformedActionsValue();
    }

    void ReadPerformedActionsValue()
    {
        //get the vector2 for the move and look actions
        moveAction.performed += context => MoveInput = context.ReadValue<Vector2>();
        moveAction.canceled += context => MoveInput = Vector2.zero;

        lookAction.performed += context => CameraInput = context.ReadValue<Vector2>();
        lookAction.canceled += context => CameraInput = Vector2.zero;
    }

    private void OnEnable()
    {
        moveAction.Enable();
        lookAction.Enable();
        interactAction.Enable();
        cancelAction.Enable();
        informationAction.Enable();
    }

    private void OnDisable()
    {
        moveAction.Disable();
        lookAction.Disable();
        interactAction.Disable();
        cancelAction.Disable();
        informationAction.Disable();
    }
}
