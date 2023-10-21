using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class EnemyObjectPool : MonoBehaviour
{
    [SerializeField] private Tilemap m_Tilemap;
    [SerializeField] private Player m_Player;
    [SerializeField] private List<EnemySpawnData> m_EnemySpawnDataList = new List<EnemySpawnData>();
    private Dictionary<Type, List<Enemy>> m_PooledEnemiesDictionary;
    private Dictionary<Type, float> m_PooledEnemiesSpawnTimeDictionary;
    private Dictionary<Type, float> m_PooledEnemiesLastSpawnTimeDictionary;

    private void Start()
    {
        m_PooledEnemiesDictionary = new Dictionary<Type, List<Enemy>>();
        m_PooledEnemiesSpawnTimeDictionary = new Dictionary<Type, float>();
        m_PooledEnemiesLastSpawnTimeDictionary = new Dictionary<Type, float>();

        foreach (EnemySpawnData enemySpawnData in m_EnemySpawnDataList)
        {
            List<Enemy> pooledObjects = new List<Enemy>();

            for (int i = 0; i < enemySpawnData.GetAmount(); i++)
            {
                Enemy obj = Instantiate(enemySpawnData.GetEnemyPrefab());
                obj.gameObject.SetActive(false);
                obj.transform.parent = this.transform;
                pooledObjects.Add(obj);
            }

            Type typeOfEnemy = enemySpawnData.GetEnemyPrefab().GetType();
            m_PooledEnemiesDictionary[typeOfEnemy] = pooledObjects;
            m_PooledEnemiesSpawnTimeDictionary[typeOfEnemy] = enemySpawnData.GetSpawnTime();
            m_PooledEnemiesLastSpawnTimeDictionary[typeOfEnemy] = 0;
        }
    }

    private void Update()
    {
        this.HandleEnemySpawning();
    }

    private Enemy GetPooledObject(Type enemyType)
    {
        if (m_PooledEnemiesDictionary.ContainsKey(enemyType))
        {
            List<Enemy> pooledObjects = m_PooledEnemiesDictionary[enemyType];
            foreach (Enemy obj in pooledObjects)
            {
                if (!obj.gameObject.activeInHierarchy)
                {
                    return obj;
                }
            }
        }

        return null;
    }

    private void HandleEnemySpawning()
    {
        foreach (Type typeOfEnemy in m_PooledEnemiesDictionary.Keys)
        {
            float spawnTime = m_PooledEnemiesSpawnTimeDictionary[typeOfEnemy];
            float lastSpawnTime = m_PooledEnemiesLastSpawnTimeDictionary[typeOfEnemy];

            if (Time.time - lastSpawnTime >= spawnTime)
            {
                m_PooledEnemiesLastSpawnTimeDictionary[typeOfEnemy] = Time.time;

                Enemy enemy = this.GetPooledObject(typeOfEnemy);

                if (enemy != null)
                {
                    BoundsInt bounds = m_Tilemap.cellBounds;
                    Vector3Int tilePosition;
                    tilePosition = new Vector3Int(UnityEngine.Random.Range(bounds.x, bounds.x + bounds.size.x), UnityEngine.Random.Range(bounds.y, bounds.y + bounds.size.y), 0);
                    Vector3 spawnPosition = m_Tilemap.GetCellCenterWorld(tilePosition);
                    enemy.transform.position = spawnPosition;
                    enemy.SetPlayer(m_Player);
                    enemy.gameObject.SetActive(true);
                }
            }
        }
    }
}
