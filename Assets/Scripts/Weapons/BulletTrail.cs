using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletTrail : MonoBehaviour
{
    private Vector3 m_StartPosition;
    private Vector3 m_TargetPosition;
    private float m_Progress;

    [SerializeField] private float m_Speed = 40f;

    private void Start()
    {
        m_StartPosition = transform.position.WithAxis(Axis.Z, -1);
    }

    private void Update()
    {
        m_Progress += Time.deltaTime * m_Speed;
        transform.position = Vector3.Lerp(m_StartPosition, m_TargetPosition, m_Progress);
    }

    public void SetTargetPosition(Vector3 targetPosition)
    {
        m_TargetPosition = targetPosition.WithAxis(Axis.Z, -1);
    }
}
