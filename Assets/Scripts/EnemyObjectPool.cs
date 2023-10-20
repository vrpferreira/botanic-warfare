using System.Collections.Generic;
using UnityEngine;

public class EnemyObjectPool : MonoBehaviour
{
    public Player m_Player;
    public SerializableEnemyDictionary m_EnemyDictionary = new SerializableEnemyDictionary();
    public static EnemyObjectPool m_Instance;
    public Dictionary<Enemy, List<Enemy>> m_PooledObjectsDictionary;
    public List<Enemy> m_EnemiesPrefabs;

    private void Awake()
    {
        if (m_Instance == null)
        {
            m_Instance = this;
        }
    }

    private void Start()
    {
        m_PooledObjectsDictionary = new Dictionary<Enemy, List<Enemy>>();

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

            m_PooledObjectsDictionary.Add(enemy, pooledObjects);
        }

        //test
        for (int j = 0; j < 10; j++)
        {
            Enemy enemy1 = this.GetPooledObject(m_EnemiesPrefabs[0]);

            if (enemy1 != null)
            {
                enemy1.transform.position = new Vector3(j * 5, 0, 0);
                enemy1.SetPlayer(m_Player);
                enemy1.gameObject.SetActive(true);
            }
        }
    }

    private void Update()
    {

    }

    public Enemy GetPooledObject(Enemy enemy)
    {
        if (m_PooledObjectsDictionary.ContainsKey(enemy))
        {
            List<Enemy> pooledObjects = m_PooledObjectsDictionary[enemy];
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
}
