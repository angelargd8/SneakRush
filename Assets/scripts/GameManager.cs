using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public LevelManager levelManager;
    [SerializeField] private GameObject endGamePanel;
    [SerializeField] private Text finalScoreText;
    [SerializeField] private Text finalMoneyText;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
    }

    public void Start()
    {
        if (endGamePanel != null)
        {
            endGamePanel.SetActive(false);
        }
    }

    public void FinishGame(int finalScore, int finalMoney)
    {
        if (endGamePanel != null)
        {
            endGamePanel.SetActive(true);
        }

        if (finalScoreText != null)
        {
            finalScoreText.text = "Score: " + finalScore;
        }

        if (finalMoneyText != null)
        {
            finalMoneyText.text = "Money: $" + finalMoney;
        }

        // pausar el juego
        Time.timeScale = 0f;

        StartCoroutine(QuitAfterDelay());
    

    }

    private IEnumerator QuitAfterDelay()
    {
        yield return new WaitForSecondsRealtime(5f);
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
    }
}
