using UnityEngine;

public class PruebaColision : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("COLISION DETECTADA CON: " + collision.gameObject.name);
    }
}