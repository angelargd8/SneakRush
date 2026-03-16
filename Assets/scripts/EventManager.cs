using System;
using UnityEngine;

public static class EventManager
{
    public static Action<PickUpObject> OnPickupCollected;
    public static Action OnLevelStarted;
    public static Action OnLevelEnded;
    public static Action<float> OnTimerChanged;

    public static void TriggerPickupCollected(PickUpObject pickUpObject)
    {
        OnPickupCollected?.Invoke(pickUpObject);
    }

    public static void TriggerLevelStarted()
    {
        OnLevelStarted?.Invoke();
    }

    public static void TriggerLevelEnded()
    {
        OnLevelEnded?.Invoke();
    }

    public static void TriggerTimerChanged(float timeRemaining)
    {
        OnTimerChanged?.Invoke(timeRemaining);
    }
}