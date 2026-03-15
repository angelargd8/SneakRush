using UnityEngine;

public class PickUpDebugListener : MonoBehaviour
{
    private void OnEnable()
    {
        EventManager.OnPickupCollected += HandlePickupCollected;
    }

    private void OnDisable()
    {
        EventManager.OnPickupCollected -= HandlePickupCollected;
    }

    private void HandlePickupCollected(PickUpObject pickUpObject)
    {
        TreasurePickupData data = pickUpObject.Data;

        Debug.Log(
            $"Collected -> Name: {data.pickUpName}, Type: {data.pickUpType}, Score: {data.scoreValue}, Coins: {data.coinValue}"
        );
    }
}