using UnityEngine;

[CreateAssetMenu(fileName = "ItemNFT", menuName = "Inventiory/ItemNFT")]
public class ItemNFT : ScriptableObject
{
    public int id;
    public string itemName;
    public int itemValue;
    public Sprite itemIcon;
}
