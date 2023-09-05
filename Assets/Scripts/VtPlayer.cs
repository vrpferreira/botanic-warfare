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
        Vector3 mousePosition = Input.mousePosition;
        Vector3 worldMousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

        Vector3 direction = worldMousePosition - m_FrontArmParentBone.position;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        if (worldMousePosition.x < transform.position.x)
        {
            m_FrontArmParentBone.rotation = Quaternion.Euler(m_FrontArmParentBone.rotation.x, 180, -angle + 180);
            m_BackArmParentBone.rotation = Quaternion.Euler(m_BackArmParentBone.rotation.x, 180, -angle + 180);

            transform.localRotation = Quaternion.Euler(new Vector3(0, 180, 0));
        }
        else
        {
            m_FrontArmParentBone.rotation = Quaternion.Euler(m_FrontArmParentBone.rotation.x, 0, angle);
            m_BackArmParentBone.rotation = Quaternion.Euler(m_BackArmParentBone.rotation.x, 0, angle);

            transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 0));
        }
    }
}
