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
    public VtWeapon m_WeaponFront;
    public VtWeapon m_WeaponBack;
    private Animator m_Animator;

    private void Start()
    {
        m_Animator = GetComponent<Animator>();
    }

    private void Update()
    {
        //Move();
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

            if (m_MoveSpeed > 5)
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
        Vector3 vectorDirArmWeapon = m_WeaponFront.m_AimPoint1.position - m_FrontArmParentBone.position;
        Vector3 vectorDirectionArm = m_FrontArmParentBone.right;

        float angleArmWeapon = Vector3.Angle(vectorDirectionArm, vectorDirArmWeapon);

        //Aim
        Vector3 mousePosition = Input.mousePosition;
        Vector3 worldMousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

        Vector3 direction = worldMousePosition - m_FrontArmParentBone.position;

        float angleArmMouse = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;


        if (worldMousePosition.x < transform.position.x)
        {
            angleArmMouse += angleArmWeapon;

            m_FrontArmParentBone.rotation = Quaternion.Euler(m_FrontArmParentBone.rotation.x, 180, -angleArmMouse + 180);
            m_BackArmParentBone.rotation = Quaternion.Euler(m_BackArmParentBone.rotation.x, 180, -angleArmMouse + 180);

            transform.localRotation = Quaternion.Euler(new Vector3(0, 180, 0));
        }
        else
        {
            angleArmMouse -= angleArmWeapon;

            m_FrontArmParentBone.rotation = Quaternion.Euler(m_FrontArmParentBone.rotation.x, 0, angleArmMouse);
            m_BackArmParentBone.rotation = Quaternion.Euler(m_BackArmParentBone.rotation.x, 0, angleArmMouse);

            transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 0));
        }
    }
}
