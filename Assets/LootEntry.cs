using UnityEngine;

[System.Serializable]
public class LootEntry : MonoBehaviour
{
    public PickUpObject pickupPrefab;
    [Min(0)] public int weight = 1;
}
