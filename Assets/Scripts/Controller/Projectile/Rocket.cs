using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class Rocket : Projectile
{
    Rigidbody2D _rb;
    GameObject _player;
    public GameObject RocketExplosionEffect;
    private WaitForSeconds _activeTime = new WaitForSeconds(3f);
    private WaitForSeconds _destroyTime = new WaitForSeconds(0.2f);
    private bool _isCollide;

    public WaitForSeconds ActiveTime
    {
        get { return _activeTime; }
        set { _activeTime = value; }
    }

    Vector3 _moveDir;

    public CharacterInfo RocketInfo = new()
    {
        Atk = 1,
        Speed = 2,
    };
 
    protected override CharacterInfo GetProjectileInfo()
    {
        base.GetProjectileInfo();
        return RocketInfo;
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
    }
    private void OnEnable()
    {
        transform.position = _player.transform.position;
        float angle = Random.Range(0, 360);
        _moveDir = new Vector3(Mathf.Cos(angle*Mathf.Deg2Rad),
            Mathf.Sin(angle * Mathf.Deg2Rad)).normalized;

        transform.rotation = Quaternion.Euler(0, 0, angle-90);
        _isCollide = false;

        StartCoroutine(ActiveTimer(_activeTime));
    }

    IEnumerator ActiveTimer(WaitForSeconds seconds)
    {
        yield return seconds;
        gameObject.SetActive(false);
    }


    private void FixedUpdate()
    { 
        if(!_isCollide)
            FireRocket();
    }

    void FireRocket()
    {
        transform.Translate(_moveDir * RocketInfo.Speed * Time.deltaTime,Space.World);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(Define.EnemyTag) 
            || collision.CompareTag(Define.BossTag))
        {
            Instantiate(RocketExplosionEffect, transform.position, Quaternion.identity);
            _isCollide = true;
            StartCoroutine(ActiveTimer(_destroyTime));
        }
    }
}
