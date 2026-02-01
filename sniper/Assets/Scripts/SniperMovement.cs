using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

public class SniperMovement : MonoBehaviour
{
    public InputManager input;
    public float mouseSensitivity;
    private Rigidbody2D rb;
    private SoundManager sound;
    public GameObject youFailed;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sound = FindFirstObjectByType<SoundManager>();

    }

    private void Start()
    {
        Cursor.visible = false;
        
    }

    void Update()
    {
        Application.targetFrameRate = 60;
        rb.linearVelocity = new Vector2(input.lookInput.x * mouseSensitivity * Time.deltaTime, input.lookInput.y * mouseSensitivity * Time.deltaTime);
    }
}
