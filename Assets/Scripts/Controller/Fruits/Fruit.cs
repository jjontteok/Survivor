using UnityEngine;

public class Fruit : MonoBehaviour
{
    [SerializeField]
    private float _rotateSpeed;
    private int _fruitLv;

    public int FruitLv
    {
        get { return _fruitLv; }
        set { _fruitLv = value; }
    }

    private void Update()
    {
        transform.Rotate(0, 0, _rotateSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(Define.PlayerTag))
        {
            gameObject.SetActive(false);
            UI_Play.Instance.FruitsCount += (_fruitLv + 1) * 4;
        }
    }
}
