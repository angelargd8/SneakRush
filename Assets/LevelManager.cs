using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [Header("Level Settings")]
    [SerializeField] private float levelDuration = 60f;
    [SerializeField] private PickUpSpawner pickupSpawner;

    private float timeRemaining;
    private bool levelActive = false;

    public float TimeRemaining => timeRemaining;
    public bool LevelActive => levelActive;




    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
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

        if (timeRemaining <= 0f)
        {
            EndLevel();
        }
    }
    
    public void StartLevel()
    {
        timeRemaining = levelDuration;
        levelActive = true;

        if (pickupSpawner != null)
        {
            pickupSpawner.SpawnRandomPickups();
        }

        EventManager.TriggerLevelStarted();
        EventManager.TriggerTimerChanged(timeRemaining);
    }

    public void EndLevel()
    {
        if (!levelActive) return;

        levelActive = false;
        EventManager.TriggerLevelEnded();

        Debug.Log("Level ended");
    }



}
