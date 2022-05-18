using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BateriaRayCast : MonoBehaviour
{
    [SerializeField] LayerMask layerMask;
    RaycastHit hit;
    bool referencia = false;
    void Update()
    {
        if (Physics.Raycast(transform.position, Vector3.left, out hit, 5f, layerMask))
        {

            Debug.DrawRay(transform.position, Vector3.left * hit.distance, Color.red);
            referencia = true;

        }
        else
        {
            Debug.DrawRay(transform.position, Vector3.left * 5f, Color.green);
            referencia = false;
        }
    }

    public bool getReferencia() { return referencia; }
}
