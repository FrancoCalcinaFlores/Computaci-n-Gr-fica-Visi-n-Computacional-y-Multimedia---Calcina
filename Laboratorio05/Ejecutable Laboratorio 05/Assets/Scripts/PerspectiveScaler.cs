using UnityEngine;
using UnityEngine.InputSystem;

public class PerspectiveScaler : MonoBehaviour
{
    public Transform cam;
    public float scaleSpeed = 0.5f;
    public float moveSpeed = 2f;

    private Vector3 originalScale;

    void Start()
    {
        originalScale = transform.localScale;
    }

    void Update()
    {
        if (Keyboard.current != null && Keyboard.current.eKey.isPressed)
        {
            transform.position += cam.forward * moveSpeed * Time.deltaTime;
            transform.localScale += Vector3.one * scaleSpeed * Time.deltaTime;
        }
    }
}