using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int m_MaxHealth = 10;
    [SerializeField] private float m_Speed = 2;
    [SerializeField] private FloatingHealthBar m_FloatingHealthBar;
    [SerializeField] Transform m_Player;

    private int m_Health = 0;

    public void Start()
    {
        m_Health = m_MaxHealth;
        m_FloatingHealthBar.UpdateHealthBar(m_Health, m_MaxHealth);
    }

    public void Update()
    {
        if (m_Player != null)
        {
            Vector3 direction = m_Player.position - transform.position;
            direction.Normalize();

            transform.position += direction * m_Speed * Time.deltaTime;
        }
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
