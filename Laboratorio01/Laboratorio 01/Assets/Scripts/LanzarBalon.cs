using UnityEngine;
using UnityEngine.InputSystem;

public class LanzarBalon : MonoBehaviour
{
    [SerializeField] private Rigidbody balonPrefab;
    [SerializeField] private Transform puntoLanzamiento;
    [SerializeField] private Camera camara;

    public InputAction disparar;
    public float fuerzaLanzamiento = 18f;

    void OnEnable()
    {
        disparar.Enable();
        disparar.performed += Lanzar;
    }

    void OnDisable()
    {
        disparar.performed -= Lanzar;
        disparar.Disable();
    }

    void Lanzar(InputAction.CallbackContext ctx)
    {
        Ray rayo = camara.ScreenPointToRay(Mouse.current.position.ReadValue());

        if (Physics.Raycast(rayo, out RaycastHit impacto))
        {
            Vector3 direccion = (impacto.point - puntoLanzamiento.position).normalized;

            Rigidbody nuevoBalon = Instantiate(
                balonPrefab,
                puntoLanzamiento.position,
                Quaternion.identity
            );

            nuevoBalon.AddForce(direccion * fuerzaLanzamiento, ForceMode.Impulse);
        }
    }
}