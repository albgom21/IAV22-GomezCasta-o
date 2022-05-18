using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HuecoBateriaLibre : MonoBehaviour
{
    public bool libre;
    private void OnTriggerEnter(Collider other)
    {
        libre = false;
    }
    private void OnTriggerExit(Collider other)
    {
        libre = true;
    }
    public bool getLibre() { return libre; }
}
