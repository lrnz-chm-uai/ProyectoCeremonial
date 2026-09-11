using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject startPanel;

    private bool isPaused;

    void Start()
    {
        ShowStartPanel(true);
        Time.timeScale = 0f;
    }

    void Update()
    {
        HandlePauseInput();
        HandleResetInput();
        HandleStartInput();
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
            Input.GetKeyDown(KeyCode.D))
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
    public void GameOver()
    {
        isPaused = true;
        Time.timeScale = 0f;
    }
}
