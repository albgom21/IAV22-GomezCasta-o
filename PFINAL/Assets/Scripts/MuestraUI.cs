using UnityEngine;

// Clase para mostrar las instrucciones
public class MuestraUI : MonoBehaviour
{
    public GameObject instrucciones;

    private bool activo = false;

    void Update() {
        // Tecla H -> activar/desactivar las instrucciones 
        if (Input.GetKeyDown(KeyCode.H)) {
            activo = !activo;
            instrucciones.SetActive(activo);
        }
    }
}
