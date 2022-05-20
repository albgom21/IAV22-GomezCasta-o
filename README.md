# PROYECTO FINAL - INTELIGENCIA ARTIFICIAL
## AUTOR
ALBERTO GÓMEZ CASTAÑO
## NOMBRE DEL PROYECTO
ESTACIONAMIENTO DE AUTOMÓVIL CON IA

<img src='README images/logo.png' height="200">


## VÍDEO DEMOSTRATIVO
Enlace: 
## RESUMEN DEL ENUNCIADO
### RESUMEN GENERAL
Construir una IA implicada en el aparcamiento autónomo sobre un juego de coches. El jugador puede moverse libremente con su coche por el mapa. Una vez escogido un hueco para el estacionamiento, el jugador podrá elegir entre aparcar el mismo el coche o por el contrario, dejar que la IA lo haga por él. Para el correcto funcionamiento de la IA, el coche debe encontrarse recto dentro del carril adyacente al hueco. 

### PROYECTO
Proyecto en Unity que implementa la IA implicada en el aparcamiento de coches, tanto en paralelo como en batería, como posible característica de un juego de coches. El jugador podrá mover un coche mediante teclado de la forma más cercana a la realidad siempre con la perspectiva de un juego que no aspira al realismo. Se desarrolla en un mapa en el que hay aparcamientos (huecos entre otros coches ya aparcados) y una zona de Parking para estacionar en batería. Una vez que el jugador haya probado a aparcar el coche en los huecos que vea, también podrá activar la IA mediante la pulsación de la tecla **E** y que de esta forma el coche se controle solo para completar el estacionamiento. La implementación se basará en el uso de sensores,  construidos con Raycasts y/o Triggers que ayuden a recoger información del entorno. Esta información será la base sobre la que trabaje la máquina de estados (con la herramienta Bolt) y vaya guiando el proceso de aparcamiento. La velocidad de IA puede ser modificada una vez esté activa, ya sea para aumentarla o disminuirla. La velocidad del coche al estar bajo el control de la IA se ve capada a una determinada velocidad. La IA se puede activar y desactivar en cualquier momento, pero su funcionamiento correcto exige dejar actuar a la IA de forma ininterrumpida, ya que como elección de diseño, al desactivarla vuelve al estado inicial en lugar de seguir por el estado en el que se apagó. Además, se contará con un indicador de daños/colisiones ocasionados a otros coches.

Para comprobar la eficiencia de la IA, se implementará un cronómetro que haga ver el tiempo transcurrido mientras actúa la IA y así poder compararlo al que tarda el jugador en realizar la maniobra de estacionamiento, que también tiene un cronómetro que se activa/desactiva con la tecla **C**.

_Trabajo de diseño_:
- Documentación del proyecto y pseudocódigo.

_Funcionalidades_:
- Crear un mapa lleno de posibles zonas de aparcamiento.
- Desarrollo de la IA para aparcamiento paralelo mediante una máquina de estados.
- Desarrollo de la IA para aparcamiento en batería mediante una máquina de estados.
- Contabilizar las veces que el coche ha chocado con otros y mostrarlo en la interfaz.
- Establecer un cronómetro para ver comparar los resultados de la IA con los del jugador, mostrándolo por pantalla.
- Establecer un cronómetro manual para medir los tiempos del jugador, mostrándolo por pantalla.
- Activar y desactivar la IA con la pulsación de la tecla **E**.
- Modificar la velocidad de la IA mediante teclado.

_Codificar herramientas que permitan establecer de forma rápida ejemplos sobre los que probar el funcionamiento_:
- Tecla **R** para el reinicio de la escena (Volver a zona de estacionamientos en paralelo).
- Tecla **T** para mover el coche a la zona del Parking (Zona de estacionamientos en batería).


Interfaz final:

<img src='README images/interfaz.png'>

## DESCRIPCIÓN DEL PUNTO DE PARTIDA
<img src='README images/puntopartida.png'>

Resumen: el punto de partida implementa un mapa con carreteras en el que el jugador puede mover un coche para poder aparcarlo manualmente entre otros dos coches aparcados. También implementa una interfaz básica que presenta los controles y la velocidad del vehículo.


Este proyecto de Unity se ha creado desde 0, se ha añadido la herramienta de Bolt y los  elementos y comportamientos descritos a continuación. 

Elementos en la escena de Unity:
- Mapa con carreteras. Creado mediante uso de assets gratuitos de la Asset Store de Unity.
- Canvas. Contiene elementos UI que muestran por pantalla información relevante sobre el juego como el velocímetro asociado al coche del jugador o las instrucciones de los controles que desaparecen al transcurrir 5 segundos.
- Coches aparcados. Vehículos estacionados con separación entre sí que no se pueden mover.
- Coche. Vehículo funcional que puede controlar el jugador, con sonido del motor.

| **Tecla** | **Función**|
| --- | --- |
| W | Acelerar |
| A | Girar a la izquierda |
| S | Marcha atrás |
| D | Girar a la derecha |
| Espacio | Frenar |
- Cámaras. Cuatro tipos de cámaras que el jugador podrá cambiar entre ellas mediante la pulsación de teclas.

| **Tecla** | **Nombre de cámara** | **Descripción** | **Imagen** |
| --- | --- | --- | --- |
| 1 | MainCamera | Sigue al coche desde una perspectiva de 3ª persona | <img src='README images/mcam.png'> |
| 2 | TopCamera | Sigue al coche desde una posición cenital | <img src='README images/tcam.png'> |
| 3 | LeftCamera | Sigue al coche desde la parte izquierda del mismo | <img src='README images/lcam.png'> |
| 4 | RightCamera | Sigue al coche desde la parte derecha del mismo  | <img src='README images/rcam.png'>|

Comportamientos implementados mediante scripts:

- CameraFollow.cs: seguimiento de una entidad, tanto en posición como en rotación con un offset.

- Follow.cs: seguimiento de la posición de una entidad con un offset.

- CameraManager.cs: gestión de las distintas cámaras dependiendo de la tecla pulsada.

- DesapareceUI.cs: desactiva el gameobject que lo contiene tras X segundos establecidos. (Usado para la UI)

- Velocimetro.cs: cálculo de la velocidad del coche y mostrarlo en la interfaz.

- CarController.cs: movimiento dinámico del coche.

- SonidoCoche.cs: simula el sonido del motor del coche en función de su velocidad.

## PSEUDOCÓDIGO Y DIAGRAMAS
Diagrama usado en bolt para el estacionamiento en paralelo con referencia (un coche delante y otro detrás)
<img src='README images/diagramaPasos.png'>

Diagrama implementado en Bolt:
<img src='README images/boltparalelo.png'>


Diagrama usado en bolt para el estacionamiento en batería.
<img src='README images/diagramaPasosB.png'>

Diagrama implementado en Bolt:
<img src='README images/boltbateria.png'>


Método para la comprobación de referencias del coche mediante Raycast:
```
//Este método puede implementarse de forma similar pero con distinto objetivo según las necesidades

    RaycastHit hit;

    if (Physics.Raycast(transform.position, Vector3.forward, out hit, 10f)){
        //Obtener los datos que nos devuelve hit, el objeto alcanzado por el rayo
        Vector3 posicion = hit.point;
        floar distancia = hit.distance;
        string nombre = hit.collider.gameObject.name;

        if (hit.transform.tag == "Aparcado"){
            hit.transform.gameObject.GetComponent<miScript>().miFuncion();
            //Pintar el rayo para depurar
            Debug.DrawRay( transform.position, Vector3.forward * hit.point, Color.green);
        }
        else {
            //Pintar el rayo para depurar
            Debug.DrawRay( transform.position, Vector3.forward * hit.point, Color.red);
        }
    }
```

Método para la comprobación del estado del coche mediante triggers:
```
   private void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag == "Hueco")
            //dependiendo del objetivo de la función
    }

```

Método para controlar el número de colisiones con otros coches:
```
    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.tag == "Aparacado")
            damage++;
    }
```

## CONTROLES
Movimiento del coche:

<img src='README images/wasd.png' height="300">

Freno:

<img src='README images/spacebar.png'>

Activar/desactivar IA para estacionamiento:

<img src='README images/e.png' height="200">

Activar/desactivar cronómetro manual:

<img src='README images/c.png' height="200">

Cambiar cámaras:

<img src='README images/1234.png' height="200">

Mostrar controles:

<img src='README images/h.png' height="200">

Reiniciar:

<img src='README images/r.png' height="200">

Mover coche al Parking:

<img src='README images/t.png' height="200">

Aumentar velocidad IA:

<img src='README images/uparrow.png' height="200">

Disminuir velocidad IA:

<img src='README images/downarrow.png' height="200">



## REFERENCIAS
- Millington, I.: Artificial Intelligence for Games. CRC Press, 3rd Edition (2019)
- Tutorial para el movimiento simple del coche: https://www.youtube.com/watch?v=Z4HA8zJhGEk&ab_channel=GameDevChef
- Recursos:
    - Asset coches: https://assetstore.unity.com/packages/3d/vehicles/land/arcade-free-racing-car-161085
    - Asset mapa: https://assetstore.unity.com/packages/3d/environments/roadways/low-poly-road-pack-67288
    - Asset cielo: https://assetstore.unity.com/packages/3d/simple-sky-cartoon-assets-42373
    - Asset barreras: https://assetstore.unity.com/packages/3d/environments/urban/concrete-plastic-barrier-72966

## PROPUESTA
Desarrollo de un proyecto en Unity que implementa la IA implicada en el aparcamiento de coches, como posible característica de un juego de coches. El jugador podrá mover un coche mediante teclado de la forma más cercana a la realidad siempre con la perspectiva de un juego que no aspira al realismo extremo, en un mapa en el que haya aparcamientos (huecos entre otros coches ya aparcados). Una vez que el jugador haya probado a aparcar el coche en los huecos que vea, también se podrá activar la IA mediante la pulsación de una tecla y que de esta forma el coche se controle solo para completar el estacionamiento. La implementación se basará en el uso de sensores,  implementados con Raycasts y/o Triggers que ayuden a recoger información del entorno para que dicha información se use en una máquina de estados (con la herramienta BOLT) y vaya guiando el proceso de aparcamiento.

La duda que se me presenta es el alcance del proyecto, si con un solo tipo de aparcamiento tendría un buen alcance o en cambio se queda algo corto y sería mejor integrar varios tipos de aparcamientos, como pueden ser por ejemplo en paralelo, batería... O también poder añadir otros comportamientos a coches que sean NPCs y circulen de forma simple por el mapa.

Preview del aparcamiento en paralelo:
<img src='README images/Ejemplo.png'>
