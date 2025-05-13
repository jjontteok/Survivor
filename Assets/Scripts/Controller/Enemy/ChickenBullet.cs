using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChickenBullet : MonoBehaviour
{
    GameObject _player;
    PlayerController _playerController;
    Vector3 _moveDir;
    [SerializeField]
    float _speed;
    private void Awake()
    {
        transform.parent = GameObject.Find("EnemyBullet").transform;
        _player = GameObject.FindWithTag(Define.PlayerTag);
        _playerController = _player.GetComponent<PlayerController>();
        _speed = 2f;
    }
    private void OnEnable()
    {
        _moveDir=(_player.transform.position-transform.position).normalized;
        Destroy(gameObject, 3f);
    }

    private void FixedUpdate()
    {
        transform.Translate(_moveDir * _speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(Define.PlayerTag))
        {
            Destroy(gameObject);
            _playerController.playerInfo.CurrentHp -= 10;
            if (_playerController.playerInfo.CurrentHp <= 0)
                SceneManager.LoadScene("GameOver");
        }
    }
}
