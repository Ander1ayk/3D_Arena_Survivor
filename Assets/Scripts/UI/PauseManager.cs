using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    [SerializeField] private GameObject UIInGame;
    [SerializeField] private GameObject PauseUI;
    [SerializeField] private GameObject GameOver;

    private float cooldown = 0.5f;
    private float lastPausedTime;
    private bool isPaused = false;
    private bool gameOverShown = false;
    PlayerStats playerStats;
    private void Start()
    {
        playerStats = FindFirstObjectByType<PlayerStats>();
    }
    private void Update()
    {
        if(playerStats.GetPlayerIsDead())
        {
            if(!gameOverShown)
            {
                ShowGameOver();
                gameOverShown = true;
            }
            return;
        }
        if (Input.GetKeyDown(KeyCode.Escape) && Time.unscaledTime - lastPausedTime > cooldown)
        {
            if (isPaused)
                ResumeGame();
            else
                PauseGame();

            lastPausedTime = Time.unscaledTime;
        }
    }
    private void PauseGame()
    {
       isPaused = true;
       UIInGame.SetActive(false);
       PauseUI.SetActive(true);
       Time.timeScale = 0f;
    }
    public void ResumeGame()
    {
        isPaused = false;
        UIInGame.SetActive(true);
        PauseUI.SetActive(false);
        Time.timeScale = 1f;
    }
    public void BackToMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
    }
    public void ShowGameOver()
    {
        isPaused = true;
        UIInGame.SetActive(false);
        PauseUI.SetActive(false);
        GameOver.SetActive(true);
    }
    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Arena");
    }
}
