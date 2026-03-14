using UnityEngine;

[CreateAssetMenu(fileName = "PickUpData", menuName = "Scriptable Objects/PickUpData")]
public class PickUpData : ScriptableObject
{
    [Header("Basic Info")]
    public string pickUpId;
    public string pickUpName;

    [Header("Visuals")]
    public Sprite icon;

    [Header("Gameplay")]
    public bool canBeCollected = true;

}
