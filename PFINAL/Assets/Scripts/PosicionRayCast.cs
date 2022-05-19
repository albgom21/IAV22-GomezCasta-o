using UnityEngine;

// Clase para controlar la distancia del coche entre los otros dos
public class PosicionRayCast : MonoBehaviour
{
    public bool delante; // True: raycast hacia delante    False: raycast hacia atras

    private float dist = 0.0f;
    private RaycastHit hit;
    private Vector3 v;

    public float getDist() { return dist; } // Devolver la distancia con el coche más cercano

    private void Awake()
    {
        if (delante) v = Vector3.forward;
        else v = Vector3.back;
    }

    void Update()
    {
        if (Physics.Raycast(transform.position, v, out hit, 5f)) {
            if (hit.transform.CompareTag("APARCADO")) {
                Debug.DrawRay(transform.position, v * hit.distance, Color.red);
                dist = hit.distance;
            }
        }
        else {
            Debug.DrawRay(transform.position, v * 5f, Color.green);
            dist = 0.0f;
        }
    }
}
