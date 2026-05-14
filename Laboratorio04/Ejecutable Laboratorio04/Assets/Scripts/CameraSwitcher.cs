
using Unity.Cinemachine;
using UnityEngine;

public class CameraSwitcher : MonoBehaviour
{
    private static CinemachineCamera activeCamera;

    public static void SwitchCamera(CinemachineCamera newCamera)
    {
        if (newCamera == null) return;

        if (activeCamera != null)
            activeCamera.Priority = 0;

        activeCamera = newCamera;
        activeCamera.Priority = 10;
    }
}