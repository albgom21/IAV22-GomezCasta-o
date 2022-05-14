using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Crono : MonoBehaviour
{
    public CarController car;
    public Text textoCrono;
    private int cont = 0;
    private double t = 0;
   
    private void Update()
    {
        if (car.getIA())
        {          
            t = Time.time - car.getTimeActivated();
            t = Math.Truncate(t * 100) / 100;    
            textoCrono.text = t.ToString() + "s";
        }         
    }
}
