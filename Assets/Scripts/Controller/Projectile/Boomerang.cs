using System.Collections;
using UnityEngine;

public class Boomerang : Projectile
{
    Rigidbody2D _rb;
    GameObject _player;
    PlayerController _playerController;
    Vector3 _moveDir;
    float _time = 0;
    float _changeDirTime = 0.7f;
    float _playerAtk;
    bool _isDirChange;
    WaitForSeconds _activeTime = new WaitForSeconds(3f);

    public CharacterInfo BoomerangInfo = new()
    {
        Atk = 1,
        Speed = 5,
    };

    protected override CharacterInfo GetProjectileInfo()
    {
        base.GetProjectileInfo();
        return BoomerangInfo;
    }

    protected override void Initialize()
    {
        base.Initialize();
        _rb = GetComponent<Rigidbody2D>();
        _rb.bodyType = RigidbodyType2D.Dynamic;
        _rb.gravityScale = 0;
        _rb.freezeRotation = true;
        _rb.linearVelocity = Vector3.zero;
        _player = GameObject.FindGameObjectWithTag(Define.PlayerTag);
        _playerController = _player.GetComponent<PlayerController>();
    }

    private void OnEnable()
    {
        SoundManager.Instance.BoomerangSound.Play();
        _time = 0;
        transform.position = _player.transform.position;
        float angle = Random.Range(0, 360);
        _moveDir = new Vector3(Mathf.Cos(angle * Mathf.Deg2Rad),
            Mathf.Sin(angle * Mathf.Deg2Rad), 0).normalized;
        _isDirChange = false;
        StartCoroutine(ActiveTimer());
    }

    private void FixedUpdate()
    {
        FireBoomerang();
    }

    IEnumerator ActiveTimer()
    {
        yield return _activeTime;
        gameObject.SetActive(false);
    }

    void FireBoomerang()
    {
        if (_time > _changeDirTime && !_isDirChange)
        {
            _moveDir = -_moveDir;
            _isDirChange = true;
        }
        _rb.AddForce(_moveDir * BoomerangInfo.Speed, ForceMode2D.Force);
        _time += Time.deltaTime;
        transform.Rotate(0, 0, 200 * Time.deltaTime, Space.World);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Enemy enemy = collision.GetComponent<Enemy>();
        IDamageable damageable = collision.GetComponent<IDamageable>();
        if (damageable != null && collision.CompareTag(Define.EnemyTag)
            || collision.CompareTag(Define.BossTag))
        {
            damageable.AnyDamage(BoomerangInfo.Atk+_playerController.playerInfo.Atk, _player, (int)Define.EProjectile.Boomerang);
        }
    }
}
