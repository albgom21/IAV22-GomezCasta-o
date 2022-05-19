using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DesapareceUI : MonoBehaviour
{
    public float tiempoEnPantalla = 20f;
    bool once = false;
    private void Update()
    {    
        if (!once && gameObject.activeSelf && (Time.time >= tiempoEnPantalla))
        {
            gameObject.SetActive(false);
            once = true;
        }
    }
}