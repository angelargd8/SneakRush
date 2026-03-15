using UnityEngine;

[CreateAssetMenu(fileName = "NewTreasurePickup", menuName = "Game/Pickups/Treasure Pickup")]
public class TreasurePickupData : PickUpData
{
    [Header("Treasure Info")]
    public PickUpType pickUpType;
    public int scoreValue = 10;
    public int coinValue = 0;
    public float weight = 1f;
    public bool addToInventory = true;
    
    [Header("Audio SFX")]
    public AudioClip pickupSound;
}