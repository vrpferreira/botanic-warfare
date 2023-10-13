using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int m_Health = 0;
    [SerializeField] private int m_MaxHealth = 10;
    [SerializeField] private FloatingHealthBar m_FloatingHealthBar;

    public void Start()
    {
        m_Health = m_MaxHealth;
        m_FloatingHealthBar.UpdateHealthBar(m_Health, m_MaxHealth);
    }

    public void Update()
    {

    }

    public void ApplyDamage(int damage)
    {
        m_Health -= damage;

        if (m_Health < 0)
        {
            m_Health = 0;
        }

        m_FloatingHealthBar.UpdateHealthBar(m_Health, m_MaxHealth);
    }

    public int GetHealth()
    {
        return m_Health;
    }
}
