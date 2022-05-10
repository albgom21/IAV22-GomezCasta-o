using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DesapareceUI : MonoBehaviour
{
    public float timeWhenDisappear = 5f;

    private void Update()
    {
        if (gameObject.activeSelf && (Time.time >= timeWhenDisappear))
            gameObject.SetActive(false);
    }
}