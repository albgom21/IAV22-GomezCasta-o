using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HuecoBateriaLibre : MonoBehaviour
{
    public bool libre = true;
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("PARKING"))
            libre = false;
    }
    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("PARKING"))
            libre = true;
    }
    public bool getLibre() { return libre; }
}
