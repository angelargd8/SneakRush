using System;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public static Action<PickUpObject> OnPickupCollected;

    public static void  TriggerPickupCollected(PickUpObject pickUpObject)
    {
        Debug.Log($"EVENT -> Pickup collected: {pickUpObject.Data.pickUpName}");
        OnPickupCollected?.Invoke(pickUpObject);
    }
}
