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
        { 0, "������ũ��" },
        { 1, "�౸��" },
        { 2, "����" },
        { 3, "�θ޶�" },
        { 4, "����" },
        { 5, "��" },
        { 6, "���׸ӽ�" },
        { 7, "��ȭ�� �Ѿ�" },
        { 8, "�ȭ" },
        { 9, "ź�� �ڼ�" },
    };
    #endregion

    #region SkillExplain
    public static Dictionary<int, string> skillExplain = new()
    {
        { 0, "������ũ��" + System.Environment.NewLine + "���ݷ� 5% ����" },
        { 1, "Ƣ�� �౸�� 1��" + System.Environment.NewLine + "��ô" },
        { 2, "���� 1�� ��ô" },
        { 3, "�θ޶� 1�� ��ô" },
        { 4, "���߼� ���� �߻�"},
        { 5, "������ ���صǴ�" + System.Environment.NewLine + "�� ����" },
        { 6, "�ִ� ü�� 5% ����" },
        { 7, "���ݷ� 10% ����" },
        { 8, "�̵� �ӵ�" + System.Environment.NewLine + "10% ����" },
        { 9, "������ ȹ�� ����" + System.Environment.NewLine + "10% ����" },
    };
    #endregion

    #region SkillExplain2
    public static Dictionary<int, string> skillExplain2 = new()
    {
        { 0, "������ũ��" + System.Environment.NewLine + "���ݷ� 5% ����" },
        { 1, "Ƣ�� �౸�� 1��" + System.Environment.NewLine + "�߰�" },
        { 2, "���� 1�� �߰�" },
        { 3, "�θ޶� 1�� �߰�" },
        { 4, "���߼� ���� �߰�"},
        { 5, "���صǴ�" + System.Environment.NewLine + "�� ũ�� ����" },
        { 6, "�ִ� ü�� 5% ����" },
        { 7, "���ݷ� 10% ����" },
        { 8, "�̵� �ӵ�" + System.Environment.NewLine + "10% ����" },
        { 9, "������ ȹ�� ����" + System.Environment.NewLine + "10% ����" },
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