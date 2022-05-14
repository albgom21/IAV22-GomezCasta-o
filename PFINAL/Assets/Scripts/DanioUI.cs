using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DanioUI : MonoBehaviour
{
    public CarController coche;
    public Text textoDanio;

    private float danio = 0.0f;

    private void Update()
    {        
        danio = coche.getDanio();

        if (textoDanio != null)
            textoDanio.text = "Daño: " + (int)danio;
    }
}
