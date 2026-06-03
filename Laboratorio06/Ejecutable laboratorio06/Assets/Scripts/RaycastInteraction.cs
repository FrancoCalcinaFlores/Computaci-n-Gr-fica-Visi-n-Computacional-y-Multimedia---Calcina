using UnityEngine;
using UnityEngine.InputSystem;

public class RaycastInteraction : MonoBehaviour
{
    public Light comfortLight;
    public Light fearLight;
    public Light sadLight;

    public float baseIntensity = 5f;
    public float intensityIncrease = 20f;
    public float maxIntensity = 100f;

    private Light currentLight;

    void Start()
    {
        ResetAllLights();
    }

    void Update()
    {
        if (Camera.main == null) return;

        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            Ray ray = Camera.main.ScreenPointToRay(
                Mouse.current.position.ReadValue()
            );

            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.CompareTag("Comfort"))
                {
                    HandleLight(comfortLight);
                }

                if (hit.collider.CompareTag("Fear"))
                {
                    HandleLight(fearLight);
                }

                if (hit.collider.CompareTag("Sadness"))
                {
                    HandleLight(sadLight);
                }
            }
        }
    }

    void HandleLight(Light selectedLight)
    {
        // Si cambió de botón
        if (currentLight != selectedLight)
        {
            ResetAllLights();

            selectedLight.enabled = true;
            selectedLight.intensity = baseIntensity;

            currentLight = selectedLight;
        }
        else
        {
            // Si clickea el mismo botón
            selectedLight.intensity =
                Mathf.Min(
                    selectedLight.intensity + intensityIncrease,
                    maxIntensity
                );
        }
    }

    void ResetAllLights()
    {
        comfortLight.enabled = false;
        fearLight.enabled = false;
        sadLight.enabled = false;

        comfortLight.intensity = baseIntensity;
        fearLight.intensity = baseIntensity;
        sadLight.intensity = baseIntensity;
    }
}