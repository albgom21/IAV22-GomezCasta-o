using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Activa o desactiva los distintos puntos de vista según la entrada
 */

public class CameraManager : MonoBehaviour
{
    public Camera main, ghost, singer, general;

    private void Start()
    {
        main.enabled = true;
        ghost.enabled = false;
        singer.enabled = false;
        general.enabled = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            main.enabled = true;
            ghost.enabled = false;
            singer.enabled = false;
            general.enabled = false;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            main.enabled = false;
            ghost.enabled = true;
            singer.enabled = false;
            general.enabled = false;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            main.enabled = false;
            ghost.enabled = false;
            singer.enabled = true;
            general.enabled = false;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            main.enabled = false;
            ghost.enabled = false;
            singer.enabled = false;
            general.enabled = true;
        }
    }
}
