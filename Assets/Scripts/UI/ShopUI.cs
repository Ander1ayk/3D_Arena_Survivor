using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopUI : MonoBehaviour
{
    [SerializeField] private Transform container;
    [SerializeField] private ShopItemUI itemPrefab;
    [SerializeField] private ShopManager shopManager;

    private void Start()
    {
        foreach (SkinData skin in shopManager.GetAvailableSkins())
        {
            ShopItemUI item = Instantiate(itemPrefab, container);
            item.Init(skin, shopManager);
        }
    }
}
