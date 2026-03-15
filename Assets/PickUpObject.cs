using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Collider2D))]

public class PickUpObject : MonoBehaviour
{
    [Header("Pickup Data")]
    [SerializeField] private TreasurePickupData pickUpData;

    [Header("Block Settings")]
    [SerializeField] private float minBlockArea = 0.04f;

    private SpriteRenderer spriteRenderer;
    private Collider2D objectCollider;
    private bool isCollected = false;

    private readonly Collider2D[] overlapResults = new Collider2D[20];

    public TreasurePickupData Data => pickUpData;
    public SpriteRenderer SpriteRenderer => spriteRenderer;
    public Collider2D ObjectCollider => objectCollider;

    //public bool shouldAddToInventory { get {  return shouldAddToInventory; } }

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        objectCollider = GetComponent<Collider2D>();
        ApplyData();
    }

    private void OnValidate()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        objectCollider = GetComponent<Collider2D>();
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

    public bool IsBlockedByAnotherPickup()
    {
        if (objectCollider == null || spriteRenderer == null) return false;

        ContactFilter2D filter = new ContactFilter2D();
        filter.useTriggers = true;
        filter.NoFilter();

        int count = objectCollider.Overlap(filter, overlapResults);

        Bounds myBounds = objectCollider.bounds;

        for (int i = 0; i < count; i++)
        {
            Collider2D otherCollider = overlapResults[i];

            if (otherCollider == null) continue;
            if (otherCollider == objectCollider) continue;

            PickUpObject otherPickup = otherCollider.GetComponent<PickUpObject>();
            if (otherPickup == null) continue;
            if (otherPickup.SpriteRenderer == null) continue;

            if (otherPickup.SpriteRenderer.sortingOrder <= spriteRenderer.sortingOrder)
                continue;

            Bounds otherBounds = otherCollider.bounds;

            float overlapWidth = Mathf.Min(myBounds.max.x, otherBounds.max.x) - Mathf.Max(myBounds.min.x, otherBounds.min.x);
            float overlapHeight = Mathf.Min(myBounds.max.y, otherBounds.max.y) - Mathf.Max(myBounds.min.y, otherBounds.min.y);

            if (overlapWidth <= 0f || overlapHeight <= 0f)
                continue;

            float overlapArea = overlapWidth * overlapHeight;

            if (overlapArea >= minBlockArea)
            {
                return true;
            }
        }

        return false;
    }

    public void TryCollect()
    {
        if (isCollected) return;
        if (pickUpData == null) return;
        if (!pickUpData.canBeCollected) return;
        if (IsBlockedByAnotherPickup()) return;

        isCollected = true;

        if (objectCollider != null)
        {
            objectCollider.enabled = false;
        }

        if (ScoreManager.Instance != null)
        {
            ScoreManager.Instance.AddPickup(pickUpData);
        }

        if (InventoryManager.Instance != null)
        {
            InventoryManager.Instance.AddItem(pickUpData);
        }

        EventManager.TriggerPickupCollected(this);
        Destroy(gameObject);
    }
}