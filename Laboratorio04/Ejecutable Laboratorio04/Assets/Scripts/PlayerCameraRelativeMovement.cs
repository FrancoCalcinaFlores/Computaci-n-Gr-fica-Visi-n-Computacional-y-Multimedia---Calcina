using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerCameraRelativeMovement : MonoBehaviour
{
    public float speed = 5f;

    private CharacterController controller;
    private Vector2 moveInput;

    void Awake()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        // Movimiento con teclado (WASD / Flechas)
        moveInput = Keyboard.current != null
            ? new Vector2(
                (Keyboard.current.aKey.isPressed ? -1 : 0) + (Keyboard.current.dKey.isPressed ? 1 : 0),
                (Keyboard.current.sKey.isPressed ? -1 : 0) + (Keyboard.current.wKey.isPressed ? 1 : 0)
              )
            : Vector2.zero;

        Vector3 move = new Vector3(moveInput.x, 0, moveInput.y);
        controller.Move(move * speed * Time.deltaTime);
    }
}