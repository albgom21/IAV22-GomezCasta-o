using UnityEngine;

// Clase para seguir al coche, la incorpora la c�mara principal
public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Vector3 offset;       // Vector3 que sirve de margen entre el objeto a seguir y la c�mara 
    [SerializeField] private Transform target;     // Objetivo a seguir 
    [SerializeField] private float translateSpeed; // Velocidad para seguir la posici�n del objetivo
    [SerializeField] private float rotationSpeed;  // Velocidad para seguir la rotaci�n del objetivo

    private void FixedUpdate()
    {
        // Mover y rotar la c�mara siguiendo al objetivo
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
