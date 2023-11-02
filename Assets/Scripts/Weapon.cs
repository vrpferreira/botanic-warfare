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
    public Transform m_BulletSpawn;
    public Transform m_AimPointDirection;
    public Bullet m_Bullet;
    public float m_FireRate = 1;
    public float m_BulletSpeed = 1;

    [SerializeField]
    private BulletObjectPool m_BulletObjectPool;

    private Vector3 m_AimDirection;
    private Vector3 m_AimMousePosition;
    private Vector3 m_WorldMousePosition;
    private float m_TimeSinceLastShot = 0;

    private void Start()
    {
        m_SpriteRenderer.sortingOrder = m_Layer;
    }

    private void Update()
    {
        this.HandleAimMousePosition();

        this.HandleShoot();
    }

    private void HandleAimMousePosition()
    {
        m_AimDirection = (m_AimPointDirection.position - m_BulletSpawn.position).normalized;

        m_AimMousePosition = m_BulletSpawn.position + m_AimDirection * Vector2.Distance(m_BulletSpawn.position, m_WorldMousePosition);

        Debug.DrawLine(m_BulletSpawn.position, m_AimMousePosition, Color.white);
    }

    public Vector3 GetAimMousePosition()
    {
        return m_AimMousePosition;
    }

    public void SetWorldMousePosition(Vector3 aimMousePosition)
    {
        m_WorldMousePosition = aimMousePosition;
    }

    private void HandleShoot()
    {
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

    private void Shoot()
    {
        Bullet bullet = m_BulletObjectPool.GetPooledObject();

        if (bullet != null)
        {
            bullet.transform.position = m_BulletSpawn.position;
            bullet.transform.rotation = this.transform.rotation;
            bullet.SetDirection(m_AimDirection);
            bullet.SetSpeed(m_BulletSpeed);
            bullet.gameObject.SetActive(true);
        }
    }
}
