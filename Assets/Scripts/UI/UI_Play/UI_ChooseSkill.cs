using NUnit.Framework;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class UI_ChooseSkill : UI_Base
{
    public Action<int> OnSkillChoosed;
    [HideInInspector]
    public List<int> RandomSkillList = new();
    public Button[] ChooseSkillButton;
    public TMP_Text[] SkillNameText;
    public TMP_Text[] SkillExplainText;
    public Image[] SkillImage;
    public TMP_Text[] SkillLevel;

    Skills _skills;
    PlayerController _playerController;
    ProjectileController _projectileController;
    void Awake()
    {
        _projectileController = GameObject.Find("ProjectileController").
            GetComponent<ProjectileController>();
        _playerController = GameObject.FindWithTag(Define.PlayerTag).
            GetComponent<PlayerController>();
        _skills = GetComponent<Skills>();
    }
    private void OnEnable()
    {
        GetSkillList();
        DisplaySkill();
    }

    private void OnDisable()
    {
        SkillListClear();
    }

    //0에서 9까지의 스킬 중 3개를 뽑는다.
    void GetSkillList()
    {
        int rndNum;
        if (_skills.PlayerSkillList.Count < 5)
        {
            rndNum = Random.Range(0, 10);
            for(int i=0; i < 3;)
            {
                if (RandomSkillList.Contains(rndNum) ||
                    _skills.SkillList[rndNum].SkillLevel>=4)
                {
                    rndNum = UnityEngine.Random.Range(0, 10);
                }
                else
                {
                    RandomSkillList.Add(rndNum);
                    i++;
                }
            }
        }
        else
        {
            int[] playerSkillNums = new int[5];
            for(int i=0; i < 5; i++)
            {
                playerSkillNums[i] = _skills.PlayerSkillList[i].SkillNumber;
            }
            rndNum = Random.Range(0, 5);
            for(int i=0; i<3;)
            {
                if (RandomSkillList.Contains(playerSkillNums[rndNum]) || 
                    _skills.SkillList[playerSkillNums[rndNum]].SkillLevel >= 4)
                {
                    rndNum = UnityEngine.Random.Range(0, 5);
                }
                else
                {
                    RandomSkillList.Add(playerSkillNums[rndNum]);
                    i++;
                }
            }
        }
    }

    void SkillListClear()
    {
        RandomSkillList.Clear();
    }

    void DisplaySkill()
    {
        for(int i=0; i < RandomSkillList.Count; i++)
        {
            int idx = RandomSkillList[i];
            SkillNameText[i].text = _skills.SkillList[idx].SkillName;
            SkillExplainText[i].text = _skills.SkillList[idx].SkillExplain;
            SkillImage[i].sprite = _skills.SkillList[idx].SkillSprite;
            SkillLevel[i].text = $"Lv.{_skills.SkillList[idx].SkillLevel}";
        }
    }

    //1레벨 스킬 - 2레벨 스킬 - 

    public void OnChooseSkillButtonClick(int idx)
    {
        int skillIdx = RandomSkillList[idx];
        int skillLevel = _skills.SkillList[skillIdx].SkillLevel;
        Skills.Skill mySkill = _skills.SkillList[skillIdx];
        if (_projectileController != null&&skillLevel<=3)
        {
            switch (skillIdx)
            {
                case 0:
                    GameObject projectilePool = GameObject.Find("ProjectilePool");
                    SpikedBall[] spikedBalls = projectilePool.
                        GetComponentsInChildren<SpikedBall>(true);
                    for(int i=0; i < spikedBalls.Length; i++)
                    {
                        spikedBalls[i].SpikedBallInfo.Atk *= 1.1f;
                    }
                    break;
                case 1: case 2: case 3: case 4: case 5:
                    _projectileController.IsProjectileActive[RandomSkillList[idx]] = true; break;
                case 6:
                    _playerController.playerInfo.MaxHp *= (1 + 0.1f * skillLevel); break;
                case 7:
                    _playerController.playerInfo.Atk += skillLevel; break;
                case 8:
                    _playerController.playerInfo.Speed *= (1 + 0.1f * skillLevel); break;
                case 9:
                    _playerController.MagnetRange += 0.3f * skillLevel; break;
            }
            gameObject.SetActive(false);
            if (_skills.PlayerSkillList.Count <= 5)
            {
                bool isPlayerSkill = false;
                for(int i=0; i < _skills.PlayerSkillList.Count; i++)
                {
                    if (_skills.PlayerSkillList[i].SkillNumber == 
                        _skills.SkillList[skillIdx].SkillNumber)
                    {
                        isPlayerSkill = true; break;
                    }
                }
                if (!isPlayerSkill) 
                {
                    _skills.PlayerSkillList.Add(_skills.SkillList[skillIdx]);
                }
                mySkill.SkillLevel = ++skillLevel;
                mySkill.SkillExplain = Define.skillExplain2[skillIdx];
                _skills.SkillList[skillIdx] = mySkill;
                OnSkillChoosed?.Invoke(skillIdx);
            }
            Time.timeScale = 1;
        }
    }
}
