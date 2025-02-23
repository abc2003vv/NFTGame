using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;
    public List<ItemNFT> items = new List<ItemNFT>();
    public Transform itemParent;
    public GameObject itemPrefab;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void Add(ItemNFT item)
    {
        items.Add(item);
    }

    public void DisplayInventory()
    {
        foreach (ItemNFT item in items)
        {
            GameObject itemObject = Instantiate(itemPrefab, itemParent);

            var itemName = itemObject.transform.Find("ItemName").GetComponent<TMPro.TextMeshProUGUI>();
            var itemImage = itemObject.transform.Find("ItemImage").GetComponent<Image>();

            itemName.text = item.itemName;
            itemImage.sprite = item.itemIcon;
        }
    }
}
