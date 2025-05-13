using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class MagnetSkill : MonoBehaviour
{
    PlayerController _playerController;

    private void Start()
    {
        _playerController = GameObject.FindGameObjectWithTag(Define.PlayerTag).
            GetComponent<PlayerController>();
    }
    private void Update()
    {
        Collider2D[] fruits = Physics2D.OverlapCircleAll(
            transform.position,
            _playerController.MagnetRange,
            LayerMask.GetMask("Fruit"));
        foreach(Collider2D fruit in fruits)
        {
            MoveTowardPlayer(fruit.transform);
        }
    }

    void MoveTowardPlayer(Transform fruit)
    {
        Vector3 dir = (transform.position - fruit.transform.position).normalized;
        fruit.position = Vector3.MoveTowards(fruit.position,
            transform.position,
            3 * Time.deltaTime);
    }
}
