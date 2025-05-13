using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

//Projectile들을 스폰시키는 클래스
public class ProjectileController : MonoBehaviour
{
    float[] _projectileTime = Enumerable.Repeat<float>(0, (int)Define.EProjectile.Max).ToArray<float>();
    float[] _projectileIntervalTime = new float[(int)Define.EProjectile.Max];

    public bool[] IsProjectileActive = new bool[(int)Define.EProjectile.Max];
    public GameObject[] projectilePrefabs;
    public Dictionary<System.Type, List<Projectile>> projectileGroup = new();
    private GameObject _projectilePool;

    private void Start()
    {
        if (_projectilePool == null)
        {
            _projectilePool = new GameObject("ProjectilePool");
            _projectilePool.transform.parent = transform;
        }
        //스파이크공 쿨타임 : 1초
        //축구공 쿨타임 : 2초
        for (int i = 0; i < _projectileIntervalTime.Length; i++)
        {
            _projectileIntervalTime[i] = 1f + i * 2f;
        }

        IsProjectileActive[0] = true;
        for (int i = 1; i < IsProjectileActive.Length; i++)
            IsProjectileActive[i] = false;
    }
    void Update()
    {
        if (GameManager.Instance.isGameOver)
        {
            ClearProjectile();
            return;
        }
        SpawnProjectileByTime();
    }

    void SpawnProjectileByTime()
    {

        for (int i = 0; i < _projectileTime.Length; i++)
        {
            if (!IsProjectileActive[i])
                continue;
            _projectileTime[i] += Time.deltaTime;
            if (_projectileTime[i] > _projectileIntervalTime[i])
            {
                _projectileTime[i] -= _projectileIntervalTime[i];
                switch (i)
                {
                    case 0:
                        SpawnProjectile<SpikedBall>(i); break;
                    case 1:
                        for(int j=0; j < Skills.Instance.SkillList[1].SkillLevel-1; j++) 
                            SpawnProjectile<SoccerBall>(i); break;
                    case 2:
                        for(int j = 0; j < Skills.Instance.SkillList[2].SkillLevel-1;j++)
                            SpawnProjectile<Brick>(i); break;
                    case 3:
                        for(int j=0; j < Skills.Instance.SkillList[3].SkillLevel-1; j++)
                            SpawnProjectile<Boomerang>(i); break;
                    case 4:
                        for(int j=0; j < Skills.Instance.SkillList[4].SkillLevel-1;j++)
                            SpawnProjectile<Rocket>(i); break;
                    case 5:
                        SpawnProjectile<Shield>(i); break;
                }
            }
        }
    }

    void SpawnProjectile<T>(int projectileType) where T : Projectile
    {
        GameManager.Instance.Target = GameManager.Instance.GetNearestTarget();

        if (GameManager.Instance.Target != null)
        {
            var projectile = GetProjectile<T>(projectileType);
            if(projectile.GetType()!=typeof(SoccerBall))
                projectile.transform.position = GameManager.Instance.Player.Center;
        }
    }

    //projectileType에 따른 Projectile을 받아오는 함수
    private T GetProjectile<T>(int projectileType) where T : Projectile
    {
        Type type = typeof(T);
        if (projectileGroup.ContainsKey(type))
        {
            for(int i=0; i < projectileGroup[type].Count; i++)
            {
                if (!projectileGroup[type][i].gameObject.activeSelf)
                {
                    projectileGroup[type][i].gameObject.SetActive(true);
                    return projectileGroup[type][i] as T;
                }
            }
            GameObject newProjectile = Instantiate(projectilePrefabs[projectileType]);
            newProjectile.transform.parent = _projectilePool.transform;
            T controller = newProjectile.GetComponent<T>();
            projectileGroup[type].Add(controller);
            return controller as T;
        }
        else
        {
            GameObject newProjectile = Instantiate(projectilePrefabs[projectileType]);
            newProjectile.transform.parent = _projectilePool.transform;
            T controller = newProjectile.GetComponent<T>();
            projectileGroup[type] = new List<Projectile>();
            projectileGroup[type].Add(controller);
            return controller as T;
        }
    }

    void ClearProjectile()
    {
        projectileGroup.Clear();
        Destroy(_projectilePool);
    }
}

