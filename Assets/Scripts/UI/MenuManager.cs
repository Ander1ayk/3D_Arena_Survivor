using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [Header("Main")]
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject preSettings;
    [Header("PreSettings")]
    [SerializeField] private GameObject controlInfo;
    [SerializeField] private GameObject audioInfo;
    public void Settings()
    {
        mainMenu.SetActive(false);
        preSettings.SetActive(true);
    }
    public void BackToMenu()
    {
        preSettings.SetActive(false);
        mainMenu.SetActive(true);
    }
    public void OpenAudio()
    {
        audioInfo.SetActive(true);
        preSettings.SetActive(false);
    }
    public void CloseAudio()
    {
        audioInfo.SetActive(false);
        preSettings.SetActive(true);
    }
    public void OpenControl()
    {
        controlInfo.SetActive(true);
        preSettings.SetActive(false);
    }
    public void CloseControl()
    {
        controlInfo.SetActive(false);
        preSettings.SetActive(true);
    }
    public void ExitGame()
    {
        Application.Quit();
    }
    public void StartGame()
    {
        SceneManager.LoadScene("Arena");
    }
    public void UpgradeMenu()
    {
        SceneManager.LoadScene("UpgradeMenu");
    }
}
