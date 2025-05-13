using System.Collections;
using UnityEngine;

public class SoccerBall : Projectile
{
    CircleCollider2D _coll;
    Rigidbody2D _rb;
    GameObject _player;
    PlayerController _playerController;
    Vector3 _moveDir;
    WaitForSeconds _activeTime = new WaitForSeconds(2f);

    public CharacterInfo SoccerBallInfo = new()
    {
        Atk = 2,
        CurrentHp=5,
        MaxHp=5,
    };

    protected override CharacterInfo GetProjectileInfo()
    {
        base.GetProjectileInfo();
        return SoccerBallInfo;
    }

    protected override void Initialize()
    {
        base.Initialize();
        _coll = GetComponent<CircleCollider2D>();
        _rb = GetComponent<Rigidbody2D>();
        _rb.bodyType = RigidbodyType2D.Dynamic;
        _rb.gravityScale = 0;
        _rb.freezeRotation = true;
        _rb.linearVelocity = Vector3.zero;
    }

    private void OnEnable()
    {
        _player = GameObject.FindGameObjectWithTag(Define.PlayerTag);
        _playerController = _player.GetComponent<PlayerController>();
        float[] posX = { -1, 0, 1 };
        float[] posY = { -1f, 0, 1f };

        transform.position = new Vector3(posX[Random.Range(0,3)] +_player.transform.position.x,
            posY[Random.Range(0,3)] +_player.transform.position.y, 0);
        _moveDir = (GameManager.Instance.Target.transform.position -
            transform.position).normalized;
        SoccerBallInfo.CurrentHp = SoccerBallInfo.MaxHp;
        FireSoccerBall();
        StartCoroutine(ActiveTime());
        Debug.Log($"Player Position {_player.transform.position}");
        Debug.Log($"SoccerBall Position {transform.position}");
    }

    IEnumerator ActiveTime()
    {
        yield return _activeTime;
        gameObject.SetActive(false);
    }

    void FireSoccerBall()
    {
        _rb.AddForce(_moveDir * Random.Range(9f, 11f));
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Enemy enemy = collision.collider.GetComponent<Enemy>();
        IDamageable damageable = collision.collider.GetComponent<IDamageable>();
        if (damageable != null && collision.collider.CompareTag(Define.EnemyTag)
            || collision.collider.CompareTag(Define.BossTag))
        {
            damageable.AnyDamage(SoccerBallInfo.Atk+ _playerController.playerInfo.Atk, _player);
            SoccerBallInfo.CurrentHp--;
            if (SoccerBallInfo.CurrentHp <= 0)
            {
                gameObject.SetActive(false);
            }
        }
    }
}
