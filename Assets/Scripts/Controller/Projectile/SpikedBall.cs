using UnityEngine;

public class SpikedBall : Projectile
{
    Rigidbody2D _rb;
    GameObject _player;
    PlayerController _playerController;
    Vector3 _moveDir;

    public CharacterInfo SpikedBallInfo = new()
    {
        Atk = 1,
        Speed = 3
    };

    protected override CharacterInfo GetProjectileInfo()
    {
        base.GetProjectileInfo();
        return SpikedBallInfo;
    }

    protected override void Initialize()
    {
        base.Initialize();
        _rb = GetComponent<Rigidbody2D>();
        _rb.bodyType = RigidbodyType2D.Kinematic;
        _rb.gravityScale = 0;
        _rb.freezeRotation = true;
        _rb.linearVelocity = Vector3.zero;
        _player = GameObject.FindGameObjectWithTag(Define.PlayerTag);
        _playerController = _player.GetComponent<PlayerController>();
    }

    private void OnEnable()
    {
        transform.position = _player.transform.position;
        _moveDir = (GameManager.Instance.Target.transform.position - 
            transform.position).normalized;
        SoundManager.Instance.SpikedBallSound.Play();
    }
    private void FixedUpdate()
    {
        FireSpikedBall();
        //Debug.Log($"spikedBall Atk {SpikedBallInfo.Atk}");
    }

    void FireSpikedBall()
    {
        transform.Translate(_moveDir * SpikedBallInfo.Speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Enemy enemy = collision.GetComponent<Enemy>();
        IDamageable damageable = collision.GetComponent<IDamageable>();
        if (damageable != null && collision.CompareTag(Define.EnemyTag) ||
            collision.CompareTag(Define.BossTag))
        {
            damageable.AnyDamage(SpikedBallInfo.Atk+_playerController.playerInfo.Atk, _player);
            gameObject.SetActive(false);
        }
    }
}
