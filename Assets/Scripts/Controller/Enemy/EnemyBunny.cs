using UnityEngine;

public class EnemyBunny : Enemy
{
    public CharacterInfo BunnyInfo = new CharacterInfo()
    {
        Atk = 7,
        CurrentHp = 1,
        MaxHp = 1,
        Speed = 1.2f,
    };

    public override CharacterInfo GetEnemyInfo()
    {
        base.GetEnemyInfo();
        return BunnyInfo;
    }

    protected override void Initialize()
    {
        base.Initialize();
    }
}
