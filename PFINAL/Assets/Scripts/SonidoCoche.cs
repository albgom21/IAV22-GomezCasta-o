using UnityEngine;

// Clase que simula el sonido del coche según su velocidad mediante el pitch
public class SonidoCoche : MonoBehaviour
{
    private float pitchCar;
    private float min = 0.85f;
    private float max = 2f;
    
    void Update() {
        pitchCar = Mathf.Lerp(min, max, (GetComponent<CarController>().getVelocidad() / 100));
        GetComponent<AudioSource>().pitch = pitchCar;
    }
}
