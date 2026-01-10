using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopItemUI : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private Image icon;
    [SerializeField] private TextMeshProUGUI infoText;
    [SerializeField] private Button actionButton;
    [SerializeField] private TextMeshProUGUI buttonText;

    private SkinData skinData;
    private ShopManager shopManager;

    public void Init(SkinData data, ShopManager manager)
    {
        skinData = data;
        shopManager = manager;

        icon.sprite = skinData.skinIcon;
        infoText.text = $"{skinData.skinName}";

        actionButton.onClick.RemoveAllListeners();
        actionButton.onClick.AddListener(OnButtonClicked);

        UpdateState();
    }
    private void OnButtonClicked()
    {
        if (shopManager.IsSkinOwned(skinData))
        {
            shopManager.ChooseSkin(skinData);
        }
        else
        {
            if(shopManager.BuySkin(skinData))
            {
                UpdateState();
            }
        }
    }
    private void UpdateState()
    {
        if (shopManager.IsSkinOwned(skinData))
        {
            buttonText.text = "Select";
        }
        else
        {
            buttonText.text = "Buy: "+skinData.price;
        }
    }
}
