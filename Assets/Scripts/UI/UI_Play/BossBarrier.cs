using UnityEngine;

public class BossBarrier : MonoBehaviour
{
    GameObject _player;
    private void OnEnable()
    {
        _player = GameObject.FindGameObjectWithTag(Define.PlayerTag);
        transform.position = _player.transform.position;
    }
    private void OnDisable()
    {
        Destroy(gameObject);
    }
}
