using UnityEngine;

// Clase utilizada en las cámaras auxiliares para seguir al coche
public class Follow : MonoBehaviour
{
    public Transform objetivo;
    public Vector3 offset;
    
    void Update() {
        transform.position = objetivo.position + offset;
    }
}
