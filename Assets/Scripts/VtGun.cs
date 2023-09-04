using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VtGun : MonoBehaviour
{
    public int m_Layer = 0;
    public SpriteRenderer m_SpriteRenderer;
    public Transform m_BulletSpawner;

    private void Start()
    {
        m_SpriteRenderer.sortingOrder = m_Layer;
    }

    private void Update()
    {

    }
}
