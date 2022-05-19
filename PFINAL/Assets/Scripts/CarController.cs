using UnityEngine;
using UnityEngine.UI;
using Bolt;
using UnityEngine.SceneManagement;


// Clase que controla el coche, tanto la lógica de la IA como en manual
public class CarController : MonoBehaviour
{

    // Variables de la UI que usan datos del coche
    public GameObject textoIA; // Saber si está la IA activada
    public Text textoMult;     // Texto multiplicador velocidad de la IA

    // Posición para moverse al Parking y probar el est. en batería
    public Transform posBateria;

    // Referencias a los Raycast para comprobar si está aparcado el coche
    public PosicionRayCast delantera;
    public PosicionRayCast trasera;
    public Retrovisor izq;
    public Retrovisor der;

    // Máquinas de estados con la IA que aparca el coche
    public StateMachine paralelo;
    public StateMachine bateria;

    // Posición y collider de las 4 ruedas
    public WheelCollider frontLeftWheelCollider;
    public WheelCollider frontRightWheelCollider;
    public WheelCollider rearLeftWheelCollider;
    public WheelCollider rearRightWheelCollider;

    public Transform frontLeftWheelTransform;
    public Transform frontRightWheelTransform;
    public Transform rearLeftWheelTransform;
    public Transform rearRightWheelTransform;

    // Sonidos al activar la IA y al colisionar con otro coche 
    public AudioSource sonidoIA;
    public AudioSource sonidoGolpe;

    // Limite de velocidad para la IA
    public int limiteVelocidadIA;
    
    // Variables para el movimiento del coche
    private float horizontalInput;
    private float verticalInput;
    private float steerAngle;
    private bool isBreaking;
    private float maxSteeringAngle = 30f;
    private float motorForce = 500f;
    private float brakeForce = 6000f;

    // Referencia al propio Rigidbody del coche
    private Rigidbody rb;

    // Variables utilizadas en BOLT 
    private bool ia = false;            // Indica si la IA está activa
    private bool stop = false;          // Indica si tiene que pararse el coche
    private bool direccion = false;     // Indica si tiene que usarse la dirección dada
    private bool avanza = false;        // Indica si tiene que moverse el coche    
    private bool aparcado = false;      // Indica si está aparcado el coche
    private float vel = 0.0f;           // Input administrado por la IA
    private float dir = 0.0f;           // Dirección de las ruedas ···  -1 = izq   ···   0 = recta   ···  1 = derecha ···
    
    private float danio = 0.0f;         // Daño ocasionado a otros coches
    private float timeActivated = 0.0f; // Tiempo desde que se activó la IA
    private float mult = 2.0f;          // Multiplicador de la velocidad de la IA

    //GETTERS y SETTERS para BOLT
    public void setAvanza(bool b, float v) { avanza = b; vel = v; }
    public void setDir(bool b, float d) { direccion = b; dir = d; }
    public void setStop(bool b) { stop = b; }
    public bool getIA() { return ia; }
    public bool getAparcado() { return aparcado; }
    public float getDanio() { return danio; }
    public float getTimeActivated() { return timeActivated; }
    public float getVelocidad() { return rb.velocity.magnitude * 3.6f; } // 3.6f para convertir en kilometros

    private void Awake() {
        rb = GetComponent<Rigidbody>();
        paralelo.enabled = true;
        bateria.enabled = false;
    }

    private void Update() { 
        // Tecla E -> activar/desactivar la IA
        if (Input.GetKeyDown(KeyCode.E)) {
            ia = !ia;
            if (!ia) {
                stop = false;
                direccion = false;
                avanza = false;
            }
            textoIA.SetActive(ia);
            timeActivated = Time.time;
            sonidoIA.Play(); ;
        }
        
        // Tecla R -> reiniciar la escena
        if (Input.GetKeyDown(KeyCode.R)) SceneManager.LoadScene(0);
        
        // Tecla T -> mover el coche a la zona de Parking (est. bateria)
        if (Input.GetKeyDown(KeyCode.T)) {
            ia = false;
            stop = false;
            direccion = false;
            avanza = false;
            textoIA.SetActive(ia);
            transform.position = posBateria.position;
            transform.rotation = posBateria.rotation;
            rb.velocity = Vector3.zero;
        }
        
        // Tecla Flecha Arriba -> Aumentar el multiplicador de vel de la IA
        if (Input.GetKeyDown(KeyCode.UpArrow)) {
            if (ia && mult < 3) {
                mult += 0.25f;
                textoMult.text = "x" + mult.ToString();
            }
        };

        // Tecla Flecha Abajo -> Disminuir el multiplicador de vel de la IA
        if (Input.GetKeyDown(KeyCode.DownArrow)) {
            if (ia && mult > 1) {
                mult -= 0.25f;
                textoMult.text = "x" + mult.ToString();
            }
        };        

        // Comprobar si ya se considera aparcado el coche en paralelo según la pos del coche
        aparcado = delantera.getDist() > 0.4f && trasera.getDist() > 0.4f &&
                   !izq.getReferencia() && !der.getReferencia();
    }
        
    // Si el coche colisiona con otro se suma daño
    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.CompareTag("APARCADO")) {
            sonidoGolpe.Play();
            danio += 1 + rb.velocity.magnitude * 3.6f;
        }
    }

    // Si el coche entra en la zona del Parking se activa la IA para el est. en batería
    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("PARKING")) {
            paralelo.enabled = false;
            bateria.enabled = true;
        }
    }

    // Si el coche sale de la zona del Parking se desactiva la IA para el est. en batería
    private void OnTriggerExit(Collider other) {
        if (other.gameObject.CompareTag("PARKING")) {
            paralelo.enabled = true;
            bateria.enabled = false;
        }
    }

    private void FixedUpdate() {
        GetInput();             // Obtener input, del usuario o de la IA
        HandleMotor();          // Transmitir el input al movimiento
        HandleSteering();       // Calcular el ángulo de las ruedas según el input
        UpdateWheels();         // Posicionar las ruedas según el ángulo resultante del input
    }

    private void GetInput() {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
        isBreaking = Input.GetKey(KeyCode.Space);

        if(verticalInput < 0) verticalInput /= 2.5f; // Reducir la velocidad del coche al ir marcha atrás

        if (avanza)
            verticalInput = vel * mult;

        if (direccion)
            horizontalInput = dir;

        if (stop)
            isBreaking = true;
    }

    private void HandleSteering() {
        steerAngle = maxSteeringAngle * horizontalInput;
        frontLeftWheelCollider.steerAngle = steerAngle;
        frontRightWheelCollider.steerAngle = steerAngle;
    }

    private void HandleMotor() {
        frontLeftWheelCollider.motorTorque = verticalInput * motorForce;
        frontRightWheelCollider.motorTorque = verticalInput * motorForce;

        //Si está avanzando con la IA capar la velocidad máxima a la que puede circular
        if (avanza && vel > 0 && getVelocidad() > limiteVelocidadIA)
            rb.velocity = new Vector3(0, 0, limiteVelocidadIA / 3.6f);

        brakeForce = isBreaking ? 6000f : 0f;
        frontLeftWheelCollider.brakeTorque = brakeForce;
        frontRightWheelCollider.brakeTorque = brakeForce;
        rearLeftWheelCollider.brakeTorque = brakeForce;
        rearRightWheelCollider.brakeTorque = brakeForce;
    }

    private void UpdateWheels() {
        UpdateWheelPos(frontLeftWheelCollider, frontLeftWheelTransform);
        UpdateWheelPos(frontRightWheelCollider, frontRightWheelTransform);
        UpdateWheelPos(rearLeftWheelCollider, rearLeftWheelTransform);
        UpdateWheelPos(rearRightWheelCollider, rearRightWheelTransform);
    }

    private void UpdateWheelPos(WheelCollider wheelCollider, Transform trans) {
        Vector3 pos;
        Quaternion rot;
        wheelCollider.GetWorldPose(out pos, out rot);
        trans.rotation = rot;
        trans.position = pos;
    }

    // Reiniciar las variables de la IA
    public void reinicio() {
        ia = false;
        stop = false;
        direccion = false;
        avanza = false;
        textoIA.SetActive(ia);
        timeActivated = Time.time;
    }
}
