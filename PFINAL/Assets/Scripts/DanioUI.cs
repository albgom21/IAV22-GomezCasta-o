using UnityEngine;
using UnityEngine.UI;

// Clase para mostrar por pantalla el daño a otros coches
public class DanioUI : MonoBehaviour
{
    public CarController coche;
    public Text textoDanio;

    private float danio = 0.0f;

    private void Update() {        
        danio = coche.getDanio();

        if (textoDanio != null)
            textoDanio.text = "Daño: " + (int)danio;
    }
}
