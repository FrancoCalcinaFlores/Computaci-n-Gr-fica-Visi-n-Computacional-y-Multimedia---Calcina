using UnityEngine;

public class RotarPuerta : MonoBehaviour
{
    private bool abrir = false;
    public float velocidad = 90f;

    void Update()
    {
        if (abrir)
        {
            transform.Rotate(0, 0, velocidad * Time.deltaTime);
        }
    }

    public void AbrirPuerta()
    {
        abrir = true;
    }
}