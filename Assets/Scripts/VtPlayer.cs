using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VtPlayer : MonoBehaviour
{
    public float m_MoveSpeed = 5;
    public float m_DividerAnimationWalk = 1;
    public Transform m_FrontArmParentBone;
    public Transform m_BackArmParentBone;
    public Transform m_FrontArmDirectionPoint;
    public VtWeapon m_WeaponFront;
    public VtWeapon m_WeaponBack;
    private Animator m_Animator;

    private void Start()
    {
        m_Animator = GetComponent<Animator>();
    }

    private void Update()
    {
        Move();
        Aim();
    }

    private void Move()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 moveDirection = new Vector3(horizontalInput, verticalInput, 0.0f);
        transform.position += moveDirection * m_MoveSpeed * Time.deltaTime;

        if (moveDirection.magnitude > 0)
        {
            m_Animator.SetBool("Moving", true);

            if (m_MoveSpeed > 3)
            {
                m_Animator.SetBool("Running", true);
            }
            else
            {
                m_Animator.SetBool("Running", false);
            }
        }
        else
        {
            m_Animator.SetBool("Moving", false);
        }
    }

    private void Aim()
    {

        //Rotate arm according mouse position
        Vector3 worldMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 dirArmMouse = worldMousePosition - m_FrontArmParentBone.position;

        float angleArmMouse = Mathf.Atan2(dirArmMouse.y, dirArmMouse.x) * Mathf.Rad2Deg;

        //Based on the weapon aim vector, map the mouse position, and calculate the correct angle to match de weapon aim direction with the mouse position
        Vector3 vectorDirectionArm = (m_FrontArmDirectionPoint.position - m_FrontArmParentBone.position).normalized;
        float distance = Vector2.Distance(m_FrontArmParentBone.position, worldMousePosition);

        m_WeaponFront.SetAimLindeDistance(distance);
        Vector3 weaponMousePointMapped = m_WeaponFront.GetMappedAimMousePosition();

        Vector3 end = m_FrontArmParentBone.position + vectorDirectionArm * distance;

        Vector3 vecDirArmMouse = (weaponMousePointMapped - m_FrontArmParentBone.position).normalized;
        Vector3 vecDirArm = (end - m_FrontArmParentBone.position).normalized;

        float adjustedAngle = Vector3.Angle(vecDirArmMouse, vecDirArm);

        if (worldMousePosition.x < transform.position.x)
        {
            //Final rotation for arm
            float finalRotation = -angleArmMouse + 180 - adjustedAngle;

            m_FrontArmParentBone.rotation = Quaternion.Euler(m_FrontArmParentBone.rotation.x, 180, finalRotation);
            m_BackArmParentBone.rotation = Quaternion.Euler(m_BackArmParentBone.rotation.x, 180, finalRotation);

            transform.localRotation = Quaternion.Euler(new Vector3(0, 180, 0));
        }
        else
        {
            //Final rotation for arm
            float finalRotation = angleArmMouse - adjustedAngle;

            m_FrontArmParentBone.rotation = Quaternion.Euler(m_FrontArmParentBone.rotation.x, 0, finalRotation);
            m_BackArmParentBone.rotation = Quaternion.Euler(m_BackArmParentBone.rotation.x, 0, finalRotation);

            transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 0));
        }
    }
}
