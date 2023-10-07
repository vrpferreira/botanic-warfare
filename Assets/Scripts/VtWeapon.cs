using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VtWeapon : MonoBehaviour
{
    public int m_Layer = 0;
    public SpriteRenderer m_SpriteRenderer;
    public Transform m_AimPoint0;
    public Transform m_AimPoint1;

    private float m_AimLineDistance = 0f;
    private Color m_AimLineExtensionColor = Color.green;
    private Vector3 m_AimDirection;
    private Vector3 m_MappedAimMousePosition;

    private void Start()
    {
        m_SpriteRenderer.sortingOrder = m_Layer;
    }

    private void Update()
    {
        DrawAimLine();
    }

    public void DrawAimLine()
    {
        m_AimDirection = (m_AimPoint1.position - m_AimPoint0.position).normalized;

        m_MappedAimMousePosition = m_AimPoint0.position + m_AimDirection * m_AimLineDistance;

        Debug.DrawLine(m_AimPoint0.position, m_MappedAimMousePosition, m_AimLineExtensionColor);
    }

    public Vector3 GetMappedAimMousePosition()
    {
        return m_MappedAimMousePosition;
    }

    public void SetAimLindeDistance(float distance)
    {
        m_AimLineDistance = distance;
    }
}
