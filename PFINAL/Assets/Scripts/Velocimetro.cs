using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

public class Velocimetro : MonoBehaviour
{
    public CarController coche;
    public Text textoKMH;

    private float speed = 0.0f;

    private void Update()
    {
        speed = coche.getVelocidad();

        if (textoKMH != null)
            textoKMH.text = ((int)speed) + " km/h";      
    }
}