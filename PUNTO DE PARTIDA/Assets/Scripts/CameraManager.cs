using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public Camera main, topCar, leftCar, rightCar;

    private void Start()
    {
        main.enabled = true;
        topCar.enabled = false;
        leftCar.enabled = false;
        rightCar.enabled = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            main.enabled = true;
            topCar.enabled = false;
            leftCar.enabled = false;
            rightCar.enabled = false;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            main.enabled = false;
            topCar.enabled = true;
            leftCar.enabled = false;
            rightCar.enabled = false;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            main.enabled = false;
            topCar.enabled = false;
            leftCar.enabled = true;
            rightCar.enabled = false;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            main.enabled = false;
            topCar.enabled = false;
            leftCar.enabled = false;
            rightCar.enabled = true;
        }
    }
}