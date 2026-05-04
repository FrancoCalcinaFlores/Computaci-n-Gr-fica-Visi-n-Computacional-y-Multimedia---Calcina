using UnityEngine;
using UnityEngine.InputSystem;

public class MovimientoJugador : MonoBehaviour
{
    public float velocidad = 5f;

    void Update()
    {
        Vector3 movimiento = Vector3.zero;

        if (Keyboard.current == null) return;

        if (Keyboard.current.wKey.isPressed) movimiento.y += 1;
        if (Keyboard.current.sKey.isPressed) movimiento.y -= 1;
        if (Keyboard.current.aKey.isPressed) movimiento.x -= 1;
        if (Keyboard.current.dKey.isPressed) movimiento.x += 1;

        transform.position += movimiento * velocidad * Time.deltaTime;
    }
}
