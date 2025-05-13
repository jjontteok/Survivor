using System.Collections;
using UnityEngine;

public class EnemyBoss : Enemy
{
    public GameObject BossBulletPrefab;
    public Transform LAttackPos;
    public Transform RAttackPos;
    Transform _bulletSpawnPos;
    SpriteRenderer _bossRend;
    Rigidbody2D _bossRb;
    Vector3 _dir;
    float _dashCoolTime = 7f;
    float _dashTime;
    bool _isDash = false;

    float _bulletCoolTime = 3f;
    float _bulletTime;


    public CharacterInfo BossInfo = new CharacterInfo()
    {
        Atk = 20,
        CurrentHp = 30,
        MaxHp = 30,
        Speed = 0.5f
    };

    public override CharacterInfo GetEnemyInfo()
    {
        EnemyInfo = BossInfo;
        return EnemyInfo;
    }

    protected override void Initialize()
    {
        base.Initialize();
        _bossRend = GetComponent<SpriteRenderer>();
        _bossRb = GetComponent<Rigidbody2D>();
        _bossRb.freezeRotation = true;
        _bossRb.linearVelocity = Vector3.zero;
        _bossRb.mass = 1;
    }

    protected override void Start()
    {
        base.Start();
    }

    protected override void FixedUpdate()
    {
        _dir = (_player.transform.position - transform.position).normalized;
        _bossRend.flipX = _dir.x > 0;
        DashTime();
        BossMove();
        FireBullet();
    }
    void DashTime()
    {
        if (!_isDash)
        {
            BossInfo.Speed = 0.5f;
            _dashTime += Time.deltaTime;
            if (_dashTime > _dashCoolTime)
            {
                _isDash = true;
                _dashTime = 1.5f;
            }
        }
        else 
        {
            if (_dashTime > 0) 
            { 
                BossInfo.Speed = 2.2f;
                _bossRb.mass = 1;
                _dashTime -= Time.deltaTime;
            }
            else 
            {
                _isDash = false;
            }
        }
    }
    void BossMove()
    {
        _bossRb.MovePosition(transform.position + _dir * BossInfo.Speed * Time.deltaTime);
    }

    void FireBullet()
    {
        _bulletTime += Time.deltaTime;
        if (_bulletTime > _bulletCoolTime)
        {
            _bulletTime -= _bulletTime;
            _bulletSpawnPos = _bossRend.flipX ? RAttackPos : LAttackPos;
            for(int i=-1; i<2; i++)
            {
                Instantiate(BossBulletPrefab, _bulletSpawnPos.position, Quaternion.Euler(0, 0, i*30));
            }
        }
    }
}
