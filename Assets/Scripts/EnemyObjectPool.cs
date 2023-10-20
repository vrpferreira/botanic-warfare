using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemyObjectPool : MonoBehaviour
{
    public static EnemyObjectPool m_Instance;
    [SerializeField] private Player m_Player;
    [SerializeField] private SerializableEnemyDictionary m_EnemyDictionary = new SerializableEnemyDictionary();
    private Dictionary<Type, List<Enemy>> m_PooledEnemiesDictionary;
    private Dictionary<Type, float> m_PooledEnemiesSpawnTimeDictionary;
    private Dictionary<Type, float> m_PooledEnemiesLastSpawnTimeDictionary;
    private List<Enemy> m_EnemiesPrefabs;

    private void Awake()
    {
        if (m_Instance == null)
        {
            m_Instance = this;
        }
    }

    private void Start()
    {
        m_PooledEnemiesDictionary = new Dictionary<Type, List<Enemy>>();
        m_PooledEnemiesSpawnTimeDictionary = new Dictionary<Type, float>();
        m_PooledEnemiesLastSpawnTimeDictionary = new Dictionary<Type, float>();

        m_EnemiesPrefabs = m_EnemyDictionary.GetKeys();

        foreach (Enemy enemy in m_EnemiesPrefabs)
        {
            List<Enemy> pooledObjects = new List<Enemy>();

            for (int i = 0; i < m_EnemyDictionary.GetAmount(enemy); i++)
            {
                Enemy obj = Instantiate(enemy);
                obj.gameObject.SetActive(false);
                obj.transform.parent = this.transform;
                pooledObjects.Add(obj);
            }

            m_PooledEnemiesDictionary.Add(enemy.GetType(), pooledObjects);
            m_PooledEnemiesSpawnTimeDictionary.Add(enemy.GetType(), m_EnemyDictionary.GetSpawnTime(enemy));
            m_PooledEnemiesLastSpawnTimeDictionary[enemy.GetType()] = 0;
        }

    }

    private void Update()
    {
        this.GenerateEnemies();
    }

    public Enemy GetPooledObject(Type enemyType)
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

    private void GenerateEnemies()
    {
        foreach (Type typeOfEnemy in m_PooledEnemiesDictionary.Keys)
        {
            float spawnTime = m_PooledEnemiesSpawnTimeDictionary[typeOfEnemy];
            float lastSpawnTime = m_PooledEnemiesLastSpawnTimeDictionary[typeOfEnemy];

            if (Time.time - lastSpawnTime >= spawnTime)
            {
                lastSpawnTime = Time.time;
                m_PooledEnemiesLastSpawnTimeDictionary[typeOfEnemy] = lastSpawnTime;

                Enemy enemy = this.GetPooledObject(typeOfEnemy);

                if (enemy != null)
                {
                    enemy.transform.position = new Vector3(0, 0, 0);
                    enemy.SetPlayer(m_Player);
                    enemy.gameObject.SetActive(true);
                }
            }
        }
    }
}
