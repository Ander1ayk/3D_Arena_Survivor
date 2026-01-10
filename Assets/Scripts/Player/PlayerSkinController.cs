using UnityEngine;

public class PlayerSkinController : MonoBehaviour
{
    [SerializeField] private Transform skinHolder;
    private GameObject currentSkinInstance;

    public void SetSkin(SkinData skinData)
    {
        if(currentSkinInstance != null)
        {
            Destroy(currentSkinInstance);
        }
        currentSkinInstance = Instantiate(skinData.skinPrefab, skinHolder);

        currentSkinInstance.transform.localPosition = Vector3.zero;
        currentSkinInstance.transform.localRotation = Quaternion.identity;
    }
}
