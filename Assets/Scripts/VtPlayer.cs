using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VtPlayer : MonoBehaviour
{
    public enum WeaponMode
    {
        Single,
        Dual
    }
    public WeaponMode m_WeaponMode;

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
        m_WeaponMode = WeaponMode.Single;
        m_Animator = GetComponent<Animator>();
    }

    private void Update()
    {
        HandleInput();
        CheckWeaponMode();
        Move();
        Aim();
    }

    private void Move()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 moveDirection = new Vector3(horizontalInput, verticalInput, 0.0f);
        transform.position += moveDirection * m_MoveSpeed * Time.deltaTime;

        /*if (moveDirection.magnitude > 0)
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
        }*/
    }

    private void Aim()
    {
        //Angle between arm and weapon
        Vector3 vectorDirArmWeapon = m_WeaponFront.m_AimPoint1.position - m_FrontArmParentBone.position;
        Vector3 vectorDirectionArm = m_FrontArmDirectionPoint.position - m_FrontArmParentBone.position;

        float angleArmWeapon = Vector3.Angle(vectorDirectionArm, vectorDirArmWeapon);

        //Rotate arm according mouse position
        Vector3 mousePosition = Input.mousePosition;
        Vector3 worldMousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

        Vector3 direction = worldMousePosition - m_FrontArmParentBone.position;

        float angleArmMouse = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        if (worldMousePosition.x < transform.position.x)
        {
            //Final rotation for arm
            float finalRotation = -angleArmMouse - angleArmWeapon + 180;

            m_FrontArmParentBone.rotation = Quaternion.Euler(m_FrontArmParentBone.rotation.x, 180, finalRotation);
            m_BackArmParentBone.rotation = Quaternion.Euler(m_BackArmParentBone.rotation.x, 180, finalRotation);

            transform.localRotation = Quaternion.Euler(new Vector3(0, 180, 0));
        }
        else
        {
            //Final rotation for arm
            float finalRotation = angleArmMouse - angleArmWeapon;

            m_FrontArmParentBone.rotation = Quaternion.Euler(m_FrontArmParentBone.rotation.x, 0, finalRotation);
            m_BackArmParentBone.rotation = Quaternion.Euler(m_BackArmParentBone.rotation.x, 0, finalRotation);

            transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 0));
        }
    }

    private void CheckWeaponMode()
    {
        if (m_WeaponMode == WeaponMode.Single)
        {
            m_Animator.SetBool("hasSingleWeapon", true);
        }
        else if (m_WeaponMode == WeaponMode.Dual)
        {
            m_Animator.SetBool("hasSingleWeapon", false);
        }
    }

    private void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            m_WeaponMode = WeaponMode.Single;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            m_WeaponMode = WeaponMode.Dual;
        }
    }
}
