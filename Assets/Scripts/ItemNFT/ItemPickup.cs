using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public ItemNFT item;

    void PickUp()
    {
        Destroy(this.gameObject);
        InventoryManager.Instance.Add(item);
        Debug.Log("Pick up " + item.itemName);
    }

    private void OnMouseDown()
    {
        PickUp();
    }
}
