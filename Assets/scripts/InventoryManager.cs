using UnityEngine;
using System.Collections.Generic;
using JetBrains.Annotations;
public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance {  get; private set; }
    private List<InventoryItem> items = new List<InventoryItem>();
    public List<InventoryItem> Items => items;

    private void Awake()
    {
        if (Instance !=null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public void AddItem(TreasurePickupData data)
    {
        if (data == null) return;

        if (!data.addToInventory) return;

        InventoryItem existingItem = items.Find(item => item.data == data);

        if (existingItem != null)
        {
            existingItem.amount++;

        }
        else
        {
            items.Add(new InventoryItem(data, 1));
        }

    }

    public void ClearInventory()
    {
        items.Clear();
    }

}
