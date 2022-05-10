# PROYECTO FINAL - INTELIGENCIA ARTIFICIAL
## NOMBRE DEL PROYECTO
---
ESTACIONAMIENTO DE AUTOMÓVIL CON IA

## AUTOR
---
ALBERTO GÓMEZ CASTAÑO

## VÍDEO DEMOSTRATIVO
---
## RESUMEN DEL ENUNCIADO
---
## DESCRIPCIÓN DEL PUNTO DE PARTIDA
---
Proyecto de Unity creado desde 0 en el que se ha añadido la herramienta de Bolt y los siguientes elementos y comportamientos. 

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

| **Tecla** | **Nombre de cámara** | **Descripción** |
| --- | --- | --- |
| 1 | MainCamera | Sigue al coche desde una perspectiva de 3ª persona |
| 2 | TopCamera | Sigue al coche desde una posición cenital |
| 3 | LeftCamera | Sigue al coche desde la parte izquierda del mismo |
| 4 | RightCamera | Sigue al coche desde la parte derecha del mismo  |

Comportamientos implementados mediante scripts:

- CameraFollow.cs: seguimiento de una entidad, tanto en posición como en rotación con un offset.

- Follow.cs: seguimiento de la posición de una entidad con un offset.

- CameraManager.cs: gestión de las distintas cámaras dependiendo de la tecla pulsada.

- DesapareceUI.cs: desactiva el gameobject que lo contiene tras X segundos establecidos. (Usado para la UI)

- Velocimetro.cs: cálculo de la velocidad del coche y mostrarlo en la interfaz.

- CarController.cs: movimiento dinámico del coche.

- SonidoCoche.cs: simula el sonido del motor del coche en función de su velocidad.

## PSEUDOCÓDIGO Y DIAGRAMAS
---

## CONTROLES
---
Movimiento del coche:

<img src='README images/wasd.png' height="300">

Freno:

<img src='README images/spacebar.png'>

Activar IA para estacionamiento:

<img src='README images/e.png' height="200">

## PROPUESTA
---
Desarrollo de un proyecto en Unity que implementa la IA implicada en el aparcamiento de coches, como posible característica de un juego de coches. El jugador podrá mover un coche mediante teclado de la forma más cercana a la realidad siempre con la perspectiva de un juego que no aspira al realismo extremo, en un mapa en el que haya aparcamientos (huecos entre otros coches ya aparcados). Una vez que el jugador haya probado a aparcar el coche en los huecos que vea, también se podrá activar la IA mediante la pulsación de una tecla y que de esta forma el coche se controle solo para completar el estacionamiento. La implementación se basará en el uso de sensores,  implementados con Raycasts y/o Triggers que ayuden a recoger información del entorno para que dicha información se use en una máquina de estados (con la herramienta BOLT) y vaya guiando el proceso de aparcamiento.

La duda que se me presenta es el alcance del proyecto, si con un solo tipo de aparcamiento tendría un buen alcance o en cambio se queda algo corto y sería mejor integrar varios tipos de aparcamientos, como pueden ser por ejemplo en paralelo, batería... O también poder añadir otros comportamientos a coches que sean NPCs y circulen de forma simple por el mapa.

Preview del aparcamiento en paralelo:
<img src='README images/Ejemplo.png'>
