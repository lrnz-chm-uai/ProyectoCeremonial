using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem; // Por defecto puedo usar WASD para mover al jugador, pero puedo cambiarlo en el Input System de Unity.
using TMPro; // TMPro me permite usar texto en 3D en Unity, lo cual es útil para mostrar información en el juego, como el contador de Red Pills y Blue Pills.

public class PlayerController : MonoBehaviour
{
    public float speed = 0; // Variable pública para controlar la velocidad del jugador, se puede ajustar desde el Inspector de Unity
    
    private Rigidbody rb; // Variable para almacenar el componente Rigidbody del jugador
    private float movementX;
    private float movementY;
    
    private int redPillsCount, bluePillsCount;
    public TextMeshProUGUI redPillsCountText, bluePillsCountText; // Variables públicas para mostrar el contador de Red Pills y Blue Pills en la interfaz de usuario

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>(); // Obtener el componente Rigidbody del jugador
        redPillsCount = 0; // Inicializar el contador de Red Pills
        bluePillsCount = 0;

        SetCountTexts();
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

    void SetCountTexts() // Método para actualizar el texto de los contadores de Red Pills y Blue Pills en la interfaz de usuario
    {
        redPillsCountText.text = "Red Pills: " + redPillsCount.ToString(); // Actualizar el texto del contador de Red Pills
        bluePillsCountText.text = "Blue Pills: " + bluePillsCount.ToString(); // Actualizar el texto del contador de Blue Pills
    }

    private void FixedUpdate() // FixedUpdate se llama a intervalos fijos y es el lugar adecuado para manejar la física del juego
    {
        // Aquí se puede implementar la lógica de movimiento del jugador usando el vector de movimiento obtenido en OnMove.
        // Por ejemplo, se podría aplicar una fuerza al Rigidbody del jugador para moverlo en la dirección del vector de movimiento.
        Vector3 movement = new Vector3(movementX, 0.0f, movementY); // Crear un vector de movimiento en 3D a partir del vector de movimiento en 2D
                                                                    // 0.0f en la componente y indica que no hay movimiento en el eje y, solo en el plano 2D (x, z).
        rb.AddForce(movement * speed);
    }

    // Metodo para detectar colisiones con coleccionables y desactivarlos
    void OnTriggerEnter(Collider other) // Para que este método funcione, el jugador debe tener un Collider con la opción "Is Trigger" activada,
                                        // y los objetos coleccionables deben tener un Collider y un Rigidbody (aunque sea kinematic y que no se
                                        // vea afectado por la gravedad). Esto es para que Unity lo interprete como un objeto dinamico
                                        // y no estatico.
    {
        if (other.gameObject.CompareTag("RedPill")) // Verificar si el objeto con el que colisiona el jugador tiene el tag "RedPill"
                                                 // Esto lo hago mediante prefabs
        {
            other.gameObject.SetActive(false); // Desactivar el objeto coleccionable al colisionar con el jugador
            redPillsCount++; // Incrementar el contador de Red Pills recogidos
        }
        else if (other.gameObject.CompareTag("BluePill")) 
        {
            other.gameObject.SetActive(false); 
            bluePillsCount++; 
        }
        SetCountTexts(); // Actualizar el texto de los contadores de Red Pills y Blue Pills en la interfaz de usuario
    }
}
