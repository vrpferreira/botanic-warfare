using System.Collections.Generic;
using UnityEngine;

public class BulletObjectPool : MonoBehaviour
{
    public static BulletObjectPool m_Instance;
    public int m_AmountToPool = 0;
    public List<Bullet> m_PooledObjects;
    public Bullet m_ObjectToPool;

    private void Awake()
    {
        if (m_Instance == null)
        {
            m_Instance = this;
        }
    }

    private void Start()
    {
        m_PooledObjects = new List<Bullet>();

        Bullet obj;
        for (int i = 0; i < m_AmountToPool; i++)
        {
            obj = Instantiate(m_ObjectToPool);
            obj.gameObject.SetActive(false);
            obj.transform.parent = this.transform;
            m_PooledObjects.Add(obj);
        }
    }

    private void Update()
    {

    }

    public Bullet GetPooledObject()
    {
        for (int i = 0; i < m_AmountToPool; i++)
        {
            if (!m_PooledObjects[i].gameObject.activeInHierarchy)
            {
                return m_PooledObjects[i];
            }
        }
        return null;
    }
}
