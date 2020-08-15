using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.UIElements.GraphView;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    
    public float moveSpeed;
    public float jumpForce;
    private float moveInput;

    private Rigidbody2D rb;

    private bool facingRight = true;

    private bool isGrounded;
    public Transform groundCheck;
    public float checkRadius;
    public LayerMask whatIsGround;

    private int extraJumps;
    public int extraJumpValue;

    private bool isDashing;
    public float dashSpeed;
    private float dashTime;
    public float startDashTime;
    private int dashDirection;
    public GameObject dashEffect;
    public float dashCamShakeAmt = 0.05f;
    CameraShake camShake;

    private float jumpTimeCounter;
    public float jumpTime;
    private bool isJumping;

    // Start is called before the first frame update
    void Start()
    {
        camShake = GameMaster.gm.GetComponent<CameraShake>();
        if (camShake == null)
        {
            Debug.LogError("No CameraShake script found on GM object");
        }
        dashTime = startDashTime;
        extraJumps = extraJumpValue;
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);

        if (Input.GetKeyUp(KeyCode.Space))
        {
            isJumping = false;
        }
        moveInput = Input.GetAxisRaw("Horizontal");
        Debug.Log(moveInput);
        if (!isDashing)
        {
            rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);
        }


        if (facingRight == false && moveInput > 0)                   //moveInput > 0 --> right
        {
            Flip();
        }
        else if (facingRight == true && moveInput < 0)             //moveInput < 0 --> left
        {
            Flip();
        }

 
    }

    private void Update()
    {
        // ========================================= JUMPING ================================================
        if (Input.GetKeyDown(KeyCode.Space) && extraJumps > 0)
        {
            isJumping = true;
            if (isGrounded)
            {
                jumpTimeCounter = jumpTime;
            }
            rb.velocity = Vector2.up * jumpForce;
            extraJumps--;
        }
        else if (Input.GetKeyDown(KeyCode.Space) && extraJumps == 0 && isGrounded == true)
        {
            rb.velocity = Vector2.up * jumpForce;
        }

        if (isGrounded == true)
        {
            extraJumps = extraJumpValue;
        }

        if (Input.GetKey(KeyCode.Space) && isJumping == true)
        {
            if (jumpTimeCounter > 0)
            {
                rb.velocity = Vector2.up * jumpForce;
                jumpTimeCounter -= Time.deltaTime;
            }
            else
            {
                isJumping = false;
            }
        }
        // ========================================= JUMPING ================================================

        // ========================================= DASHING ================================================
        
        if (dashDirection == 0)
        {
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                if (moveInput > 0)
                {
                    dashDirection = 1;
                }
                else if (moveInput < 0)
                {
                    dashDirection = 2;
                }
            }
        }
        else
        {
            isDashing = true;
            Instantiate(dashEffect, transform.position, Quaternion.identity);
            camShake.Shake(dashCamShakeAmt, dashTime);
            if (dashTime <= 0)
            {
                dashDirection = 0;
                dashTime = startDashTime;
                rb.velocity = Vector2.zero;
                isDashing = false;
            }
            else
            {
                dashTime -= Time.deltaTime;

                if (dashDirection == 1)
                {
                    rb.velocity = Vector2.right * dashSpeed;
                }
                else if (dashDirection == 2)
                {
                    rb.velocity = Vector2.left * dashSpeed;
                }
            }
        }
        // ========================================= DASHING ================================================
        
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;
    }
    
}
