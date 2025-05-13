using System.Collections;
using UnityEngine;

public class Brick : Projectile
{
    Rigidbody2D _rb;
    GameObject _player;
    PlayerController _playerController;
    Vector3 _moveDir;
    WaitForSeconds _activeTime = new WaitForSeconds(3f);

    public CharacterInfo BrickInfo = new()
    {
        Atk = 2,
    };

    protected override CharacterInfo GetProjectileInfo()
    {
        base.GetProjectileInfo();
        return BrickInfo;
    }

    protected override void Initialize()
    {
        base.Initialize();
        _rb = GetComponent<Rigidbody2D>();
        _rb.bodyType = RigidbodyType2D.Dynamic;
        _rb.gravityScale = 1;
        _rb.linearVelocity = Vector3.zero;
        _player = GameObject.FindGameObjectWithTag(Define.PlayerTag);
        _playerController = _player.GetComponent<PlayerController>();
    }

    private void OnEnable()
    {
        float x = _player.transform.position.x;
        _moveDir = new Vector3(Random.Range(-0.2f, 0.2f), 1, 0);
        transform.position = _player.transform.position;
        FireBrick();
        StartCoroutine(ActiveTime());
    }

    IEnumerator ActiveTime()
    {
        yield return _activeTime;
        gameObject.SetActive(false);
    }

    void FireBrick()
    {
        _rb.AddForce(_moveDir * Random.Range(5.5f, 7f), ForceMode2D.Impulse);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Enemy enemy = collision.GetComponent<Enemy>();
        IDamageable damageable = collision.GetComponent<Enemy>();

        if (damageable != null && collision.CompareTag(Define.EnemyTag)
            ||collision.CompareTag(Define.BossTag))
        {
            damageable.AnyDamage(BrickInfo.Atk+_playerController.playerInfo.Atk,
                _player, (int)Define.EProjectile.Brick);
        }
    }
}
