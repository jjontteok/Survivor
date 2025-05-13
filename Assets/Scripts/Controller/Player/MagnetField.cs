using UnityEngine;

public class MagnetField : MonoBehaviour
{
    GameObject _player;
    public bool isMagnetActive;
    public float magnetSpeed = 2000f;

    float _time = 0;
    float _magnetTime = 1f;

    private void OnEnable()
    {
        isMagnetActive = true;
        _player = GameObject.FindGameObjectWithTag(Define.PlayerTag);
    }

    private void OnDisable()
    {
        isMagnetActive = false;
    }

    private void Update()
    {
        if (isMagnetActive)
        {
            MagnetFieldActive();
        }
    }

    void MagnetFieldActive()
    {
        _time += Time.deltaTime;
        if (_time < _magnetTime)
        {
            Collider2D[] fruits = Physics2D.OverlapCircleAll(
                _player.transform.position,
                20f,
                LayerMask.GetMask("Fruit"));
            foreach (Collider2D fruit in fruits)
            {
                MoveFruitsTowardPlayer(fruit.transform);
            }
        }
        else
        {
            isMagnetActive = false;
            Destroy(gameObject);
        }
    }

    private void MoveFruitsTowardPlayer(Transform fruit)
    {
        Vector3 dir = (_player.transform.position - fruit.position).normalized;
        fruit.position = Vector3.MoveTowards(fruit.position,
            _player.transform.position,
            magnetSpeed * Time.deltaTime);
    }
}
