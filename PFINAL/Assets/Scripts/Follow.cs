using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{

    public Transform objetivo;

    public Vector3 offset;
    
    void Update()
    {
        transform.position = objetivo.position + offset;
    }
}