using UnityEngine;

public class EnemyBee : Enemy
{
    public CharacterInfo BeeInfo = new CharacterInfo()
    {
        Atk = 5,
        CurrentHp = 2,
        MaxHp = 2,
        Speed = 0.5f,
    };

    public override CharacterInfo GetEnemyInfo()
    {
        base.GetEnemyInfo();
        return BeeInfo;
    }

    protected override void Initialize()
    {
        base.Initialize();
    }
}
