using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

public class SniperMovement : MonoBehaviour
{
    public InputManager input;
    public float mouseSensitivity;

    private void Awake()
    {
        // input = InputManager.Instance;
    }

    void Start()
    {

    }

    void Update()
    {
        transform.position += new Vector3(input.lookInput.x * Time.deltaTime * mouseSensitivity, 
            input.lookInput.y * Time.deltaTime * mouseSensitivity, 0);
    }
}
