using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject startPanel;
    public GameObject countdownClock, noTimeLeftMessage;
    public float levelTimeLimit = 30f; // Tiempo límite para el nivel en segundos.

    private bool isPaused;
    private float remainingTime;

    void Start()
    {
        remainingTime = levelTimeLimit;
        countdownClock.GetComponent<TextMeshProUGUI>().text = "Level ends in: " + levelTimeLimit;
        ShowStartPanel(true);
        Time.timeScale = 0f; // Pausa el juego al inicio para mostrar el panel de inicio.
    }

    void Update()
    {
        HandleStartInput();
        HandlePauseInput();
        HandleResetInput();
        UpdateCountdown();
    }

    /// Alterna la pausa del juego con la tecla P.
    private void HandlePauseInput()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            TogglePause();
        }
    }

    /// Reinicia la escena actual con la tecla R.
    private void HandleResetInput()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            RestartGame();
        }
    }

    /// Detecta WASD para ocultar el mensaje inicial y comenzar a jugar.
    private void HandleStartInput()
    {
        if (startPanel == null || !startPanel.activeSelf)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.W) ||
            Input.GetKeyDown(KeyCode.A) ||
            Input.GetKeyDown(KeyCode.S) ||
            Input.GetKeyDown(KeyCode.D) ||
            Input.GetKeyDown(KeyCode.UpArrow) ||
            Input.GetKeyDown(KeyCode.DownArrow) ||
            Input.GetKeyDown(KeyCode.LeftArrow) ||
            Input.GetKeyDown(KeyCode.RightArrow))
        {
            ShowStartPanel(false);
            Time.timeScale = 1f;
        }
    }

    /// Activa o desactiva el panel inicial.
    private void ShowStartPanel(bool show)
    {
        if (startPanel != null)
        {
            startPanel.SetActive(show);
        }
    }

    /// Alterna el estado de pausa del juego.
    public void TogglePause()
    {
        if (startPanel != null && startPanel.activeSelf)
        {
            return;
        }

        isPaused = !isPaused;
        Time.timeScale = isPaused ? 0f : 1f;
    }

    /// Recarga la escena actual para reiniciar la partida.
    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    /// Permite reanudar el juego cuando termina la partida y se presiona R.
    /// Se puede invocar desde PlayerController si se detecta la muerte del jugador.
    
    void UpdateCountdown()
    {
        if (isPaused || startPanel.activeSelf) return;

        remainingTime -= Time.unscaledDeltaTime;
        countdownClock.GetComponent<TextMeshProUGUI>().text = "Level ends in: " + Mathf.Max(0, remainingTime).ToString("F1");

        if (remainingTime <= 0)
        {
            noTimeLeftMessage.SetActive(true);
            GameOver();
        }
    }

    public void GameOver()
    {
        isPaused = true;
        Time.timeScale = 0f;
    }
}
