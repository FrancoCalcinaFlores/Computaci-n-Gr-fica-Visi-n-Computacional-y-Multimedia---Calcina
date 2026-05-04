using UnityEngine;
using UnityEngine.UI;

public class SumarFallos : MonoBehaviour
{
    public int fallos = 0;
    public int maxFallos = 10;
    public Slider barraFallos;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "balon(Clone)")
        {
            fallos++;
            barraFallos.value = fallos;

            Debug.Log("FALLO");

            if (fallos >= maxFallos)
            {
                Time.timeScale = 0f;
            }
        }
    }
}