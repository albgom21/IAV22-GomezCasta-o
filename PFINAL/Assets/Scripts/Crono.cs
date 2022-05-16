using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Crono : MonoBehaviour
{
    public CarController car;
    public Text textoCrono;
    private double t = 0;
   
    private void Update()
    {
        if (car.getIA())
        {          
            t = Time.time - car.getTimeActivated();
            t = Math.Truncate(t * 1) / 1;    
            textoCrono.text = t.ToString() + "s";
        }       
    }
}
