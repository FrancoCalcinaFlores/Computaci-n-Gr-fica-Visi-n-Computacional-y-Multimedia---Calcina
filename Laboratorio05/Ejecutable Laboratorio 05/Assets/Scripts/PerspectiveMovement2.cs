using UnityEngine;
using UnityEngine.InputSystem;

public class PerspectiveMovement : MonoBehaviour
{
    public Transform cam;
    public float moveSpeed = 3f;

    void Update()
    {
        if (Keyboard.current.eKey.isPressed)
        {
            transform.position += cam.forward * moveSpeed * Time.deltaTime;
        }

        if (Keyboard.current.qKey.isPressed)
        {
            transform.position -= cam.forward * moveSpeed * Time.deltaTime;
        }
    }
}