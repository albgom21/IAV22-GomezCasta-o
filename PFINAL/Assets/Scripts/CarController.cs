using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using BehaviorDesigner.Runtime.Tasks;
using BehaviorDesigner.Runtime;
using Bolt;
using UnityEngine.SceneManagement;

public class CarController : MonoBehaviour
{
    private float horizontalInput;
    private float verticalInput;
    private float steerAngle;
    private bool isBreaking;

    public GameObject textoIA;
    public Transform posBateria;

    public PosicionRayCast delantera;
    public PosicionRayCast trasera;

    public Retrovisor izq;
    public Retrovisor der;

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

    private Rigidbody rb;
    //EN PUBLIC PARA DEPURAR CAMBIAR A PRIVATE
    public bool ia = false;
    public bool stop = false;
    public bool direccion = false;
    public bool avanza = false;
    public bool aparcado = false;
    private float vel = 0.0f;
    private float dir = 0.0f;
    private float danio = 0.0f;
    private float timeActivated = 0.0f;
    private float mult = 2.0f;

    //METODOS PARA BOLT
    public void setAvanza(bool b, float v) { avanza = b; vel = v; }
    public void setDir(bool b, float d) { direccion = b; dir = d; }
    public void setStop(bool b) { stop = b; }
    public bool getIA() { return ia; }
    public bool getAparcado() { return aparcado; }
    public float getDanio() { return danio; }
    public float getTimeActivated() { return timeActivated; }
    public float getVelocidad() { return rb.velocity.magnitude * 3.6f; } // 3.6f para convertir en kilometros

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            ia = !ia;
            if (!ia)
            {
                stop = false;
                direccion = false;
                avanza = false;
            }
            textoIA.SetActive(ia);
            timeActivated = Time.time;
        }
        if (Input.GetKeyDown(KeyCode.R)) SceneManager.LoadScene(0);
        if (Input.GetKeyDown(KeyCode.T)) transform.position = posBateria.position;

        aparcado = delantera.getDist() > 0.4f && trasera.getDist() > 0.4f &&
                !izq.getReferencia() && !der.getReferencia();
    }

    public void reinicio()
    {
        ia = false;
        stop = false;
        direccion = false;
        avanza = false;
        textoIA.SetActive(ia);
        timeActivated = Time.time;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("APARCADO"))
        {
            //Sonido de golpe
            danio += rb.velocity.magnitude * 3.6f;
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
            verticalInput = vel * mult;
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