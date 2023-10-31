using UnityEngine;

public class PlayerAim : MonoBehaviour
{
    public Transform m_FrontArmParentBone;
    public Transform m_BackArmParentBone;
    public Transform m_FrontArmDirectionPoint;
    public Weapon m_WeaponFront;
    public Weapon m_WeaponBack;

    private void Start()
    {

    }

    private void Update()
    {
        Aim();
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

        Transform bulletSpawnWeaponFront = m_WeaponFront.GetBulletSpawn();
        Transform bulletSpawnWeaponBack = m_WeaponBack.GetBulletSpawn();

        float distWeaponFront = Vector2.Distance(bulletSpawnWeaponFront.position, worldMousePosition);
        float distWeaponBack = Vector2.Distance(bulletSpawnWeaponBack.position, worldMousePosition);

        m_WeaponFront.SetAimDistance(distWeaponFront);
        m_WeaponBack.SetAimDistance(distWeaponBack);

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
        }
        else
        {
            //Final rotation for arm
            float finalRotation = angleArmMouse - adjustedAngle;

            m_FrontArmParentBone.rotation = Quaternion.Euler(m_FrontArmParentBone.rotation.x, 0, finalRotation);
            m_BackArmParentBone.rotation = Quaternion.Euler(m_BackArmParentBone.rotation.x, 0, finalRotation);
        }
    }
}
