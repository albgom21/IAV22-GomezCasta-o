using UnityEngine;

// Clase para obtener la referencia necesaria para el estacionamiento en batería
public class BateriaRayCast : MonoBehaviour
{
    [SerializeField] LayerMask layerMask;
    private RaycastHit hit;

    private bool referencia = false;

    void Update()
    {
        //Raycast para obtener la referencia al faro más lejano en el estacionamiento en batería
        if (Physics.Raycast(transform.position, Vector3.left, out hit, 5f, layerMask))
        {
            Debug.DrawRay(transform.position, Vector3.left * hit.distance, Color.red);
            referencia = true;
        }
        else
        {
            Debug.DrawRay(transform.position, Vector3.left * 5f, Color.green);
            referencia = false;
        }
    }

    public bool getReferencia() { return referencia; } //Usado en Bolt para comprobar la posición del coche
}
