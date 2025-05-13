using UnityEngine;

public class RocketExplosionEffect : MonoBehaviour
{
    GameObject _player;
    PlayerController _playerController;
    Rocket _rocket;
    float _atk;
    private void Start()
    {
        SoundManager.Instance.RocketExplosionSound.Play();
        _player = GameObject.FindGameObjectWithTag(Define.PlayerTag);
        _playerController = _player.GetComponent<PlayerController>();
        _rocket =
            GameObject.FindGameObjectWithTag(Define.RocketTag).GetComponent<Rocket>();
        _atk = _rocket.RocketInfo.Atk;
    }

    // Animation Event
    void RocketExplosionEffectEndEvent()
    {
        Destroy(gameObject);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        Enemy enemy = collision.GetComponent<Enemy>();
 
        IDamageable damageable = collision.GetComponent<IDamageable>();
        if (damageable!=null && collision.CompareTag(Define.EnemyTag) ||
            collision.CompareTag(Define.BossTag))
        {
            damageable.AnyDamage(_atk + _playerController.playerInfo.Atk,
                _player, (int)Define.EProjectile.Rocket);
        }
    }
}
