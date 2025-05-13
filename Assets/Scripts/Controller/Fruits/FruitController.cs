using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FruitController : MonoBehaviour
{
    float[] _fruitTime = 
        Enumerable.Repeat<float>(0, (int)Define.EFruit.Max).ToArray<float>();
    float[] _fruitIntervalTime = new float[(int)Define.EFruit.Max];

    public GameObject[] fruitPrefabs;
    public Dictionary<int, List<GameObject>> fruitGroup = new();
    public bool[] IsFruitSpawn = new bool[] { false, };

    GameObject _fruitPool;
    GameObject _player;

    private void Start()
    {
        if (_fruitPool == null)
        {
            _fruitPool = new GameObject("FruitPool");
            _fruitPool.transform.parent = transform;
        }
        for (int i = 0; i < _fruitIntervalTime.Length; i++)
        {
            _fruitIntervalTime[i] = 1f + i * 4f;
        }
        _player = GameObject.FindWithTag(Define.PlayerTag);
    }

    private void Update()
    {
        if (GameManager.Instance.isBossSpawn)
            return;
        SpawnFruitByTime();
        SetSpawnFruitByTime();
    }
    void SetSpawnFruitByTime()
    {
        if (Timer.Instance.CurrentTime > 0)
            IsFruitSpawn[0] = true;
        if (Timer.Instance.CurrentTime > 60)
            IsFruitSpawn[1] = true;
        if (Timer.Instance.CurrentTime > 120)
            IsFruitSpawn[2] = true;
    }

    void SpawnFruitByTime()
    {
        for(int i=0; i < fruitPrefabs.Length; i++)
        {
            if (!IsFruitSpawn[i])
                continue;
            _fruitTime[i] += Time.deltaTime;
            if (_fruitTime[i] > _fruitIntervalTime[i])
            {
                _fruitTime[i] -= _fruitTime[i];
                SpawnFruit(i);
            }
        }
    }
    void SpawnFruit(int fruitType)
    {
        float boxWidth = 5f;
        float boxHeight = 2.5f;
        Vector2 playerPos = _player.transform.position;
        Vector3 rndPos = new Vector3(Random.Range(-boxWidth + playerPos.x, boxWidth + playerPos.x),
           Random.Range(-boxHeight + playerPos.y, boxHeight + playerPos.y), 0);

        GameObject fruit = GetFruit(fruitType);
        fruit.transform.position = rndPos;
        fruit.GetComponent<Fruit>().FruitLv = fruitType;
    }

    GameObject GetFruit(int fruitType)
    {
        if (fruitGroup.ContainsKey(fruitType))
        {
            for(int i=0; i < fruitGroup[fruitType].Count; i++)
            {
                if (!fruitGroup[fruitType][i].activeSelf)
                {
                    fruitGroup[fruitType][i].SetActive(true);
                    return fruitGroup[fruitType][i];
                }
            }
            GameObject newFruit = Instantiate(fruitPrefabs[fruitType]);
            newFruit.transform.parent = _fruitPool.transform;
            fruitGroup[fruitType].Add(newFruit.gameObject);
            return newFruit;
        }
        else
        {
            {
                GameObject newFruit = Instantiate(fruitPrefabs[fruitType]);
                newFruit.transform.parent = _fruitPool.transform;
                List<GameObject> newList = new();
                newList.Add(newFruit.gameObject);
                fruitGroup.Add(fruitType, newList);
                return newFruit;
            }
        }
    }
}
