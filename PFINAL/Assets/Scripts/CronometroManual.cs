using System;
using UnityEngine;
using UnityEngine.UI;

// Clase para medir el tiempo de aparcamiento de forma manual
public class CronometroManual : MonoBehaviour
{
    public Text textoCronoM;

    private double timeActivated = 0;
    private double t = 0;
    private double t2 = 0;
    private bool activo = false;
     
    private void Update() {
        // Tecla C -> activar/desactivar el cronometro
        if (Input.GetKeyDown(KeyCode.C)) { 
            activo = !activo;
            if (activo) timeActivated = Time.time;
            GetComponent<AudioSource>().Play();
        }        

        if (activo) {                                    
            t = Time.time - timeActivated;                // Calcular el tiempo que lleva el crono activado         
            t2 = Math.Truncate(t * 100) / 100;            // Obtener el tiempo con 2 decimales
            t = Math.Truncate(t * 1) / 1;               
            textoCronoM.text = t.ToString() + "s";        // Mostrar el tiempo sin decimales   
        }
        else                                            
            textoCronoM.text = t2.ToString() + "s";       // Al desactivarse el crono mostrar el tiempo con 2 decimales   
    }
}
