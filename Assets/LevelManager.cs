using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using Unity.VisualScripting;

public class LevelManager : MonoBehaviour
{
    [Header("Level Settings")]
    [SerializeField] private float levelDuration = 60f;
    [SerializeField] private PickUpSpawner pickupSpawner;
    [SerializeField] AudioClip ambienceClip;

    private float timeRemaining;
    private bool levelActive = false;

    private int remainingPickups = 0;

    public float TimeRemaining => timeRemaining;
    public bool LevelActive => levelActive;

    //private List<PickUpObject> pickups; 

    private void OnEnable()
    {
        EventManager.OnPickupCollected += HandlePickupCollected;
    }

    private void OnDisable()
    {
        EventManager.OnPickupCollected -= HandlePickupCollected;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        AudioManager.Instance.PlayAmbience(ambienceClip);
        StartLevel();
    }

    // Update is called once per frame
    void Update()
    {
        if (!levelActive) return;

        timeRemaining -= Time.deltaTime;

        if (timeRemaining < 0f)
        {
            timeRemaining = 0f;
        }

        EventManager.TriggerTimerChanged(timeRemaining);

        if (timeRemaining == 0f)
        {
            EndLevel();
        }
    }
    
    public void StartLevel()
    {
        timeRemaining = levelDuration;
        levelActive = true;
        

        if (InventoryManager.Instance != null)
        {
            InventoryManager.Instance.ClearInventory();
        }


        if (pickupSpawner != null)
        {
            remainingPickups = pickupSpawner.SpawnRandomPickups();
        }
        else
        {
            remainingPickups = 0;
        }

        EventManager.TriggerLevelStarted();
        EventManager.TriggerTimerChanged(timeRemaining);
    }

    private void HandlePickupCollected(PickUpObject pickup)
    {
        if (!levelActive) return;
        if (remainingPickups <= 0) return;

        remainingPickups--;

        Debug.Log("Pickups restantes: " + remainingPickups);

        if (remainingPickups <= 0)
        {
            EndLevel();
        }
    }


    public void EndLevel()
    {
        if (!levelActive) return;

        levelActive = false;
        EventManager.TriggerLevelEnded();
        Debug.Log("Level ended");

        //creo que no es la mejor practica porque seria mejor en el game manager, pero asi lo dejare por el momento
        if (GameManager.instance != null)
        {
            Debug.Log("FINISH GAME");
            GameManager.instance.FinishGame();
        }



    }



}
