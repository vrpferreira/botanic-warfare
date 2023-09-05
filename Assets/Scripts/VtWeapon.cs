using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VtWeapon : MonoBehaviour
{
    public int m_Layer = 0;
    public SpriteRenderer m_SpriteRenderer;
    public Transform m_AimPoint0;
    public Transform m_AimPoint1;
    public float m_AimLineExtensionDistance = 5.0f;
    private Color m_AimLineExtensionColor = Color.green;

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
        Vector3 start = m_AimPoint0.position;
        Vector3 bulletDirection = (m_AimPoint1.position - m_AimPoint0.position).normalized;
        Vector3 end = m_AimPoint0.position + bulletDirection * (m_AimLineExtensionDistance + Vector3.Distance(m_AimPoint0.position, m_AimPoint1.position));

        Debug.DrawLine(start, end, m_AimLineExtensionColor);
    }
}
