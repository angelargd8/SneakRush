using UnityEngine;
using System;

[Serializable]
public class InventoryItem 
{
    public TreasurePickupData data;
    public int amount;
    public InventoryItem(TreasurePickupData data, int amount)
    {
        this.data = data;
        this.amount = amount;

    }
}
