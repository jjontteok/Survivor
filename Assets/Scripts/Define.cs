using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.UI;

public class Define 
{
    #region Tag
    public const string PlayerTag = "Player";
    public const string AreaTag = "Area";
    public const string GroundTag = "Ground";
    public const string ProjectileTag = "Projectile";
    public const string SpikedBallTag = "SpikedBall";
    public const string RocketTag = "Rocket";
    public const string EnemyTag = "Enemy";
    public const string BossTag = "Boss";
    public const string BossBarrierTag = "BossBarrier";

    public const string HealthItemTag = "HealthItem";
    public const string MagneticItemTag = "MagneticItem";
    public const string CoinItemTag = "CoinItem";
    #endregion

    #region Animator
    public readonly static int isMoveHash = Animator.StringToHash("isMove");

    #endregion

    #region Enemy
    public enum EEnemy
    {
        Bee,
        BlueBird,
        Bunny,
        Chicken,
        Boss,
        Max,
    }
    #endregion

    #region Projectile
    public enum EProjectile
    {
        SpikedBall,
        SoccerBall,
        Brick,
        Boomerang,
        Rocket,
        Shield,
        Max,
    }
    #endregion

    #region Fruit
    public enum EFruit
    {
        Apple,
        Orange,
        Kiwi,
        Max,
    }
    #endregion

    #region SkillName
    public static Dictionary<int, string> skillName = new()
    {
        { 0, "스파이크공" },
        { 1, "축구공" },
        { 2, "벽돌" },
        { 3, "부메랑" },
        { 4, "로켓" },
        { 5, "방어막" },
        { 6, "런닝머신" },
        { 7, "고화력 총알" },
        { 8, "운동화" },
        { 9, "탄력 자석" },
    };
    #endregion

    #region SkillExplain
    public static Dictionary<int, string> skillExplain = new()
    {
        { 0, "스파이크공" + System.Environment.NewLine + "공격력 5% 증가" },
        { 1, "튀는 축구공 1개" + System.Environment.NewLine + "투척" },
        { 2, "벽돌 1개 투척" },
        { 3, "부메랑 1개 투척" },
        { 4, "폭발성 로켓 발사"},
        { 5, "주위에 용해되는" + System.Environment.NewLine + "방어막 생성" },
        { 6, "최대 체력 5% 증가" },
        { 7, "공격력 10% 증가" },
        { 8, "이동 속도" + System.Environment.NewLine + "10% 증가" },
        { 9, "아이템 획득 범위" + System.Environment.NewLine + "10% 증가" },
    };
    #endregion

    #region SkillExplain2
    public static Dictionary<int, string> skillExplain2 = new()
    {
        { 0, "스파이크공" + System.Environment.NewLine + "공격력 5% 증가" },
        { 1, "튀는 축구공 1개" + System.Environment.NewLine + "추가" },
        { 2, "벽돌 1개 추가" },
        { 3, "부메랑 1개 추가" },
        { 4, "폭발성 로켓 추가"},
        { 5, "용해되는" + System.Environment.NewLine + "방어막 크기 증가" },
        { 6, "최대 체력 5% 증가" },
        { 7, "공격력 10% 증가" },
        { 8, "이동 속도" + System.Environment.NewLine + "10% 증가" },
        { 9, "아이템 획득 범위" + System.Environment.NewLine + "10% 증가" },
    };
    #endregion

    #region SkillImage
    public static Sprite[] SkillSprites = Resources.LoadAll<Sprite>("Skill");
    #endregion
}

public struct CharacterInfo
{
    public float Atk;
    public float CurrentHp;
    public float MaxHp;
    public float Speed;
}