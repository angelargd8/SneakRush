using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public LevelManager levelManager;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
    }

    public void FinishGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
    Application.Quit();
#endif

    }
}
