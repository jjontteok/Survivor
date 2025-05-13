using UnityEngine;

public class EnemyBlueBird : Enemy
{
    SpriteRenderer _blueBirdRend;
    Rigidbody2D _blueBirdRb;
    Vector3 _dir;
    float _distance;

    public CharacterInfo BlueBirdInfo = new CharacterInfo()
    {
        Atk = 6,
        CurrentHp = 3,
        MaxHp = 3,
        Speed = 0.6f,
    };

    public override CharacterInfo GetEnemyInfo()
    {
        base.GetEnemyInfo();
        return BlueBirdInfo;
    }
    protected override void Initialize()
    {
        base.Initialize();
        _blueBirdRend = GetComponent<SpriteRenderer>();
        _blueBirdRb = GetComponent<Rigidbody2D>();
        _blueBirdRb.freezeRotation = true;
        _blueBirdRb.linearVelocity = Vector3.zero;
    }

    protected override void Start()
    {
        base.Start();
    }

    protected override void FixedUpdate()
    {
        _dir = (_player.transform.position - transform.position).normalized;
        _distance = (_player.transform.position - transform.position).sqrMagnitude;
        _blueBirdRend.flipX = _dir.x > 0;

        if (_distance > 1.5f && _distance < 9)
        {
            BlueBirdInfo.Speed = 2f;
        }
        else
        {
            BlueBirdInfo.Speed = 0.7f;
        }
        _blueBirdRb.MovePosition(transform.position + _dir * BlueBirdInfo.Speed * Time.deltaTime);
    }
}
