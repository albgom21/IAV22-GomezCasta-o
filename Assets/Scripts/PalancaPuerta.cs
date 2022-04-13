﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Se encarga de abrir o cerrar la puerta de la celda en contacto con otro objeto, actualizando la variable gate del Blackboard
 */

public class PalancaPuerta : MonoBehaviour
{
    public GameBlackboard Blackboard;

    public Transform gate;

    public float step = 0.2f;
    public float altura = 5.8f;

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Cantante>() || other.gameObject.GetComponent<Player>()) return;

        Interact();
    }

    public void Interact()
    {
        Blackboard.gate = !Blackboard.gate;
        if (!Blackboard.gate)
        {
            gate.GetComponent<Rigidbody>().isKinematic = false;
        } else
        {
            gate.GetComponent<Rigidbody>().isKinematic = true;
        }

    }
    private void FixedUpdate()
    {
        if (Blackboard.gate && gate.transform.position.y < altura)
        {
            gate.transform.Translate(new Vector3(0, step, 0));
        }
    }
}
