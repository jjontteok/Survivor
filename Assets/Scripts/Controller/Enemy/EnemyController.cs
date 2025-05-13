using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    float[] _enemyTime = 
        Enumerable.Repeat<float>(0, (int)Define.EEnemy.Max-1).ToArray<float>();
    float[] _enemyIntervalTime = new float[(int)Define.EEnemy.Max-1];
    public bool[] IsEnemySpawn = new bool[] { false, };

    public GameObject[] enemyPrefabs;
    public Dictionary<System.Type, List<Enemy>> enemyGroup = new();
    private GameObject _enemyPool;
    private GameObject _player;
    Vector2 _playerPos;

    WaitForSeconds seconds = new WaitForSeconds(2f);

    private void Start()
    {
        if (_enemyPool == null)
        {
            _enemyPool = new GameObject("EnemyPool");
            _enemyPool.transform.parent = transform;
        }
        for(int i=0; i < _enemyIntervalTime.Length; i++)
        {
            _enemyIntervalTime[i] = 2f + i * 2f;
        }
        _player = GameObject.FindWithTag(Define.PlayerTag);
    }

    private void Update()
    {
        SetSpawnEnemyByTime();
        SpawnEnemyByTime();
    }
    void SetSpawnEnemyByTime()
    {
        if (Timer.Instance.CurrentTime > 0)
            IsEnemySpawn[0] = true;
        if (Timer.Instance.CurrentTime > 60)
            IsEnemySpawn[1] = true;
        if (Timer.Instance.CurrentTime > 120)
            IsEnemySpawn[2] = true;
        if (Timer.Instance.CurrentTime > 180)
            IsEnemySpawn[3] = true;
        if (Timer.Instance.CurrentTime > 300 && !GameManager.Instance.isBossSpawn)
        {
           StartCoroutine(BossSpawn());
        }
    }
    void SpawnEnemyByTime()
    {
        if (GameManager.Instance.isBossSpawn) return;
        for(int i = 0; i < _enemyTime.Length; i++)
        {
            if (!IsEnemySpawn[i])
                continue;
            _enemyTime[i] += Time.deltaTime;
            if (_enemyTime[i] > _enemyIntervalTime[i])
            {
                _enemyTime[i] -= _enemyIntervalTime[i];
                switch (i)
                {
                    case 0:
                        SpawnEnemy<EnemyBee>(i); break;
                    case 1:
                        SpawnEnemy<EnemyBlueBird>(i); break;
                    case 2:
                        SpawnEnemy<EnemyBunny>(i); break;
                    case 3:
                        SpawnEnemy<EnemyChicken>(i); break;
                }
            }
        }
    }

    void SpawnEnemy<T>(int enemyType) where T : Enemy
    {
        float boxWidth = 10f; 
        float boxHeight = 5f;
        _playerPos = _player.transform.position;
        Vector3 rndPos = new Vector3(UnityEngine.Random.Range(-boxWidth + _playerPos.x, boxWidth + _playerPos.x),
            UnityEngine.Random.Range(-boxHeight + _playerPos.y, boxHeight + _playerPos.y), 0);

        var enemy = GetEnemy<T>(enemyType);
        enemy.transform.position = rndPos;
        Enemy enemyScript = enemy.GetComponent<Enemy>();
    }

    private T GetEnemy<T>(int enemyType) where T:Enemy
    {
        Type type = typeof(T);
        if (enemyGroup.ContainsKey(type))
        {
            for(int i=0; i < enemyGroup[type].Count; i++)
            {
                if (!enemyGroup[type][i].gameObject.activeSelf)
                {
                    enemyGroup[type][i].gameObject.SetActive(true);
                    return enemyGroup[type][i] as T;
                }
            }
            GameObject newEnemy = Instantiate(enemyPrefabs[enemyType]);
            newEnemy.transform.parent = _enemyPool.transform;
            T controller = newEnemy.GetComponent<T>();
            enemyGroup[type].Add(controller);
            return controller as T;
        }
        else
        {
            GameObject newEnemy = Instantiate(enemyPrefabs[enemyType]);
            newEnemy.transform.parent = _enemyPool.transform;
            T controller = newEnemy.GetComponent<T>();
            enemyGroup[type] = new List<Enemy>();
            enemyGroup[type].Add(controller);
            return controller as T;
        }
    }

    IEnumerator BossSpawn()
    {
        GameManager.Instance.isBossSpawn = true;
        enemyGroup.Clear();
        Destroy(_enemyPool);
        yield return seconds;
        Vector3 rndPos = new Vector3(UnityEngine.Random.Range(1 + _playerPos.x, 1 + _playerPos.x),
            UnityEngine.Random.Range(-1f + _playerPos.y, 1f + _playerPos.y), 0);
        Instantiate(enemyPrefabs[4], rndPos, Quaternion.identity);
    }
}
