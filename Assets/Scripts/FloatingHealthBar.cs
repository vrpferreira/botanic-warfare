using UnityEngine;
using UnityEngine.UI;

public class FloatingHealthBar : MonoBehaviour
{
    [SerializeField] private Slider m_Slider;
    [SerializeField] private Camera m_Camera;
    [SerializeField] private Transform m_Target;
    [SerializeField] private Vector3 m_Offset;

    private void Start()
    {
        m_Camera = Camera.main;
    }

    public void UpdateHealthBar(float currentHealth, float maxHealth)
    {
        m_Slider.value = currentHealth / maxHealth;
    }

    private void Update()
    {
        transform.rotation = m_Camera.transform.rotation;
        transform.position = m_Target.position + m_Offset;
    }
}
