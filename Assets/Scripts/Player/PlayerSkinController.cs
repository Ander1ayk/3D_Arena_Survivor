using UnityEngine;

public class PlayerSkinController : MonoBehaviour
{
    [SerializeField] private Transform skinHolder;
    private GameObject currentSkinInstance;

    private void Start()
    {
        var shop = FindObjectOfType<ShopManager>();
        if(shop != null)
        {
            var skin = shop.GetCurrentSkin();
            if (skin != null)
            {
                SetSkin(skin);
            }
        }
    }
    public void SetSkin(SkinData skinData)
    {
        if(currentSkinInstance != null)
        {
            Destroy(currentSkinInstance);
        }
        currentSkinInstance = Instantiate(skinData.skinPrefab, skinHolder);

        currentSkinInstance.transform.localPosition = Vector3.zero;
        currentSkinInstance.transform.localRotation = Quaternion.identity;
        currentSkinInstance.transform.localScale = Vector3.one;
    }
}
