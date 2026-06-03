using UnityEngine;
using UnityEngine.InputSystem;

public class SimpleFPSController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float rotationSpeed = 80f;

    private CharacterController controller;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        if (Keyboard.current == null) return;

        // Movimiento
        Vector3 move = Vector3.zero;

        if (Keyboard.current.wKey.isPressed)
        {
            move += transform.forward;
        }

        if (Keyboard.current.sKey.isPressed)
        {
            move -= transform.forward;
        }

        if (Keyboard.current.aKey.isPressed)
        {
            transform.Rotate(
                Vector3.up * -rotationSpeed * Time.deltaTime
            );
        }

        if (Keyboard.current.dKey.isPressed)
        {
            transform.Rotate(
                Vector3.up * rotationSpeed * Time.deltaTime
            );
        }

        controller.Move(
            move.normalized *
            moveSpeed *
            Time.deltaTime
        );
    }
}