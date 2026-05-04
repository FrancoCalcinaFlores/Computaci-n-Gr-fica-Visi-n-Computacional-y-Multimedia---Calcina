using UnityEngine;

public class InteraccionJugador : MonoBehaviour
{
    public RotarPuerta puerta;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "Llave")
        {
            puerta.AbrirPuerta();
            Destroy(other.gameObject);
        }
    }
}