using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI_GameOver : UI_Base
{
    public Button ToMainButton;
    public Button RetryButton;
    public TMP_Text ScoreText;

    private void Start()
    {
        ToMainButton.onClick.AddListener(OnToMainButtonClick);
        RetryButton.onClick.AddListener(OnRetryButtonClick);
        int score = UI_Play.Instance.CoinCount + UI_Play.Instance.DeadEnemyCount;
        ScoreText.text = $"Score : {score}";
    }

    void OnToMainButtonClick()
    {
        SceneManager.LoadScene("Main");
    }

    void OnRetryButtonClick()
    {
        SceneManager.LoadScene("Game");
    }
}
