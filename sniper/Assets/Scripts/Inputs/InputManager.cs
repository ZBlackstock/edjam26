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

public class InputManager : MonoBehaviour
{
    //Input Actions Assets
    [SerializeField] private InputActionAsset PlayerInputs;

    //Action map names
    [SerializeField] private string playerActionMapName = "Player";

    //Action names
    [SerializeField] private string moveLeft = "Previous";
    [SerializeField] private string moveRight = "Next";
    [SerializeField] private string look = "Look";
    [SerializeField] private string attack = "Attack";

    //Actions
    private InputAction moveLeftAction;
    private InputAction moveRightAction;
    private InputAction lookAction;
    private InputAction attackAction;

    //Inputs
    public bool moveLeftInput { get; private set; }
    public bool moveRightInput { get; private set; }
    public Vector2 lookInput { get; private set; }
    public bool attackInput { get; private set; }

    public static InputManager Instance { get; private set; }

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
        moveLeftAction = PlayerInputs.FindActionMap(playerActionMapName).FindAction(moveLeft);
        moveRightAction = PlayerInputs.FindActionMap(playerActionMapName).FindAction(moveRight);
        lookAction = PlayerInputs.FindActionMap(playerActionMapName).FindAction(look);
        attackAction = PlayerInputs.FindActionMap(playerActionMapName).FindAction(attack);

        ReadPerformedActionsValue();
    }

    void ReadPerformedActionsValue()
    {
        //get the vector2 for the look actions

        lookAction.performed += context => lookInput = context.ReadValue<Vector2>();
        lookAction.canceled += context => lookInput = Vector2.zero;
    }

    private void OnEnable()
    {
        moveLeftAction.Enable();
        moveRightAction.Enable();
        lookAction.Enable();
        attackAction.Enable();
    }

    private void OnDisable()
    {
        moveLeftAction.Disable();
        moveRightAction.Disable();
        lookAction.Disable();
        attackAction.Disable();
    }
}
