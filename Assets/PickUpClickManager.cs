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
            HandleClick();
        }
    }

    private void HandleClick()
    {
        Vector3 mouseWorld = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        Vector2 point = new Vector2(mouseWorld.x, mouseWorld.y);

        Collider2D[] hits = Physics2D.OverlapPointAll(point);

        //Debug.Log("Hits: " + hits.Length);

        if (hits.Length == 0) return;

        PickUpObject topPickup = null;
        int highestSortingOrder = int.MinValue;

        foreach (Collider2D hit in hits)
        {
            PickUpObject pickup = hit.GetComponent<PickUpObject>();
            if (pickup == null) continue;

            int sortingOrder = pickup.SpriteRenderer.sortingOrder;
            //Debug.Log($"Pickup: {pickup.name} | sortingOrder: {sortingOrder}");

            if (sortingOrder > highestSortingOrder)
            {
                highestSortingOrder = sortingOrder;
                topPickup = pickup;
            }
        }

        if (topPickup != null)
        {
            bool blocked = topPickup.IsBlockedByAnotherPickup();
            //Debug.Log($"Se intentó recolectar: {topPickup.name} | blocked: {blocked}");

            topPickup.TryCollect();
        }
    }
}