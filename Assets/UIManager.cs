using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Text scoreText;
    [SerializeField] private Text moneyText;
    [SerializeField] private Text timeText;

    [SerializeField] private float currentTime = 60f;

    private void Start()
    {
        UpdateUI();
    }

    private void Update()
    {
        currentTime -= Time.deltaTime;

        if (currentTime < 0f)
            currentTime = 0f;

        UpdateUI();
    }

    private void UpdateUI()
    {
        if (ScoreManager.Instance != null)
        {
            //Debug.Log("Score:" + ScoreManager.Instance.Score);
            //Debug.Log("Money:" + ScoreManager.Instance.Money);

            scoreText.text = "Score: " + ScoreManager.Instance.Score;
            moneyText.text = "Money: $" + ScoreManager.Instance.Money;
        }
        else
        {
            Debug.Log("score manager doesnt exists");
        }

            timeText.text = "Time: " + Mathf.CeilToInt(currentTime);
    }
}