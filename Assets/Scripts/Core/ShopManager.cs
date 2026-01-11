using System;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    [Header("Shop Settings")]
    [SerializeField] private List<SkinData> availableSkins;
    private List<SkinData> ownedSkins = new List<SkinData>();
    private SkinData currentSkin;
    public List<SkinData> GetAvailableSkins() => availableSkins;
    private ShopSaveData shopSaveData;

    public event Action OnSkinChanged;
    private void Awake()
    {
        shopSaveData = SaveService.Load();
        foreach(var skinId in shopSaveData.ownedSkins)
        {
            var skin = availableSkins.Find(s => s.skinId == skinId);
            if(skin != null)
            {
                ownedSkins.Add(skin);
            }
        }
        if (!string.IsNullOrEmpty(shopSaveData.selectedSkinId))
        {
            currentSkin = availableSkins.Find(s => s.skinId == shopSaveData.selectedSkinId);
        }
    }
    private bool CheckSkinIsBought(SkinData data)
    {
        if(ownedSkins.Contains(data))
        {
            Debug.Log("Skin already owned: " + data.skinName);
            return true;
        }
        else
        {
            Debug.Log("Skin not owned: " + data.skinName);
            return false;
        }
    }
    public bool BuySkin(SkinData data)
    {
        if (CheckSkinIsBought(data))
        {
            Debug.Log("Cannot buy skin, already owned: " + data.skinName);
            return false;
        }
        else
        {
            if (Wallet.Instance.SpendCoins(data.price))
            {
                ownedSkins.Add(data);
                shopSaveData.ownedSkins.Add(data.skinId);
                SaveService.SaveShop(shopSaveData);
                Debug.Log("Skin purchased: " + data.skinName);
                return true;
            }
            else
            {
                Debug.Log("Not enough coins to buy skin: " + data.skinName);
                return false;
            }
        }
    }
    public void ChooseSkin(SkinData data)
    {
        if (!IsSkinOwned(data)) return;

        currentSkin = data;
        shopSaveData.selectedSkinId = data.skinId;
        SaveService.SaveShop(shopSaveData);

        FindAnyObjectByType<PlayerSkinController>().SetSkin(data);

        OnSkinChanged?.Invoke();
    }
    //private void SaveChoice(SkinData data)
    //{
    //    PlayerPrefs.SetString("ChosenSkin", data.skinId);
    //    Debug.Log("Saved chosen skin: " + currentSkin.skinName);
    //}
    public bool IsSkinOwned(SkinData data)
    {
        return ownedSkins.Contains(data);
    }
    public SkinData GetCurrentSkin()
    {
        return currentSkin;
    }

}
