using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PosicionRayCast : MonoBehaviour
{
    public bool delante;
    private float dist = 0.0f;
    RaycastHit hit;
    Vector3 v;
    public float getDist() { return dist; }

    private void Awake()
    {
        if (delante) v = Vector3.forward;
        else v = Vector3.back;
    }

    void Update()
    {
        if (Physics.Raycast(transform.position, v, out hit, 5f))
        {
            if (hit.transform.CompareTag("APARCADO"))
            {
                Debug.DrawRay(transform.position, v * hit.distance, Color.red);
                dist = hit.distance;
                //print("Delante: "+delante + " Dist:" + hit.distance);
            }
        }
        else
        {
            Debug.DrawRay(transform.position, v * 5f, Color.green);
            dist = 0.0f;
        }
    }

}
