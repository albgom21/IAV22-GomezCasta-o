using UnityEngine;

// Clase para seguir al coche, la incorpora la cámara principal
public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Vector3 offset;       // Vector3 que sirve de margen entre el objeto a seguir y la cámara 
    [SerializeField] private Transform target;     // Objetivo a seguir 
    [SerializeField] private float translateSpeed; // Velocidad para seguir la posición del objetivo
    [SerializeField] private float rotationSpeed;  // Velocidad para seguir la rotación del objetivo

    private void FixedUpdate()
    {
        // Mover y rotar la cámara siguiendo al objetivo
        HandleTranslation(); 
        HandleRotation();
    }

    private void HandleTranslation()
    {
        var targetPosition = target.TransformPoint(offset);
        transform.position = Vector3.Lerp(transform.position, targetPosition, translateSpeed * Time.deltaTime);
    }

    private void HandleRotation()
    {
        var direction = target.position - transform.position;
        var rotation = Quaternion.LookRotation(direction, Vector3.up);
        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);
    }
}
