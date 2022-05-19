using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuestraUI : MonoBehaviour
{
    public GameObject instrucciones;
    bool activo = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            activo = !activo;
            instrucciones.SetActive(activo);
        }
    }
}
