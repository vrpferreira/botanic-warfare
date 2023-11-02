using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float m_MoveSpeed = 5;

    [SerializeField]
    public float m_DividerAnimationWalk = 1;

    private Animator m_Animator;
    private Rigidbody2D m_Rigidbody;
    private float m_HorizontalInput;
    private float m_VerticalInput;
    private bool m_IsFacingRight;
    private Vector3 m_WorldMousePosition;

    private void Start()
    {
        m_Animator = GetComponent<Animator>();
        m_Rigidbody = GetComponent<Rigidbody2D>();
        m_IsFacingRight = true;
    }

    private void Update()
    {
        m_HorizontalInput = Input.GetAxis("Horizontal");
        m_VerticalInput = Input.GetAxis("Vertical");

        m_WorldMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        this.HandleFlip();
    }

    private void FixedUpdate()
    {
        Vector3 moveDirection = new Vector3(m_HorizontalInput, m_VerticalInput, 0.0f);

        m_Rigidbody.MovePosition(
            new Vector3(
                transform.position.x + moveDirection.x * m_MoveSpeed * Time.fixedDeltaTime,
                transform.position.y + moveDirection.y * m_MoveSpeed * Time.fixedDeltaTime,
                0
            )
        );

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

    private void HandleFlip()
    {
        if (m_IsFacingRight && m_WorldMousePosition.x < transform.position.x)
        {
            this.Flip();
        }
        else if (!m_IsFacingRight && m_WorldMousePosition.x > transform.position.x)
        {
            this.Flip();
        }
    }

    private void Flip()
    {
        m_IsFacingRight = !m_IsFacingRight;
        transform.Rotate(0, -180, 0);
    }
}
