using System.Collections;
using UnityEngine;

public class Shield : Projectile
{
    SpriteRenderer _rend;
    CircleCollider2D _coll;
    GameObject _player;
    PlayerController _playerController;
    WaitForSeconds _activeTime = new WaitForSeconds(3f);
    Vector3 _moveDir;

    public Sprite[] ShieldSprites;

    public CharacterInfo ShieldInfo = new()
    {
        Atk = 0.05f,
        Speed = 3
    };

    protected override CharacterInfo GetProjectileInfo()
    {
        base.GetProjectileInfo();
        return ShieldInfo;
    }

    protected override void Initialize()
    {
        base.Initialize();
        _rend = GetComponent<SpriteRenderer>();
        _coll = GetComponent<CircleCollider2D>();
        _player = GameObject.FindGameObjectWithTag(Define.PlayerTag);
        _playerController = _player.GetComponent<PlayerController>();
    }

    private void OnEnable()
    {
        _coll.radius = 0.9f + (Skills.Instance.SkillList[5].SkillLevel - 2) * 0.3f;
        _rend.sprite = ShieldSprites[Skills.Instance.SkillList[5].SkillLevel - 2];
        transform.position = _player.transform.position;
        StartCoroutine(ActiveTime());
    }

    IEnumerator ActiveTime()
    {
        yield return _activeTime;
        gameObject.SetActive(false);
    }

    private void Update()
    {
        transform.position = _player.transform.position;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        Enemy enemy = collision.GetComponent<Enemy>();
        IDamageable damageable = collision.GetComponent<IDamageable>();
        if (damageable != null && collision.CompareTag(Define.EnemyTag)
            || collision.CompareTag(Define.BossTag))
        {
            damageable.AnyDamage(ShieldInfo.Atk+ _playerController.playerInfo.Atk, 
                _player, (int)Define.EProjectile.Shield);
        }
    }
}
