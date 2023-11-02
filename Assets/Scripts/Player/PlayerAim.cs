using UnityEngine;

public class PlayerAim : MonoBehaviour
{
    public Transform m_FrontArmParentBone;
    public Transform m_BackArmParentBone;
    public Transform m_FrontArmDirectionPoint;
    public Weapon m_WeaponFront;
    public Weapon m_WeaponBack;

    private Vector3 m_WorldMousePosition;

    private void Start()
    {

    }

    private void Update()
    {
        m_WorldMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        m_WeaponFront.SetWorldMousePosition(m_WorldMousePosition);
        m_WeaponBack.SetWorldMousePosition(m_WorldMousePosition);

        Quaternion frontArmRotation = this.CalculateFrontArmRotation();

        m_FrontArmParentBone.rotation = frontArmRotation;
        m_BackArmParentBone.rotation = frontArmRotation;
    }

    private Quaternion CalculateFrontArmRotation()
    {
        //Rotate arm according mouse position
        Vector3 dirArmMouse = m_WorldMousePosition - m_FrontArmParentBone.position;

        float angleArmMouse = Mathf.Atan2(dirArmMouse.y, dirArmMouse.x) * Mathf.Rad2Deg;

        //Based on the weapon aim vector, map the mouse position, and calculate the correct angle to match de weapon aim direction with the mouse position
        Vector3 vectorDirectionArm = (m_FrontArmDirectionPoint.position - m_FrontArmParentBone.position).normalized;
        float distance = Vector2.Distance(m_FrontArmParentBone.position, m_WorldMousePosition);

        Vector3 weaponMousePointMapped = m_WeaponFront.GetAimMousePosition();

        Vector3 end = m_FrontArmParentBone.position + vectorDirectionArm * distance;

        Vector3 vecDirArmMouse = (weaponMousePointMapped - m_FrontArmParentBone.position).normalized;
        Vector3 vecDirArm = (end - m_FrontArmParentBone.position).normalized;

        float adjustedAngle = Vector3.Angle(vecDirArmMouse, vecDirArm);

        if (m_WorldMousePosition.x < transform.position.x)
        {
            return Quaternion.Euler(m_FrontArmParentBone.rotation.x, 180, -angleArmMouse + 180 - adjustedAngle);
        }
        else
        {
            return Quaternion.Euler(m_FrontArmParentBone.rotation.x, 0, angleArmMouse - adjustedAngle);
        }
    }
}
