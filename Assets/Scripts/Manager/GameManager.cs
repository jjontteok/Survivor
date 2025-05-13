using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager:MonoBehaviour
{
    #region JoyStick
    public event Action<Vector2> OnMoveDirChanged;

    
    Vector2 _moveDir;
    
    public Vector2 MoveDir
    {
        get { return _moveDir; }
        set
        {
            _moveDir = value;
            OnMoveDirChanged?.Invoke(value);
        }
    }
    #endregion

    public static GameManager Instance;

    List<Type> types = new()
    {
        typeof(EnemyBee),
        typeof(EnemyBlueBird),
        typeof(EnemyBunny),
        typeof(EnemyChicken),
    };

    public PlayerController Player;
    public EnemyController EnemyController;
    public GameObject Target { get; set; }
    public bool isGameOver = false;
    public bool isBossSpawn = false;
    GameObject _boss;
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
        if (Player == null)
        {
            GameObject playerGameObject = GameObject.FindGameObjectWithTag(Define.PlayerTag);
            Player = playerGameObject.GetComponent<PlayerController>();
        }
        if (EnemyController == null)
        {
            EnemyController = GameObject.Find("EnemyController").GetComponent<EnemyController>();
        }
        Debug.Log(Player);
    }

    public GameObject GetNearestTarget(float distance=12)
    {
        if (isBossSpawn)
        {
            _boss = GameObject.FindWithTag(Define.BossTag);
            return _boss;
        }
        EnemyController = FindAnyObjectByType<EnemyController>();
        List<Enemy> targetList = new();
        int enemyGroupKey = EnemyController.enemyGroup.Count;
        if (enemyGroupKey <= 0)
        {
            return null;
        }

        for(int i=0; i < 4; i++)
        {
            if (!EnemyController.enemyGroup.ContainsKey(types[i]))
                continue;
            
            targetList.AddRange(EnemyController.enemyGroup[types[i]].
                Where(enemy => enemy.gameObject.activeSelf));
        }
        if (Player == null)
        {
            return null;
        }
        var target = targetList.OrderBy(enemy => 
            (Player.Center- enemy.transform.position).sqrMagnitude).FirstOrDefault();

        if (target == null||(target.transform.position - Player.Center).sqrMagnitude > distance)
        {
            return null;
        }
        return target.gameObject;
    }
}