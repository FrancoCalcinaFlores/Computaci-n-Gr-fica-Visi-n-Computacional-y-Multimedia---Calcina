using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BalonLogica : MonoBehaviour
{
    public Slider barraFallos;
    public int maxFallos = 10;
    public float tiempoParaFallo = 2f;

    private bool marco = false;
    private static int fallos = 0;

    void Start()
    {
        // Inicia el temporizador del intento
        StartCoroutine(EsperarResultado());
    }

    private void OnTriggerEnter(Collider other)
    {
        // Si encesta
        if (other.CompareTag("Points"))
        {
            marco = true;
            Destroy(gameObject);
        }
    }

    IEnumerator EsperarResultado()
    {
        yield return new WaitForSeconds(tiempoParaFallo);

        // Si después del tiempo NO marcó → fallo
        if (!marco)
        {
            fallos++;
            barraFallos.value = fallos;

            Debug.Log("Fallo por tiempo: " + fallos);

            if (fallos >= maxFallos)
            {
                Debug.Log("GAME OVER");
                Time.timeScale = 0f;
            }

            Destroy(gameObject);
        }
    }
}