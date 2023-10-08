using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float m_Damage = 1;
    [SerializeField] private float m_Speed = 1;
    private Vector3 m_Direction = Vector3.zero;
    private Rigidbody2D m_Rigidbody;
    Camera m_MainCamera;

    void Start()
    {
        m_MainCamera = Camera.main;
        m_Rigidbody = this.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        m_Rigidbody.velocity = m_Direction * m_Speed;

        if (this.IsOffScreen())
        {
            this.gameObject.SetActive(false);
        }
    }

    public void SetDirection(Vector3 direction)
    {
        m_Direction = direction;
    }

    private bool IsOffScreen()
    {
        Vector3 objectPosition = transform.position;
        Vector3 viewportPoint = m_MainCamera.WorldToViewportPoint(objectPosition);

        bool isOffScreen = viewportPoint.x < 0 || viewportPoint.x > 1 || viewportPoint.y < 0 || viewportPoint.y > 1;

        return isOffScreen;
    }
}
