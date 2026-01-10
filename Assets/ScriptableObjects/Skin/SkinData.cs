using UnityEngine;
[CreateAssetMenu(menuName = "ScriptableObjects / SkinData")]
public class SkinData : ScriptableObject
{
    [Header("Basic info")]
    public string skinId;
    public string skinName;
    [Header("Visual")]
    public GameObject skinPrefab;
    public Sprite skinIcon;
    [Header("Shop")]
    public int price;
}
