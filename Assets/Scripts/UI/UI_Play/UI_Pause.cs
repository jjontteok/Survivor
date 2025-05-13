using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI_Pause : MonoBehaviour
{
    public Button PlayButton;
    public Button RetryButton;
    public Button ToMainButton;

    private void Start()
    {
        PlayButton.onClick.AddListener(OnPlayButtonClick);
        RetryButton.onClick.AddListener(OnRetryButtonClick);
        ToMainButton.onClick.AddListener(OnToMainButtonClick);
    }

    void OnPlayButtonClick()
    {
        gameObject.SetActive(false);
        Time.timeScale = 1.0f;
    }

    void OnRetryButtonClick()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("Game");
    }

    void OnToMainButtonClick()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("Main");
    }
}
