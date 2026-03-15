using UnityEngine;

public class PickUpClickManager : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;

    private void Awake()
    {
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            TryCollectTopPickupUnderMouse();
        }
    }

    private void TryCollectTopPickupUnderMouse()
    {
        if (mainCamera == null) return;

        Vector3 mouseWorldPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        Vector2 point = new Vector2(mouseWorldPosition.x, mouseWorldPosition.y);

        Collider2D[] hits = Physics2D.OverlapPointAll(point);

        if (hits == null || hits.Length == 0) return;

        PickUpObject topPickup = null;
        int highestSortingOrder = int.MinValue;

        foreach (Collider2D hit in hits)
        {
            PickUpObject pickup = hit.GetComponent<PickUpObject>();
            if (pickup == null) continue;
            if (pickup.SpriteRenderer == null) continue;

            int currentOrder = pickup.SpriteRenderer.sortingOrder;

            if (currentOrder > highestSortingOrder)
            {
                highestSortingOrder = currentOrder;
                topPickup = pickup;
            }
        }

        if (topPickup != null)
        {
            topPickup.TryCollect();
        }
    }
}