using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SerializableEnemyDictionary
{
    [SerializeField]
    private List<Enemy> m_EnemiesPrefabs;
    [SerializeField]
    private List<int> m_Amount;
    [SerializeField]
    private List<float> m_SpawnTime;

    public void Add(Enemy key, int amount, float spawnSpeed)
    {
        m_EnemiesPrefabs.Add(key);
        m_Amount.Add(amount);
        m_SpawnTime.Add(spawnSpeed);
    }

    public int GetAmount(Enemy key)
    {
        int index = m_EnemiesPrefabs.IndexOf(key);

        if (index != -1)
        {
            return m_Amount[index];
        }

        return -1;
    }

    public float GetSpawnTime(Enemy key)
    {
        int index = m_EnemiesPrefabs.IndexOf(key);

        if (index != -1)
        {
            return m_SpawnTime[index];
        }

        return -1;
    }

    public List<Enemy> GetKeys()
    {
        return m_EnemiesPrefabs;
    }
}