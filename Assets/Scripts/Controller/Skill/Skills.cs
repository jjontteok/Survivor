using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Skills : MonoBehaviour
{
    public static Skills Instance;
    public struct Skill
    {
        public int SkillNumber;
        public string SkillName;
        public Sprite SkillSprite;
        public string SkillExplain;
        public int SkillLevel;
    };

    public List<Skill> SkillList = new();
    public List<Skill> PlayerSkillList = new();
    public int PlayerSkillCount = 5;

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
        Skill defaultSkill = new Skill()
        {
            SkillNumber=0,
            SkillName = Define.skillName[0],
            SkillSprite = Define.SkillSprites[0],
            SkillExplain = Define.skillExplain[0],
            SkillLevel = 1,
        };
        PlayerSkillList.Add(defaultSkill);
        defaultSkill.SkillLevel = 2;
        SkillList.Add(defaultSkill);
        for(int i=1; i<10; i++)
        {
            SkillList.Add(new Skill()
            {
                SkillNumber = i,
                SkillName = Define.skillName[i],
                SkillSprite = Define.SkillSprites[i],
                SkillExplain = Define.skillExplain[i],
                SkillLevel = 1
            });
        }
        
    }
}
