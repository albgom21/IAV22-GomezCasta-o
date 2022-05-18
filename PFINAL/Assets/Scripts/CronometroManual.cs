using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CronometroManual : MonoBehaviour
{
    public Text textoCronoM;
    private double timeActivated = 0;
    private double t = 0;
    private double t2 = 0;
    private bool activo = false;
     

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C)) { 
            activo = !activo;
            if (activo) timeActivated = Time.time;
            GetComponent<AudioSource>().Play(); ;
        }
        

        if (activo) {
            t = Time.time - timeActivated;
            t2 = Math.Truncate(t * 100) / 100;
            t = Math.Truncate(t * 1) / 1;
            textoCronoM.text = t.ToString() + "s";
        }
        else
            textoCronoM.text = t2.ToString() + "s";
    }
}