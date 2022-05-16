using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Retrovisor : MonoBehaviour
{
    [SerializeField] LayerMask layerMask;
    bool referencia = false;
    public bool front;
    RaycastHit hit;
    Vector3 v;


    void Update()
    {
        if (front)
            v = Vector3.forward;
        else
            v = transform.parent.TransformDirection(Vector3.back);        

        if (Physics.Raycast(transform.position, v, out hit, 10f, layerMask))
        {
            Debug.DrawRay(transform.position, v * hit.distance, Color.red);
            referencia = true;
        }
        else
        {
            Debug.DrawRay(transform.position, v * 10f, Color.green);
            referencia = false;
        }
    }
    public bool getReferencia() { return referencia; }
}