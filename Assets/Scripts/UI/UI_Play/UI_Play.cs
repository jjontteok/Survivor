using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI_Play : UI_Base
{
    public static UI_Play Instance;
    public GameObject PlayerHpFrame;
    public Image PlayerHpBar;
    public Image FruitGaugeBar;
    public Image BossHpBar;
    public GameObject NormalPanel;
    public GameObject PausePanel;
    public GameObject BossPanel;
    public GameObject BossBarrier;
    public TMP_Text LevelText;
    public TMP_Text DeadEnemyCountText;
    public TMP_Text CoinCountText;
    private float _fruitCount;
    private float _maxFruitCount;
    private int _levelCount;
    private int _deadEnemyCount;
    private int _coinCount;

    public GameObject ChooseSkillPanel;
    public Image BlackOutCurtain;
    float _blackOutCurtainValue;
    float _blackOutCurtainSpeed;

    public Button PauseButton;
    public Image[] SkillImage;
    List<int> _skillList = new();
    public float FruitsCount
    {
        get => _fruitCount; 
        set => _fruitCount = value; 
    }

    public int DeadEnemyCount
    {
        get => _deadEnemyCount;
        set => _deadEnemyCount = value;
    }

    public int CoinCount
    {
        get => _coinCount;
        set => _coinCount = value;
    }
    GameObject _player;
    PlayerController _playerController;
    UI_ChooseSkill _uiChooseSkill;
    float _playerMaxHp;

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
        _uiChooseSkill= ChooseSkillPanel.GetComponent<UI_ChooseSkill>();
    }
    private void OnEnable()
    {
        BlackOutCurtain.gameObject.SetActive(true);
        _uiChooseSkill.OnSkillChoosed += OnSkillChoosed;
    }

    private void OnDisable()
    {
        _uiChooseSkill.OnSkillChoosed -= OnSkillChoosed;
    }

    void Start()
    {
        _player = GameObject.FindWithTag(Define.PlayerTag);
        _playerController = _player.GetComponent<PlayerController>();
        _playerMaxHp = _playerController.playerInfo.MaxHp;
        _fruitCount = 0;
        _maxFruitCount = 10;
        _levelCount = 1;
        _deadEnemyCount = 0;
        _blackOutCurtainValue = 1.0f;
        _blackOutCurtainSpeed=0.8f;
        PauseButton.onClick.AddListener(OnPauseButtonClick);
        gameObject.SetActive(true);
        BossBarrier.SetActive(false);
        GameManager.Instance.isGameOver = false;
        GameManager.Instance.isBossSpawn = false;
        PausePanel.SetActive(false);
        InitializePlayerSkill();
    }

    void InitializePlayerSkill()
    {
        SkillImage[0].sprite = Define.SkillSprites[0];
        _skillList.Add(0);
        for(int i=1; i < 5; i++)
        {
            Color color = SkillImage[i].color;
            color.a = 0;
            SkillImage[i].color = color;
        }
    }

    void Update()
    {
        if (_blackOutCurtainValue > 0)
        {
            HideBlackOutCurtain();
        }
        PlayerHpBarUpdate();
        FruitGaugeBarUpdate();
        LevelUpdate();
        BossSpawn();
        DeadEnemyUpdate();
        CoinUpdate();
        if (GameManager.Instance.isGameOver)
        {
            gameObject.SetActive(false);
            SceneManager.LoadScene("GameOver");
        }
    }
    private void FixedUpdate()
    {
        PlayerHpFrame.transform.position = Camera.main.WorldToScreenPoint(_player.transform.position
            + new Vector3(0, -0.35f, 0));
        
    }
    void HideBlackOutCurtain()
    {
        _blackOutCurtainValue -= Time.deltaTime * _blackOutCurtainSpeed;
        BlackOutCurtain.color = new Color(0.0f, 0.0f, 0.0f, _blackOutCurtainValue);
        if (_blackOutCurtainValue <= 0)
        {
            BlackOutCurtain.gameObject.SetActive(false);
        }
    }

    void PlayerHpBarUpdate()
    {
        PlayerHpBar.fillAmount = _playerController.playerInfo.CurrentHp / _playerMaxHp;
    }
    void FruitGaugeBarUpdate()
    {
        FruitGaugeBar.fillAmount = _fruitCount / _maxFruitCount;
        if (FruitGaugeBar.fillAmount == 1)
        {
            ChooseSkillPanel.SetActive(true);
            Time.timeScale = 0;
            _fruitCount %= _maxFruitCount;
            _maxFruitCount += _levelCount * 15;
            _levelCount++;
        }
    }
    void LevelUpdate()
    {
        LevelText.text = $"Lv. {_levelCount}";
    }

    void BossSpawn()
    {
        if (GameManager.Instance.isBossSpawn)
        {
            NormalPanel.SetActive(false);
            BossPanel.SetActive(true);
            BossBarrier.SetActive(true);
        }
    }

    void DeadEnemyUpdate()
    {
        DeadEnemyCountText.text = _deadEnemyCount.ToString();
    }

    void CoinUpdate()
    {
        CoinCountText.text = _coinCount.ToString();
    }

    void OnPauseButtonClick()
    {
        Time.timeScale = Time.timeScale = 0;
        PausePanel.SetActive(true);
    }

    void OnSkillChoosed(int skillIdx)
    {
        if (_skillList.Contains(skillIdx))
            return;
        _skillList.Add(skillIdx);
        SkillImage[_skillList.Count - 1].sprite = Define.SkillSprites[skillIdx];
        Color color = SkillImage[_skillList.Count - 1].color;
        color.a = 1;
        SkillImage[_skillList.Count - 1].color = color;
    }
}
