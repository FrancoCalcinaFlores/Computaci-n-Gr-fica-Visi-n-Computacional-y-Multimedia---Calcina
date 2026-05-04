using UnityEngine;

public class EscalarLlave : MonoBehaviour
{
    public float escalaMin = 0.5f;
    public float escalaMax = 1.5f;
    public float velocidad = 2f;

    void Update()
    {
        float escala = Mathf.Lerp(escalaMin, escalaMax, (Mathf.Sin(Time.time * velocidad) + 1) / 2);
        transform.localScale = new Vector3(escala, escala, 1);
    }
}