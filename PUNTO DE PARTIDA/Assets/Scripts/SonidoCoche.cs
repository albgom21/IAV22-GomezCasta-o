using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SonidoCoche : MonoBehaviour
{
    AudioSource audioSource;
    Rigidbody rb;

    private float pitchFromCar;
    private float minimum = 0.85f;
    private float maximum = 2f;

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        pitchFromCar = Mathf.Lerp(minimum, maximum, (rb.velocity.magnitude * 3.6f / 100));
        audioSource.pitch = pitchFromCar;
    }
}