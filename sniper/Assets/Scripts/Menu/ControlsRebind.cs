using UnityEngine;
using UnityEngine.InputSystem;

public class ControlsRebind : MonoBehaviour
{
    [SerializeField] InputActionAsset controls;
    InputManager man;

    void Start()
    {
        man = InputManager.Instance;
        man.moveLeftAction.ChangeBinding(1).WithName("a");
        //controls.FindBinding(InputAction);
    }
}
