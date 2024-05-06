using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float acceleration;
    [SerializeField] float deceleration;
    [SerializeField] float jumpForce;
    [SerializeField] int maxJumps;
    [SerializeField] float coyoteTimeCount;
    [SerializeField] SpriteRenderer playerSpriteRenderer;
    [SerializeField] Animator playerAnimator;
    [SerializeField] Rigidbody2D playerRigidbody2D;
    [SerializeField] Transform groundCheckPosition;
    [SerializeField] LayerMask groundLayer;
    Vector2 moveInput;
    int jumpCount;
    float lastGroundTime;


    private void FixedUpdate()
    {
        playerAnimator.SetBool("running", false);
        playerAnimator.SetBool("jumping", false);
        if (Mathf.Abs(moveInput.x) > 0f)
        {
            playerAnimator.SetBool("running", true);

            if (moveInput.x < 0f)
                playerSpriteRenderer.flipX = true;
            else 
                playerSpriteRenderer.flipX = false;

            playerRigidbody2D.AddForce(new Vector2(moveInput.x * acceleration, 0));
        }
        else
        {
            playerRigidbody2D.velocity = new Vector2(Mathf.MoveTowards(playerRigidbody2D.velocity.x, 0f, deceleration * Time.fixedDeltaTime), playerRigidbody2D.velocity.y);
        }
        playerRigidbody2D.velocity = new Vector2(Mathf.Clamp(playerRigidbody2D.velocity.x, -speed, speed), playerRigidbody2D.velocity.y);

        if(IsGrounded())
        {
            jumpCount = 0;
            lastGroundTime = Time.time;
        }
        else if(jumpCount == 0
            //&& lastGroundTime + coyoteTimeCount < Time.time
            )
        {
            playerAnimator.SetBool("jumping", true);
            jumpCount++;
            Debug.Log(jumpCount);
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed && jumpCount < maxJumps)
        {
            playerRigidbody2D.velocity = new Vector2(playerRigidbody2D.velocity.x, 0f);
            playerRigidbody2D.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }
    
    private bool IsGrounded()
    {
        return Physics2D.OverlapBox(groundCheckPosition.position, groundCheckPosition.lossyScale, 0 , groundLayer) != null && playerRigidbody2D.velocity.y <= Mathf.Epsilon;
    }
}
