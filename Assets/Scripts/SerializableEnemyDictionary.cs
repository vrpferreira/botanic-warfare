using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SerializableEnemyDictionary
{
    [SerializeField]
    private List<Enemy> m_Keys;
    [SerializeField]
    private List<int> m_Amount;
    [SerializeField]
    private List<float> m_SpawnSpeed;

    public void Add(Enemy key, int amount, float spawnSpeed)
    {
        m_Keys.Add(key);
        m_Amount.Add(amount);
        m_SpawnSpeed.Add(spawnSpeed);
    }

    public int GetAmount(Enemy key)
    {
        int index = m_Keys.IndexOf(key);

        if (index != -1)
        {
            return m_Amount[index];
        }

        return -1;
    }

    public float GetSpawnSpeed(Enemy key)
    {
        int index = m_Keys.IndexOf(key);

        if (index != -1)
        {
            return m_SpawnSpeed[index];
        }

        return -1;
    }

    public List<Enemy> GetKeys()
    {
        return m_Keys;
    }
}