using TMPro;
using UnityEngine;

public class UpgradeMenuUI : MonoBehaviour
{
    public TextMeshProUGUI coinsText;
    private void Update()
    {
        coinsText.text = $"Coins: {Wallet.Instance.GetCurrentCoins().ToString()}";
    }
    public void BackToMenu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Menu");
    }
    public void ResetProgress()
    {
        PlayerPrefs.DeleteAll();
        Wallet.Instance.ResetMoney();
        PlayerStatsSaveService.DeletePlayerStats();
        PlayerProgressSaveService.DeletePlayerProgress();
        SaveService.DeleteShopData();
        UnityEngine.SceneManagement.SceneManager.LoadScene("Menu");
    }
}
