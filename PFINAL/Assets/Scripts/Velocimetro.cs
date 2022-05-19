using UnityEngine;
using UnityEngine.UI;

// Clase que muestra por pantalla la velodidad del coche
public class Velocimetro : MonoBehaviour
{
    public CarController coche;
    public Text textoKMH;

    private float speed = 0.0f;

    private void Update(){
        speed = coche.getVelocidad();

        if (textoKMH != null)
            textoKMH.text = ((int)speed) + " km/h";      
    }
}
