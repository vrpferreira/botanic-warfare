using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VtPlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5;
    public float dividerAnimationWalk = 1;

    private Animator animator;
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 moveDirection = new Vector3(horizontalInput, verticalInput, 0.0f);
        transform.position += moveDirection * moveSpeed * Time.deltaTime;

        // Define as condições para as animações
        if (moveDirection.magnitude > 0)
        {
            animator.SetBool("Moving", true);

            if (moveSpeed > 5)
            {
                animator.SetBool("Running", true);
            }
            else
            {
                animator.SetBool("Running", false);
            }
        }
        else
        {
            animator.SetBool("Moving", false);
        }
        /*
                // Flip o sprite conforme a direção do movimento
                if (horizontalInput > 0)
                {
                    spriteRenderer.flipX = false; // Não inverter
                }
                else if (horizontalInput < 0)
                {
                    spriteRenderer.flipX = true; // Inverter
                }*/
    }
}
