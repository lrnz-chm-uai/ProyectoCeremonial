using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem; // Por defecto puedo usar WASD para mover al jugador, pero puedo cambiarlo en el Input System de Unity.
using TMPro; // TMPro me permite usar texto en 3D en Unity, lo cual es útil para mostrar información en el juego, como el contador de Red Pills y Blue Pills.

public class PlayerController : MonoBehaviour
{
    public float speed = 0; // Variable pública para controlar la velocidad del jugador, se puede ajustar desde el Inspector de Unity
    public TextMeshProUGUI redPillsCountText, bluePillsCountText; // Variables públicas para mostrar el contador de Red Pills y Blue Pills en la interfaz de usuario
    public TextMeshProUGUI gameOverText, redPillPickUpText, bluePillPickUpMessage;

    private Rigidbody rb; // Variable para almacenar el componente Rigidbody del jugador
    private float movementX;
    private float movementY;
    private Coroutine redPillCoroutine, bluePillCoroutine;


    private int redPillsCount, bluePillsCount;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>(); // Obtener el componente Rigidbody del jugador
        redPillsCount = 0; // Inicializar el contador de Red Pills
        bluePillsCount = 0;

        gameOverText.gameObject.SetActive(false);
        redPillPickUpText.gameObject.SetActive(false);
        bluePillPickUpMessage.gameObject.SetActive(false);
        SetCountTexts("Red");
        SetCountTexts("Blue");
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
            SetCountTexts("Red");
            SetPillPickUpText("Red");
        }
        else if (other.gameObject.CompareTag("BluePill")) 
        {
            other.gameObject.SetActive(false); 
            bluePillsCount++; 
            SetCountTexts("Blue");
            SetPillPickUpText("Blue");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy")) // Verificar si el objeto con el que colisiona
                                                      // el jugador tiene el tag "Enemy"
        {
            gameOverText.text = "Game over mate"; // TODO: el mensaje solo se muestre a partir del input del jugador
            gameOverText.gameObject.SetActive(true);

            // Pausar el juego explícitamente
            //Time.timeScale = 0f;

            rb.isKinematic = true; // Desactivar la física del jugador
            enabled = false; // Desactivar el script del jugador para evitar procesamiento adicional
            
            Destroy(gameObject, 1f); // Destruir el objeto con una pequeña pausa para permitir que el mensaje se renderice
            //Debug.Log("Colisión detectada"); // Para verificar que la colisión ocurre
            //Debug.Log("gameOverText asignado: " + (gameOverText != null)); // Verifica la referencia
        }
    }

    void SetCountTexts(string pillType) // Método para actualizar el texto de los contadores de Red Pills y Blue Pills en la interfaz de usuario
    {
        if (pillType == "Red")
        {
            redPillsCountText.text = "Red Pills: " + redPillsCount.ToString(); // Actualizar el texto del contador de Red Pills
        }
        else if (pillType == "Blue")
        {
            bluePillsCountText.text = "Blue Pills: " + bluePillsCount.ToString(); // Actualizar el texto del contador de Blue Pills
        }

        if (redPillsCount + bluePillsCount >= 8) // Si el jugador ha recogido al menos 4 Red Pills y 4 Blue Pills
        {
            // Destruye "Enemy"
            //Destroy(GameObject.FindGameObjectWithTag("Enemy"));

            // No destruir "Enemy", sino que lo detiene
            GameObject enemy = GameObject.FindGameObjectWithTag("Enemy");
            if (enemy != null)
            {
                EnemyMovement enemyMovement = enemy.GetComponent<EnemyMovement>();
                if (enemyMovement != null)
                {
                    enemyMovement.enabled = false;
                }
                UnityEngine.AI.NavMeshAgent agent = enemy.GetComponent<UnityEngine.AI.NavMeshAgent>();
                if (agent != null)
                {
                    agent.isStopped = true;
                    agent.velocity = Vector3.zero;
                }
            }
            gameOverText.gameObject.SetActive(true); // TODO: puedo resetear la escena?
        }
    }

    void SetPillPickUpText(string pillType)
    {
        if (pillType == "Red")
        {
            if (redPillCoroutine != null)
                StopCoroutine(nameof(ShowMessageForDuration)); // Detener la corrutina si ya se está ejecutando,
                                                           // para que no se solapen los mensajes
            redPillCoroutine = StartCoroutine(ShowMessageForDuration(redPillPickUpText.gameObject));
        }
        else if (pillType == "Blue")
        {
            if (bluePillCoroutine != null)
                StopCoroutine(nameof(ShowMessageForDuration));
            bluePillCoroutine = StartCoroutine(ShowMessageForDuration(bluePillPickUpMessage.gameObject));
        }
    }

    private IEnumerator ShowMessageForDuration(GameObject messageObject)
    {
        messageObject.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        messageObject.SetActive(false);
    }
}
