using Unity.VectorGraphics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    [SerializeField] private GameObject UIInGame;
    [SerializeField] private GameObject PauseUI;

    private float cooldown = 0.5f;
    private float lastPausedTime;
    private bool isPaused = false;
    private void Update()
    {
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
}
