using UnityEngine;

public class Enemy : MonoBehaviour
{
    private int m_Health = 0;

    void Start()
    {

    }

    void Update()
    {

    }

    public void ApplyDamage(int damage)
    {
        m_Health -= damage;

        if (m_Health < 0)
        {
            m_Health = 0;
        }
    }

    public int GetHealth()
    {
        return m_Health;
    }
}
