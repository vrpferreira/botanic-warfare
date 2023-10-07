using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float m_Damage = 1;
    [SerializeField] private float m_Speed = 5;
    private Vector3 m_Direction = Vector3.zero;
    private Rigidbody2D m_Rigidbody;

    void Start()
    {
        m_Rigidbody = this.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        m_Rigidbody.velocity = m_Direction * m_Speed;
    }

    public void SetDirection(Vector3 direction)
    {
        m_Direction = direction;
    }
}
