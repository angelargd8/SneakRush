using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Collider2D))]
public class PickUpObject : MonoBehaviour
{
    [Header("Pickup Data")]
    [SerializeField] private TreasurePickupData pickUpData;

    private SpriteRenderer spriteRenderer;
    private Collider2D objectCollider;
    private bool isCollected = false;

    public TreasurePickupData Data => pickUpData;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        objectCollider = GetComponent<Collider2D>();
        ApplyData();
    }

    private void OnValidate()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        ApplyData();
    }

    private void ApplyData()
    {
        if (pickUpData == null) return;

        if (spriteRenderer != null && pickUpData.icon != null)
        {
            spriteRenderer.sprite = pickUpData.icon;
        }

        gameObject.name = pickUpData.pickUpName;
    }

    private void OnMouseDown()
    {
        TryCollect();
    }

    public void TryCollect()
    {
        if (isCollected) return;
        if (pickUpData == null) return;
        if (!pickUpData.canBeCollected) return;

        isCollected = true;

        if (objectCollider != null)
        {
            objectCollider.enabled = false;
        }

        EventManager.TriggerPickupCollected(this);

        Destroy(gameObject);
    }
}