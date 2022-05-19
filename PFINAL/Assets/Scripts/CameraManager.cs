using UnityEngine;

// Clase que maneja el uso de las diferentes cámaras del proyecto
public class CameraManager : MonoBehaviour
{
    public Camera main, topCar, leftCar, rightCar; // Diferentes cámaras

    private void Start() // Empezar con la cámara principal
    {
        main.enabled = true;
        topCar.enabled = false;
        leftCar.enabled = false;
        rightCar.enabled = false;
    }

    void Update()
    {
        // Según que número se pulse (no numpad) se cambia de cámara
        if (Input.GetKeyDown(KeyCode.Alpha1))       // Tecla 1
        {
            main.enabled = true;
            topCar.enabled = false;
            leftCar.enabled = false;
            rightCar.enabled = false;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))  // Tecla 2
        {
            main.enabled = false;
            topCar.enabled = true;
            leftCar.enabled = false;
            rightCar.enabled = false;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))  // Tecla 3
        {
            main.enabled = false;
            topCar.enabled = false;
            leftCar.enabled = true;
            rightCar.enabled = false;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))  // Tecla 4
        {
            main.enabled = false;
            topCar.enabled = false;
            leftCar.enabled = false;
            rightCar.enabled = true;
        }
    }
}
