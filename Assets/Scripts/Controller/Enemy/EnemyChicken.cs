using UnityEngine;

public class EnemyChicken : Enemy
{
    public GameObject ChickenBulletPrefab;
    GameObject  player;
    float _distance;
    float _time;
    float _bulletTime = 3f;
    bool _isBulletTime;

    public CharacterInfo ChickenInfo = new CharacterInfo()
    {
        Atk = 8,
        CurrentHp = 5,
        MaxHp = 5,
        Speed = 0.9f
    };

    public override CharacterInfo GetEnemyInfo()
    {
        base.GetEnemyInfo();
        return ChickenInfo;
    }

    protected override void Initialize()
    {
        base.Initialize();
    }
    protected override void Start()
    {
        base.Start();
        player = GameObject.FindGameObjectWithTag(Define.PlayerTag);
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        _distance = (player.transform.position - 
            transform.position).sqrMagnitude;
        if (_distance > 10)
            return;
        FireBullet();
    }
    void FireBullet()
    {
        _time += Time.deltaTime;
        if (_time > _bulletTime)
        {
            _time -= _time;

            Instantiate(ChickenBulletPrefab,
                transform.position, Quaternion.identity);
        }
    }
}
