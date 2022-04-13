using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

public class Velocimetro : MonoBehaviour
{
    public Rigidbody coche;
    public Text textoKMH;

    private float speed = 0.0f;

    private void Update()
    {
        // 3.6f para convertir en kilometros
        speed = coche.velocity.magnitude * 3.6f;

        if (textoKMH != null)
            textoKMH.text = ((int)speed) + " km/h";      
    }
}