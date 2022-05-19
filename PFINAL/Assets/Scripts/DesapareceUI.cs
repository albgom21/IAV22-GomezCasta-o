using UnityEngine;

// Clase para hacer desaparecer las instrucciones al cabo de X segundos
public class DesapareceUI : MonoBehaviour
{
    public float tiempoEnPantalla = 20f;
    bool once = false;

    private void Update()
    {    
        if (!once && gameObject.activeSelf && (Time.time >= tiempoEnPantalla)) {
            gameObject.SetActive(false);
            once = true;
        }
    }
}
