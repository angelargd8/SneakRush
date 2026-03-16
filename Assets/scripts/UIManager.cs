using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("Top UI")]
    [SerializeField] private Text scoreText;
    [SerializeField] private Text moneyText;
    [SerializeField] private Text timeText;
    [SerializeField] private float currentTime = 60f;

    [Header("Inventory UI")]
    [SerializeField] private Transform inventoryContainer;
    [SerializeField] private GameObject inventoryItemPrefab;

    private void Start()
    {
        UpdateUI();
        RefreshInventoryUI();
    }

    private void Update()
    {
        currentTime -= Time.deltaTime;

        if (currentTime < 0f)
            currentTime = 0f;

        UpdateUI();
    }

    private void OnEnable()
    {
        EventManager.OnPickupCollected += HandlePickupCollected;
    }

    private void OnDisable()
    {
        EventManager.OnPickupCollected -= HandlePickupCollected;
    }

    private void HandlePickupCollected(PickUpObject pickup)
    {
        RefreshInventoryUI();
    }

    private void UpdateUI()
    {
        if (ScoreManager.Instance != null)
        {
            scoreText.text = "Score: " + ScoreManager.Instance.Score;
            moneyText.text = "Money: $" + ScoreManager.Instance.Money;
        }

        timeText.text = "Time: " + Mathf.CeilToInt(currentTime);
    }

    private void RefreshInventoryUI()
    {
        if (inventoryContainer == null || inventoryItemPrefab == null || InventoryManager.Instance == null)
            return;

        for (int i = 0; i < inventoryContainer.childCount; i++)
        {
            Destroy(inventoryContainer.GetChild(i).gameObject);
        }

        List<InventoryItem> items = InventoryManager.Instance.Items;

        foreach (InventoryItem item in items)
        {
            if (item == null || item.data == null) continue;

            GameObject itemGO = Instantiate(inventoryItemPrefab, inventoryContainer);
            InventoryItemUI itemUI = itemGO.GetComponentInChildren<InventoryItemUI>();

            if (itemUI != null)
            {
                itemUI.Setup(item);
            }
        }
    }
}