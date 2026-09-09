using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI; // Importante para usar el sistema de navegación de Unity,
                      // que permite a los enemigos moverse de manera inteligente por el escenario evitando obstáculos.

public class EnemyMovement : MonoBehaviour
{
    public Transform player; // Variable pública para almacenar la posición del jugador,
                             // se puede asignar desde el Inspector de Unity
    private NavMeshAgent agent; // Variable privada para almacenar el componente NavMeshAgent del enemigo,
                                // que permite que el enemigo se mueva por el escenario siguiendo la malla de navegación.
                                // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            agent.SetDestination(player.position); // Necesito actualizar la posición de destino del enemigo
                                                   // en cada frame del juego, para que siga al jugador.
        }
    }
}