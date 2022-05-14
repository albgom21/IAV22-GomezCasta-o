using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using BehaviorDesigner.Runtime.Tasks;
using BehaviorDesigner.Runtime;
using Bolt;

public class CarController : MonoBehaviour
{
    private float horizontalInput;
    private float verticalInput;
    private float steerAngle;
    private bool isBreaking;

    public GameObject textoIA;

    public WheelCollider frontLeftWheelCollider;
    public WheelCollider frontRightWheelCollider;
    public WheelCollider rearLeftWheelCollider;
    public WheelCollider rearRightWheelCollider;
    public Transform frontLeftWheelTransform;
    public Transform frontRightWheelTransform;
    public Transform rearLeftWheelTransform;
    public Transform rearRightWheelTransform;

    public float maxSteeringAngle = 40f;
    public float motorForce = 50f;
    public float brakeForce = 0f;

    private bool ia = false;
    private bool stop = false;
    private bool direccion = false;
    private bool avanza = false;
    private float vel = 0.0f;
    private float dir = 0.0f;
    private float danio = 0.0f;

    //METODOS PARA BOLT
    public void setAvanza(bool b, float v) { avanza = b; vel = v; } 
    public void setDir(bool b, float d) { direccion = b; dir = d; } 
    public void setStop(bool b) { stop = b; }
    public bool getIA() { return ia; }
    public float getDanio() { return danio; }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            ia = !ia;
            textoIA.SetActive(ia);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "APARCADO")
        {
            //Sonido de golpe
            danio += GetComponent<Rigidbody>().velocity.magnitude * 3.6f;
        }
    }

    private void FixedUpdate()
    {
        GetInput();
        HandleMotor();
        HandleSteering();
        UpdateWheels();
    }

    private void GetInput()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");       
        isBreaking = Input.GetKey(KeyCode.Space);
             
        if (avanza)
            verticalInput = vel;
        if (direccion)
            horizontalInput = dir; // 1 full derecha // -1 full izquierda

        if (stop)
            isBreaking = true;
    }

    private void HandleSteering()
    {
        steerAngle = maxSteeringAngle * horizontalInput;
        frontLeftWheelCollider.steerAngle = steerAngle;
        frontRightWheelCollider.steerAngle = steerAngle;
    }

    private void HandleMotor()
    {        
        frontLeftWheelCollider.motorTorque = verticalInput * motorForce;
        frontRightWheelCollider.motorTorque = verticalInput * motorForce;

        brakeForce = isBreaking ? 6000f : 0f;
        frontLeftWheelCollider.brakeTorque = brakeForce;
        frontRightWheelCollider.brakeTorque = brakeForce;
        rearLeftWheelCollider.brakeTorque = brakeForce;
        rearRightWheelCollider.brakeTorque = brakeForce;
    }

    private void UpdateWheels()
    {
        UpdateWheelPos(frontLeftWheelCollider, frontLeftWheelTransform);
        UpdateWheelPos(frontRightWheelCollider, frontRightWheelTransform);
        UpdateWheelPos(rearLeftWheelCollider, rearLeftWheelTransform);
        UpdateWheelPos(rearRightWheelCollider, rearRightWheelTransform);
    }

    private void UpdateWheelPos(WheelCollider wheelCollider, Transform trans)
    {
        Vector3 pos;
        Quaternion rot;
        wheelCollider.GetWorldPose(out pos, out rot);
        trans.rotation = rot;
        trans.position = pos;
    }       
}