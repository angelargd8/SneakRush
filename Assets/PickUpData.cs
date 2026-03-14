using UnityEngine;

[CreateAssetMenu(fileName = "PickUpData", menuName = "Scriptable Objects/PickUpData")]
public class PickUpData : ScriptableObject
{
    [Header("Basic Info")]
    public string PickUpId;
    public string PickUpName;
    

    [Header("Visuals")]
    public Sprite Icon;

    [Header("gameplay")]
    public bool canBeCollected = true;
    public float weight = 1f;

    [Header("Audio SFX")]
    public AudioClip pickupSound;



}
