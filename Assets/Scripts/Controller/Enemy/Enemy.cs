using System.Collections;
using System.Data.Common;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemy : MonoBehaviour, IDamageable
{
    SpriteRenderer _rend;
    CircleCollider2D _coll;
    Animator _anim;
    Rigidbody2D _rb;

    public CharacterInfo EnemyInfo;

    protected GameObject _player;
    PlayerController _playerController;
    bool _isDead;

    public bool IsDead { get { return _isDead; } set { _isDead = value; } }
    public string Tag { get; set; } = Define.EnemyTag;
    public virtual CharacterInfo GetEnemyInfo()
    {
        return EnemyInfo;
    }

    private void Awake()
    {
        Initialize();
    }
    protected virtual void Initialize()
    {
        _rend = GetComponent<SpriteRenderer>();
        _coll = GetComponent<CircleCollider2D>();
        _anim = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody2D>();
        _rb.bodyType = RigidbodyType2D.Dynamic;
        _rb.gravityScale = 0;
        _rb.freezeRotation = true;
        _rb.linearVelocity = Vector3.zero;
        EnemyInfo = GetEnemyInfo();
    }

    private void OnEnable()
    {
        _isDead = false;
        EnemyInfo.CurrentHp = EnemyInfo.MaxHp;
    }

    protected virtual void Start()
    {
        _player = GameObject.FindWithTag(Define.PlayerTag);
        _playerController = _player.GetComponent<PlayerController>();
    }

    protected virtual void FixedUpdate()
    {
        EnemyMove();
    }


    void EnemyMove()
    {
        if (!_isDead)
        {
            Vector3 distance = (_player.transform.position - transform.position).normalized;
            _rend.flipX = distance.x > 0;
            Debug.Log($"Blue Bird Speed {EnemyInfo.Speed}");
            _rb.MovePosition(transform.position + distance * EnemyInfo.Speed * Time.deltaTime);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag(Define.PlayerTag) && !_isDead)
        {
            _playerController.playerInfo.CurrentHp -= EnemyInfo.Atk;
            if (_playerController.playerInfo.CurrentHp <= 0)
            {
                GameManager.Instance.isGameOver = true;
                if (gameObject.tag == Define.BossTag)
                    GameManager.Instance.isBossSpawn = false;
            }
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.CompareTag(Define.PlayerTag) && !_isDead)
        {
            _playerController.playerInfo.CurrentHp -= EnemyInfo.Atk;
            if (_playerController.playerInfo.CurrentHp <= 0)
            {
                GameManager.Instance.isGameOver = true;
                if (gameObject.tag == Define.BossTag)
                    GameManager.Instance.isBossSpawn = false;
            }
        }
    }

    public bool AnyDamage(float damage, GameObject damageCauser, 
        int projectile = (int)Define.EProjectile.SpikedBall)
    {
        
        EnemyInfo.CurrentHp -= damage;
        if (EnemyInfo.CurrentHp <= 0)
        {
            gameObject.SetActive(false);
            if (gameObject.tag == Define.BossTag)
            {
                GameManager.Instance.isBossSpawn = false;
                GameManager.Instance.isGameOver = true;
            }
            UI_Play.Instance.DeadEnemyCount++;
            SoundManager.Instance.EnemyDeadSound.Play();
        }
        if (gameObject.tag == Define.BossTag)
        {
             UI_Play.Instance.BossHpBar.fillAmount = EnemyInfo.CurrentHp / EnemyInfo.MaxHp;
        }
        if(projectile==(int)Define.EProjectile.SpikedBall && gameObject.tag!=Define.BossTag)
            OnKnockBack(damageCauser);
        return true;
    }

    void OnKnockBack(GameObject causer)
    {
        Vector2 dir = (transform.position - causer.transform.position).normalized;
        _rb.AddForce(dir * 1000, ForceMode2D.Force);
    }
}
