using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

public class SniperMovement : MonoBehaviour
{
    public InputManager input;
    public float mouseSensitivity;
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(input.lookInput.x * mouseSensitivity, input.lookInput.y * mouseSensitivity);
    }
}
