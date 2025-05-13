using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    SpriteRenderer _rend;
    Animator _anim;
    Collider2D _coll;
    Rigidbody2D _rb;
    private Vector2 _moveDir;
    private float _magnetRange;

    public CharacterInfo playerInfo = new CharacterInfo()
    {
        //HP:500
        Atk = 0,
        CurrentHp = 500,
        MaxHp = 500,
        Speed = 1.5f
    };

    public Vector3 Center
    {
        get => _coll.bounds.center;
    }

    public Vector2 MoveDir
    {
        get => _moveDir; set => _moveDir = value.normalized;
    }

    public float MagnetRange
    {
        get => _magnetRange; set => _magnetRange = value;
    }

    void Start()
    {
        _rend = GetComponent<SpriteRenderer>();
        _anim = GetComponent<Animator>();
        _coll = GetComponent<Collider2D>();
        _rb = GetComponent<Rigidbody2D>();
        _rb.gravityScale = 0;
        _rb.freezeRotation = true;
        _rb.linearVelocity = Vector3.zero;
        _rb.mass = 2000;

        _magnetRange = 0.2f;
        GameManager.Instance.OnMoveDirChanged += dir => _moveDir = dir;
        Camera.main.AddComponent<CameraController>();
    }

    void FixedUpdate()
    {
        PlayerMove();
    }

    //플레이어가 움직이는 함수
    void PlayerMove()
    {
        _rb.linearVelocity = Vector2.zero;
        Vector3 moveDir = transform.position + (Vector3)_moveDir * playerInfo.Speed * Time.deltaTime;
        _rb.MovePosition(moveDir);
        _rend.flipX = _moveDir.x < 0;
        _anim.SetBool(Define.isMoveHash, _moveDir != Vector2.zero);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag(Define.BossBarrierTag))
        {
            playerInfo.CurrentHp -= 5;
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.CompareTag(Define.BossBarrierTag))
        {
            playerInfo.CurrentHp -= 1f;
        }
    }
}
