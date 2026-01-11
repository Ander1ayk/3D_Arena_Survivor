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

    [SerializeField] private Image selectedFrame;

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

        Subscribe();
        UpdateState();
    }
    private void OnDestroy()
    {
        Unsubscribe();
    }
    //private void OnEnable()
    //{
    //    if (Wallet.Instance != null)
    //        Wallet.Instance.OnMoneyChanged += OnMoneyChanged;
    //    shopManager.OnSkinChanged += UpdateState;
    //}

    //private void OnDisable()
    //{
    //    if (Wallet.Instance != null)
    //        Wallet.Instance.OnMoneyChanged -= OnMoneyChanged;
    //    shopManager.OnSkinChanged -= UpdateState;
    //}
    public void Subscribe()
    {
        if (Wallet.Instance != null)
            Wallet.Instance.OnMoneyChanged += OnMoneyChanged;

        if (shopManager != null)
            shopManager.OnSkinChanged += UpdateState;
    }

    public void Unsubscribe()
    {
        if (Wallet.Instance != null)
            Wallet.Instance.OnMoneyChanged -= OnMoneyChanged;

        if (shopManager != null)
            shopManager.OnSkinChanged -= UpdateState;
    }

    private void OnMoneyChanged(int money)
    {
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
        bool isOwned = shopManager.IsSkinOwned(skinData);
        bool isSelected = shopManager.GetCurrentSkin() == skinData; 
        if (isOwned)
        {
            buttonText.text = isSelected ? "Selected" : "Select";
            actionButton.interactable = !isSelected;
        }
        else
        {
            buttonText.text = "Buy: " + skinData.price;
            actionButton.interactable = Wallet.Instance.GetCurrentCoins() >= skinData.price;
        }
        if(selectedFrame != null)
        {
            selectedFrame.gameObject.SetActive(isSelected);
        }
    }
}
