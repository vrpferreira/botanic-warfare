using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VtPlayer : MonoBehaviour
{
    public Transform m_FrontArmParentBone;
    public Transform m_BackArmParentBone;

    public Transform m_WeaponFront;
    public Transform m_WeaponBack;

    private void Start()
    {

    }

    private void Update()
    {
        Aim();
    }

    private void Aim()
    {
        Vector3 mousePosition = Input.mousePosition;
        Vector3 worldMousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

        Vector3 direction = worldMousePosition - m_FrontArmParentBone.position;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        if (worldMousePosition.x < transform.position.x)
        {
            m_FrontArmParentBone.rotation = Quaternion.Euler(m_FrontArmParentBone.rotation.x, 180, -angle + 180);
            m_BackArmParentBone.rotation = Quaternion.Euler(m_BackArmParentBone.rotation.x, 180, -angle + 180);

            transform.localRotation = Quaternion.Euler(new Vector3(0, 180, 0));

            m_WeaponFront.localRotation = Quaternion.Euler(new Vector3(0, 0, 0));
            m_WeaponBack.localRotation = Quaternion.Euler(new Vector3(0, 0, 0));
        }
        else
        {
            m_FrontArmParentBone.rotation = Quaternion.Euler(m_FrontArmParentBone.rotation.x, 0, angle);
            m_BackArmParentBone.rotation = Quaternion.Euler(m_BackArmParentBone.rotation.x, 0, angle);

            transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 0));

            m_WeaponFront.localRotation = Quaternion.Euler(new Vector3(0, 0, 0));
            m_WeaponBack.localRotation = Quaternion.Euler(new Vector3(0, 0, 0));
        }
    }
}
