using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VtWeapon : MonoBehaviour
{
    public int m_Layer = 0;
    public SpriteRenderer m_SpriteRenderer;
    public Transform m_AimPoint0;
    public Transform m_AimPoint1;

    private void Start()
    {
        m_SpriteRenderer.sortingOrder = m_Layer;
    }

    private void Update()
    {

    }
}
