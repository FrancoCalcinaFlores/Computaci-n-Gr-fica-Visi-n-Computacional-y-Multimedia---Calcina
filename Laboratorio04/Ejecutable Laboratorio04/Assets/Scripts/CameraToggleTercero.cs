using UnityEngine;
using UnityEngine.InputSystem;
using Unity.Cinemachine;

public class CameraToggleTercero : MonoBehaviour
{
    public CinemachineCamera camNormal;
    public CinemachineCamera camTercero;

    private bool usandoTercero = false;

    void Start()
    {
        CameraSwitcher.SwitchCamera(camNormal);
    }

    void Update()
    {
        if (Keyboard.current == null) return;

        if (Keyboard.current.cKey.wasPressedThisFrame)
        {
            usandoTercero = !usandoTercero;

            if (usandoTercero)
                CameraSwitcher.SwitchCamera(camTercero);
            else
                CameraSwitcher.SwitchCamera(camNormal);
        }
    }
}