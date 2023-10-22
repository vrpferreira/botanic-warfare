using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int m_MaxHealth = 10;
    [SerializeField] private float m_Speed = 2;
    [SerializeField] private FloatingHealthBar m_FloatingHealthBar;
    [SerializeField] Player m_Player;

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
            Vector3 direction = m_Player.transform.position - transform.position;
            direction.Normalize();

            transform.position += direction * m_Speed * Time.deltaTime;
        }

        if (m_Health <= 0)
        {
            this.gameObject.SetActive(false);
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

    public void ResetHealth()
    {
        m_Health = m_MaxHealth;
        m_FloatingHealthBar.UpdateHealthBar(m_Health, m_MaxHealth);
    }

    public void SetPlayer(Player player)
    {
        m_Player = player;
    }

    public void SetMaxHealth(int maxHealth)
    {
        m_MaxHealth = maxHealth;
    }

    public void SetSpeed(float speed)
    {
        m_Speed = speed;
    }
}
