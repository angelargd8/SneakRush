using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Collider2D))]
public class PickUpObject : MonoBehaviour
{
    [Header("Data")]
    [SerializeField] private TreasurePickupData pickUpData;

    private SpriteRenderer spriteRenderer;
    private Collider2D col2D;
    private bool collected = false;

    public TreasurePickupData Data => pickUpData;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        col2D = GetComponent<Collider2D>();
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
        if (collected) return;
        if (pickUpData == null) return;
        if (!pickUpData.canBeCollected) return;

        collected = true;

        EventManager.TriggerPickupCollected(this);

        Destroy(gameObject);
    }
}