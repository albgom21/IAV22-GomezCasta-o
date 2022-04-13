using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Cantante : MonoBehaviour
{
    // Segundos que estara cantando
    public double tiempoDeCanto;
    // Segundo en el que comezo a cantar
    private double tiempoComienzoCanto;
    // Segundos que esta descanasando
    public double tiempoDeDescanso;
    // Segundo en el que comezo a descansar
    private double tiempoComienzoDescanso;
    // Si esta capturada
    public bool capturada = false;

    [Range(0, 180)]
    // Angulo de vision en horizontal
    public double anguloVistaHorizontal;
    // Distancia maxima de vision
    public double distanciaVista;
    // Objetivo al que ver"
    public Transform objetivo;

    // Segundos que puede estar merodeando
    public double tiempoDeMerodeo;
    // Segundo en el que comezo a merodear
    public double tiempoComienzoMerodeo = 0;
    // Distancia de merodeo
    public int distanciaDeMerodeo = 16;
    // Si canta o no
    public bool cantando = false;

    // Componente cacheado NavMeshAgent
    private NavMeshAgent agente;

    // Objetivos de su itinerario
    public Transform Escenario;
    public Transform Bambalinas;

    // La blackboard
    public GameBlackboard bb;

    public void Awake()
    {
        agente = GetComponent<NavMeshAgent>();
    }

    public void Start()
    {
        agente.updateRotation = false;
    }

    public void LateUpdate()
    {
        if (agente.velocity.sqrMagnitude > Mathf.Epsilon)
        {
            transform.rotation = Quaternion.LookRotation(agente.velocity.normalized);
        }
    }

    // Comienza a cantar, reseteando el temporizador
    public void Cantar()
    {
        tiempoComienzoCanto = 0;
        cantando = true;
    }

    // Comprueba si tiene que dejar de cantar
    public bool TerminaCantar()
    {
        NavMeshHit navHit;
        NavMesh.SamplePosition(transform.position, out navHit, 2f, NavMesh.AllAreas);
        if ((1 << NavMesh.GetAreaFromName("Escenario") & navHit.mask) != 0)
        {
            tiempoComienzoCanto += Time.deltaTime;  // Solo disminuye si se encuentra en el escenario como tal
            if(!GetComponent<AudioSource>().isPlaying) GetComponent<AudioSource>().Play();
        }
        return tiempoComienzoCanto >= tiempoDeCanto;
    }

    // Comienza a descansar, reseteando el temporizador
    public void Descansar()
    {
        cantando = false;
        tiempoComienzoDescanso = 0;
    }

    // Comprueba si tiene que dejar de descansar
    public bool TerminaDescansar()
    {
        NavMeshHit navHit;
        NavMesh.SamplePosition(transform.position, out navHit, 2f, NavMesh.AllAreas);
        if ((1 << NavMesh.GetAreaFromName("Bambalinas") & navHit.mask) != 0) tiempoComienzoDescanso += Time.deltaTime;  // Solo disminuye si esta en las bambalinas
        return tiempoComienzoDescanso >= tiempoDeDescanso;
    }

    // Comprueba si se encuentra en la celda
    public bool EstaEnCelda()
    {
        NavMeshHit navHit;
        NavMesh.SamplePosition(transform.position, out navHit, 2f, NavMesh.AllAreas);
        return (1 << NavMesh.GetAreaFromName("Celda") & navHit.mask) != 0;
    }

    // Comprueba si esta en un sitio desde el cual sabe llegar al escenario
    public bool ConozcoEsteSitio()
    {
        NavMeshHit navHit;
        NavMesh.SamplePosition(transform.position, out navHit, 2f, NavMesh.AllAreas);
        return (1 << NavMesh.GetAreaFromName("Escenario") & navHit.mask) != 0 ||
            (1 << NavMesh.GetAreaFromName("Bambalinas") & navHit.mask) != 0 ||
            (1 << NavMesh.GetAreaFromName("Palco Oeste") & navHit.mask) != 0 ||
            (1 << NavMesh.GetAreaFromName("Palco Este") & navHit.mask) != 0 ||
            (1 << NavMesh.GetAreaFromName("Butacas") & navHit.mask) != 0 ||
            (1 << NavMesh.GetAreaFromName("Pasillos Escenario") & navHit.mask) != 0;
    }

    //Mira si ve al vizconde con un angulo de vision y una distancia maxima
    public bool Scan()
    {
        Vector3 pos = transform.position;
        double fwdAngle = Vector3.Angle(transform.forward, objetivo.position - pos);

        if (fwdAngle < anguloVistaHorizontal && Vector3.Magnitude(pos - objetivo.position) <= distanciaVista)
        {
            RaycastHit vista;
            if (Physics.Raycast(pos, objetivo.position - pos, out vista, Mathf.Infinity) && vista.collider.gameObject.GetComponent<Player>())
            {
                return true;
            };
        }
        return false;
    }

    // Genera una posicion aleatoria a cierta distancia dentro de las areas permitidas
    private Vector3 RandomNavSphere(float distance) 
    { 
        Vector3 randomDirection = UnityEngine.Random.insideUnitSphere * distance;
        randomDirection += gameObject.transform.position;
        NavMeshHit navHit;
        do
        {
            randomDirection = UnityEngine.Random.insideUnitSphere * distance;
            randomDirection += gameObject.transform.position;
            NavMesh.SamplePosition(randomDirection, out navHit, distance, NavMesh.AllAreas);
        } while ((1 << NavMesh.GetAreaFromName("Escenario") & navHit.mask) == 0 &&
            (1 << NavMesh.GetAreaFromName("Bambalinas") & navHit.mask) == 0 &&
            (1 << NavMesh.GetAreaFromName("Palco Oeste") & navHit.mask) == 0 &&
            (1 << NavMesh.GetAreaFromName("Palco Este") & navHit.mask) == 0 &&
            (1 << NavMesh.GetAreaFromName("Butacas") & navHit.mask) == 0 &&
            (1 << NavMesh.GetAreaFromName("Walkable") & navHit.mask) == 0 &&
            (1 << NavMesh.GetAreaFromName("Jump") & navHit.mask) == 0 &&
            (1 << NavMesh.GetAreaFromName("Vestíbulo") & navHit.mask) == 0 &&
            (1 << NavMesh.GetAreaFromName("Sótano Este") & navHit.mask) == 0 &&
            (1 << NavMesh.GetAreaFromName("Sótano Oeste") & navHit.mask) == 0 &&
            (1 << NavMesh.GetAreaFromName("Celda") & navHit.mask) == 0 &&
            (1 << NavMesh.GetAreaFromName("Sótano Norte") & navHit.mask) == 0 &&
            (1 << NavMesh.GetAreaFromName("Música") & navHit.mask) == 0 &&
            (1 << NavMesh.GetAreaFromName("Pasillos Escenario") & navHit.mask) == 0);
        return navHit.position; 
    }

    // Genera un nuevo punto de merodeo cada vez que agota su tiempo de merodeo actual
    public void IntentaMerodear()
    {

        if ((transform.position - agente.destination).magnitude <= 0.1 + agente.stoppingDistance)
        {
            tiempoComienzoMerodeo -= Time.deltaTime;          
            if (tiempoComienzoMerodeo <= 0)
            {
                tiempoComienzoMerodeo = tiempoDeMerodeo;
                agente.SetDestination(RandomNavSphere(distanciaDeMerodeo));
            }
        }
    }
}
