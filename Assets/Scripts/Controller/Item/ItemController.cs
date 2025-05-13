using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour
{
    ItemBox _itemBox;
    public GameObject[] itemPrefabs;
    public List<GameObject> itemList = new();
    GameObject _itemPool;
    GameObject _itemController;

    private void OnEnable()
    {
        _itemBox = GetComponent<ItemBox>();
        _itemBox.OnItemBoxCrashed += OnItemBoxCrashed;
    }

    private void OnDisable()
    {
        _itemBox.OnItemBoxCrashed -= OnItemBoxCrashed;
    }
    private void Start()
    {
        _itemController = GameObject.Find("ItemController");
        if (_itemController == null)
            return;
        _itemPool = GameObject.Find("ItemPool");
        if (_itemPool == null)
        {
            _itemPool = new GameObject("ItemPool");
            _itemPool.transform.parent = _itemController.transform;
        }
    }

    void OnItemBoxCrashed(GameObject itemBox)
    {
        GameObject item = GetItem();
        item.transform.position = itemBox.transform.position;
    }

    GameObject GetItem()
    {
        int rnd = Random.Range(0, 3);
        foreach(GameObject item in itemList)
        {
            if (item.activeSelf)
                continue;

            if (rnd == 0 && item.CompareTag(Define.HealthItemTag))
            {
                item.SetActive(true);
                return item;
            }
            else if (rnd == 0 && item.CompareTag(Define.MagneticItemTag))
            {
                item.SetActive(true);
                return item;
            }
            else if (rnd == 2 && item.CompareTag(Define.CoinItemTag))
            {
                item.SetActive(true);
                return item;
            }
        }
        GameObject newItem = Instantiate(itemPrefabs[rnd]);
        newItem.transform.parent = _itemPool.transform;
        itemList.Add(newItem);
        newItem.SetActive(true);
        return newItem;
    }
}
