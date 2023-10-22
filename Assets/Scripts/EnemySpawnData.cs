using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class EnemySpawnData
{
    [SerializeField] private Enemy m_EnemyPrefab;
    [SerializeField] private int m_Amount;
    [SerializeField] private float m_SpawnTime;
    [SerializeField] private int m_MaxHealth;
    [SerializeField] private float m_Speed;

    public Enemy GetEnemyPrefab()
    {
        return m_EnemyPrefab;
    }

    public int GetAmount()
    {
        return m_Amount;
    }

    public float GetSpawnTime()
    {
        return m_SpawnTime;
    }

    public int GetMaxHealth()
    {
        return m_MaxHealth;
    }

    public float GetSpeed()
    {
        return m_Speed;
    }
}
