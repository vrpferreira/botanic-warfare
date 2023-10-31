using UnityEngine;

public class Weapon : MonoBehaviour
{
    public enum FireMode
    {
        Auto,
        Single
    }

    public FireMode m_FireMode;
    public int m_Layer = 0;
    public SpriteRenderer m_SpriteRenderer;
    public Transform m_AimPoint0;
    public Transform m_AimPoint1;
    public Bullet m_Bullet;
    public float m_FireRate = 1;
    public float m_BulletSpeed = 1;

    [SerializeField]
    private BulletObjectPool m_BulletObjectPool;

    private float m_AimDistance = 0f;
    private Vector3 m_AimDirection;
    private Vector3 m_MappedAimMousePosition;
    private float m_TimeSinceLastShot = 0;

    private void Start()
    {
        m_SpriteRenderer.sortingOrder = m_Layer;
    }

    private void Update()
    {
        DrawAimLine();

        if (Input.GetMouseButtonDown(0) && m_FireMode == FireMode.Single)
        {
            this.Shoot();
        }
        else if (Input.GetMouseButton(0) && m_FireMode == FireMode.Auto && Time.time - m_TimeSinceLastShot >= m_FireRate)
        {
            this.Shoot();
            m_TimeSinceLastShot = Time.time;
        }
    }

    public void DrawAimLine()
    {
        m_AimDirection = (m_AimPoint1.position - m_AimPoint0.position).normalized;

        m_MappedAimMousePosition = m_AimPoint0.position + m_AimDirection * m_AimDistance;

        Debug.DrawLine(m_AimPoint0.position, m_MappedAimMousePosition, Color.white);
    }

    public Transform GetBulletSpawn()
    {
        return m_AimPoint0;
    }

    public Vector3 GetMappedAimMousePosition()
    {
        return m_MappedAimMousePosition;
    }

    public void SetAimDistance(float distance)
    {
        m_AimDistance = distance;
    }

    private void Shoot()
    {
        Bullet bullet = m_BulletObjectPool.GetPooledObject();

        if (bullet != null)
        {
            bullet.transform.position = m_AimPoint0.position;
            bullet.transform.rotation = this.transform.rotation;
            bullet.SetDirection(m_AimDirection);
            bullet.SetSpeed(m_BulletSpeed);
            bullet.gameObject.SetActive(true);
        }
    }
}
