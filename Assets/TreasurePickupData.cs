using UnityEngine;

[CreateAssetMenu(fileName = "NewTreasurePickup", menuName = "Game/Pickups/Treasure Pickup")]
public class TreasurePickupData : PickUpData
{
    public PickUpType pickupType;
    public int scoreValue = 10;
    public int coinValue = 0;
    public bool isRare = false;
}