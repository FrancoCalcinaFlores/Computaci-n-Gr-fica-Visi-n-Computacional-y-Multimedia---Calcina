using UnityEngine;
using UnityEngine.UI;

public class SumarPuntos : MonoBehaviour
{
    public int puntos = 0;
    public Slider barraPuntos;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "balon(Clone)")
        {
            puntos += 2;
            barraPuntos.value = puntos;
            Debug.Log("PUNTO");

            // 🔴 ESTA ES LA ÚNICA LÍNEA NUEVA
            Destroy(other.gameObject);
        }
    }
}