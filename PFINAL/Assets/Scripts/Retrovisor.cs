using UnityEngine;

// Clase para comprobar referencias al aparcar en paralelo
public class Retrovisor : MonoBehaviour
{
    public bool front;  //  True: raycast hacia delante    False: raycast hacia atras en función del padre

    [SerializeField] LayerMask layerMask;
    private bool referencia = false;
    private RaycastHit hit;
    private Vector3 v;

    void Update() {
        if (front)
            v = Vector3.forward;
        else
            v = transform.parent.TransformDirection(Vector3.back);        

        if (Physics.Raycast(transform.position, v, out hit, 10f, layerMask))
        {
            Debug.DrawRay(transform.position, v * hit.distance, Color.red);
            referencia = true;
        }
        else
        {
            Debug.DrawRay(transform.position, v * 10f, Color.green);
            referencia = false;
        }
    }
    public bool getReferencia() { return referencia; } // Método para BOLT
}
