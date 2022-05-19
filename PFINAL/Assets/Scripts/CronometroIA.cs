using System;
using UnityEngine;
using UnityEngine.UI;

// Clase para medir el tiempo de actuación de la IA
public class CronometroIA : MonoBehaviour
{
    public CarController car;
    public Text textoCrono;

    private double t = 0;
    private double t2 = 0;
   
    private void Update() {
        if (car.getIA()) {          
            t = Time.time - car.getTimeActivated(); // Calcular el tiempo que lleva la IA activa           
            t2 = Math.Truncate(t * 100) / 100;      // Obtener el tiempo con 2 decimales
            t = Math.Truncate(t * 1) / 1;    
            textoCrono.text = t.ToString() + "s";   // Mostrar el tiempo sin decimales
        }       
        else
            textoCrono.text = t2.ToString() + "s";  // Al desactivarse la IA mostrar el tiempo con 2 decimales
    }
}
