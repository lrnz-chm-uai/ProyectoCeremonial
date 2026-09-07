using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour // Este script controla la cámara en el juego,
                                              // permitiendo que siga al jugador.
{
    public GameObject player; // Variable pública para almacenar el objeto del jugador,
                              // se puede asignar desde el Inspector de Unity
    private Vector3 offset; // Variable privada para almacenar la diferencia de posición entre la cámara y el jugador

    // Start is called before the first frame update
    void Start()
    {
        // Necesito calcular la diferencia de posición entre la cámara y el jugador al inicio del juego,
        // para mantener esa diferencia constante mientras la cámara sigue al jugador.
        offset = transform.position - player.transform.position;
    }

    void LateUpdate()
    {
        // Necesito actualizar la posición de la cámara en cada frame del juego,
        // para que siga al jugador manteniendo la diferencia de posición calculada al inicio.
        // PERO debo usar LateUpdate en lugar de Update para asegurarme de que la cámara
        // se actualice después de que el jugador se haya movido.
        transform.position = player.transform.position + offset;
    }
}
