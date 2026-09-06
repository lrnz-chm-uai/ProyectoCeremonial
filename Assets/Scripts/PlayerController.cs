using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem; // Por defecto puedo usar WASD para mover al jugador, pero puedo cambiarlo en el Input System de Unity.

public class PlayerController : MonoBehaviour
{
    public float speed = 0; // Variable pública para controlar la velocidad del jugador, se puede ajustar desde el Inspector de Unity

    private Rigidbody rb; // Variable para almacenar el componente Rigidbody del jugador
    private float movementX;
    private float movementY;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>(); // Obtener el componente Rigidbody del jugador
    }

    void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();
        // La linea de arriba define un vector de movimiento en 2D, que se puede usar para mover al jugador en el juego.
        // Vector2 es una estructura que representa un vector en 2 dimensiones, con componentes x e y.
        // 'Get' es un método que obtiene el valor del input.
        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    private void FixedUpdate() // FixedUpdate se llama a intervalos fijos y es el lugar adecuado para manejar la física del juego
    {
        // Aquí se puede implementar la lógica de movimiento del jugador usando el vector de movimiento obtenido en OnMove.
        // Por ejemplo, se podría aplicar una fuerza al Rigidbody del jugador para moverlo en la dirección del vector de movimiento.
        Vector3 movement = new Vector3(movementX, 0.0f, movementY); // Crear un vector de movimiento en 3D a partir del vector de movimiento en 2D
                                                                    // 0.0f en la componente y indica que no hay movimiento vertical, solo horizontal.
        rb.AddForce(movement * speed);
    }
}
