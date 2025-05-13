using UnityEngine;

public class HealthItem : MonoBehaviour
{
    GameObject _player;
    PlayerController _playerController;

    private void Start()
    {
        _player = GameObject.FindWithTag(Define.PlayerTag);
        _playerController = _player.GetComponent<PlayerController>();

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(Define.PlayerTag))
        {
            SoundManager.Instance.HealthItemGainSound.Play();
            _playerController.playerInfo.CurrentHp += 100;
            if (_playerController.playerInfo.CurrentHp > _playerController.playerInfo.MaxHp)
            {
                _playerController.playerInfo.CurrentHp = _playerController.playerInfo.MaxHp;
            }
            gameObject.SetActive(false);
        }
    }
}
