using System.Collections.Generic;
using UnityEngine;

public class ItemBoxController : MonoBehaviour
{
    public GameObject itemBoxPrefab;

    public List<GameObject> itemBoxList = new();
    GameObject _itemBoxPool;

    float _spawnTime = 10f;
    float _coolTime = 0f;

    GameObject player;


    void Start()
    {
        player = GameObject.FindWithTag("Player");
        if (_itemBoxPool == null)
        {
            _itemBoxPool = new GameObject("ItemBoxPool");
            _itemBoxPool.transform.parent = transform;
        }
    }

    void Update()
    {
        _coolTime += Time.deltaTime;
        if (_coolTime > _spawnTime)
        {
            _coolTime -= _coolTime;
            ItemBoxSpawn();
        }
    }

    void ItemBoxSpawn()
    {
        Vector2 playerPos = player.transform.position;
        GameObject itemBox = GetItemBox();
        itemBox.SetActive(true);
        itemBox.transform.position =
            new Vector3(Random.Range(-15f + playerPos.x, 15f + playerPos.x),
            Random.Range(-10f + playerPos.y, 10f + playerPos.y), 0);
    }

    GameObject GetItemBox()
    {
        foreach (GameObject item in itemBoxList)
        {
            if (!item.activeSelf)
            {
                return item;
            }
        }
        GameObject newItemBox = Instantiate(itemBoxPrefab);
        newItemBox.transform.parent = _itemBoxPool.transform;
        itemBoxList.Add(newItemBox);
        return newItemBox;
    }
}
