using System;
using System.Collections;
using TMPro;
using UnityEngine;
public class Timer : MonoBehaviour
{

    public static Timer Instance;
    [SerializeField]
    private TMP_Text _timerText;
    public float CurrentTime;

    private int _minute;
    private int _second;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        
        CurrentTime = 0;
        StartCoroutine(StartTimer());
    }
    private void Update()
    {
        if (GameManager.Instance.isGameOver)
             StopAllCoroutines();
    }
    IEnumerator StartTimer()
    {
        while (CurrentTime < 600)
        {
            CurrentTime += Time.deltaTime;
            DisplayTimer();
            yield return null;
            if (CurrentTime >= 480)
            {
                CurrentTime = 0;
                yield break;
            }
        }
    }

    void DisplayTimer()
    {
        _minute = (int)CurrentTime / 60;
        _second = (int)CurrentTime % 60;
        _timerText.text = _minute.ToString("00") + ":" + _second.ToString("00");
    }
}
