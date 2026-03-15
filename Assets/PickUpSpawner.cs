using System.Collections.Generic;
using UnityEngine;

public class PickUpSpawner : MonoBehaviour
{
    [Header("Spawn Settings")]
    [SerializeField] private Transform spawnParent;
    [SerializeField] private Vector2 spawnAreaMin = new Vector2(-5.5f, -4.5f);
    [SerializeField] private Vector2 spawnAreaMax = new Vector2(9f, 1f);
    [SerializeField] private int amountToSpawn = 90;

    [Header("Pickup Prefabs Pool")]
    [SerializeField] private List<PickUpObject> pickupPrefabs = new();

    [Header("Layering")]
    [SerializeField] private int baseSortingOrder = 0;
    [SerializeField] private int sortingStep = 1;

    private readonly List<PickUpObject> spawnedPickups = new();

    public List<PickUpObject> SpawnedPickups => spawnedPickups;

    public void SpawnRandomPickups()
    {
        ClearSpawnedPickups();

        if (pickupPrefabs == null || pickupPrefabs.Count == 0)
        {
            Debug.LogWarning("no hay prefabs asignados");
            return;
        }

        for (int i = 0; i < amountToSpawn; i++)
        {
            PickUpObject selectedPrefab = GetWeightedRandomPrefab();
            if (selectedPrefab == null) continue;

            Vector2 randomLocalPosition = new Vector2(
                Random.Range(spawnAreaMin.x, spawnAreaMax.x),
                Random.Range(spawnAreaMin.y, spawnAreaMax.y)
            );

            Vector3 spawnWorldPosition = spawnParent != null
                ? spawnParent.TransformPoint(randomLocalPosition)
                : new Vector3(randomLocalPosition.x, randomLocalPosition.y, 0f);

            PickUpObject instance = Instantiate(
                selectedPrefab,
                spawnWorldPosition,
                Quaternion.identity,
                spawnParent
            );

            if (instance.SpriteRenderer != null)
            {
                instance.SpriteRenderer.sortingOrder = baseSortingOrder + (i * sortingStep);
            }

            spawnedPickups.Add(instance);
        }
    }

    private PickUpObject GetWeightedRandomPrefab()
    {
        float totalWeight = 0f;

        foreach (PickUpObject prefab in pickupPrefabs)
        {
            if (prefab == null) continue;
            if (prefab.Data == null) continue;
            if (prefab.Data.weight <= 0f) continue;

            totalWeight += prefab.Data.weight;
        }

        if (totalWeight <= 0f)
        {
            //Debug.LogWarning("PickUpSpawner: el peso total es 0.");
            return null;
        }

        float randomValue = Random.Range(0f, totalWeight);
        float accumulatedWeight = 0f;

        foreach (PickUpObject prefab in pickupPrefabs)
        {
            if (prefab == null) continue;
            if (prefab.Data == null) continue;
            if (prefab.Data.weight <= 0f) continue;

            accumulatedWeight += prefab.Data.weight;

            if (randomValue <= accumulatedWeight)
            {
                return prefab;
            }
        }

        return null;
    }

    public void ClearSpawnedPickups()
    {
        for (int i = spawnedPickups.Count - 1; i >= 0; i--)
        {
            if (spawnedPickups[i] != null)
            {
                Destroy(spawnedPickups[i].gameObject);
            }
        }

        spawnedPickups.Clear();
    }

    private void OnDrawGizmosSelected()
    {
        Transform reference = spawnParent != null ? spawnParent : transform;

        Vector3 bottomLeft = reference.TransformPoint(new Vector3(spawnAreaMin.x, spawnAreaMin.y, 0f));
        Vector3 bottomRight = reference.TransformPoint(new Vector3(spawnAreaMax.x, spawnAreaMin.y, 0f));
        Vector3 topLeft = reference.TransformPoint(new Vector3(spawnAreaMin.x, spawnAreaMax.y, 0f));
        Vector3 topRight = reference.TransformPoint(new Vector3(spawnAreaMax.x, spawnAreaMax.y, 0f));

        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(bottomLeft, bottomRight);
        Gizmos.DrawLine(bottomRight, topRight);
        Gizmos.DrawLine(topRight, topLeft);
        Gizmos.DrawLine(topLeft, bottomLeft);
    }
}