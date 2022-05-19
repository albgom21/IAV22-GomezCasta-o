using UnityEngine;

// Clase para saber si el hay hueco libre en el est. de batería
public class HuecoBateriaLibre : MonoBehaviour
{
    public bool libre = true;

    private void OnTriggerEnter(Collider other){ // Si entra algo en el hueco no se puede aparcar
        if (!other.CompareTag("PARKING"))
            libre = false;
    }

    private void OnTriggerExit(Collider other){ // Si sale algo que estba en el hueco ya se puede aparcar
        if (!other.CompareTag("PARKING"))
            libre = true;
    }

    public bool getLibre() { return libre; } // Método para BOLT
}
