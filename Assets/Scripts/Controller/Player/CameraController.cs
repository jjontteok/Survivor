using System.Runtime.Serialization;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Transform _target;
    private Vector3 _offset = new Vector3(0, 0, -10);
    private Vector3 _movePos;
    private float _speed = 10;
    void Start()
    {
        if (GameManager.Instance.Player == null)
        {
            GameObject player = GameObject.FindGameObjectWithTag(Define.PlayerTag);
            GameManager.Instance.Player = player.GetComponent<PlayerController>();
        }
        _target = GameManager.Instance.Player.transform;
    }

    //플레이어를 물리적으로 이동시킬 것이기 때문에 FixedUpdate로
    private void FixedUpdate()
    {
        _movePos = _target.position + _offset;
        transform.position = Vector3.Lerp(transform.position, _movePos, _speed * Time.deltaTime);
    }
}

