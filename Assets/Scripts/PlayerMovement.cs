using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    PlayerShoot ps;

    private float horizontal;
    private float speed = 8f;
    private float jumpingPower = 16f;

    private float coyoteTime = 0.2f;
    private float coyoteTimeCounter;

    private float jumpBufferTime = 0.2f;
    private float jumpBufferCounter;

    private bool doubleJump;

    private bool canDash = true;
    private bool isDashing;
    private float dashingPower = 5f;
    private float dashingTime = 0.2f;
    private float dashingCooldown = 1f;

    private bool isWallSliding;
    private float wallSlidingSpeed = 2f;

    private bool isWallJumping;
    private float wallJumpingDirection;
    private float wallJumpingTime = 0.2f;
    private float wallJumpingCounter = 0.2f;
    private float wallJumpingDuration = 0.4f;
    private Vector2 wallJumpingPower = new Vector2(2f, 16f);

    private Animator anim;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private TrailRenderer tr;
    [SerializeField] private Transform wallCheck;
    [SerializeField] private LayerMask wallLayer;
    [HideInInspector] public bool isFacingRight = true;
    [HideInInspector] public bool isFacingUp = false;
    private void Start()
    {
        anim = GetComponent<Animator>();
        ps = GetComponent<PlayerShoot>();
    }
    void Update()
    {

        if(isDashing)
        {
            return;
        }


        horizontal = Input.GetAxisRaw("Horizontal");

        if (horizontal != 0f)
        {
            anim.SetBool("isRunning", true);
        }
        else
        {
            anim.SetBool("isRunning", false);
        }

        if (!isGrounded())
        {
            anim.SetBool("isJumping", true);
        }
        else
        {
            anim.SetBool("isJumping", false);
        }
        if(ps.timeUntilFire < Time.time)
        {
            anim.SetBool("isShooting", false);
        }
        else
        {
            anim.SetBool("isShooting", true);
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            anim.SetBool("isFacingUp", true);
            isFacingUp = true;
        }
        if(Input.GetKeyUp(KeyCode.W))
        {
            anim.SetBool("isFacingUp", false);
            isFacingUp = false;
        }
           

        

        if (isGrounded() && !Input.GetButton("Jump"))
        {
            doubleJump = false;
        }

        if(isGrounded() || doubleJump)
        {
            coyoteTimeCounter = coyoteTime;
        }
        else
        {
            coyoteTimeCounter -= Time.deltaTime; 
        }

        if(Input.GetButtonDown("Jump"))
        {
            jumpBufferCounter = jumpBufferTime;   
        }
        else
        {
            jumpBufferCounter -= Time.deltaTime;
        }


        if(jumpBufferCounter > 0f && coyoteTimeCounter > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
            jumpBufferCounter = 0f;
            doubleJump = !doubleJump;
        }
        if(Input.GetButtonUp("Jump") && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
            coyoteTimeCounter = 0f;
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash)
        {
            StartCoroutine(Dash());
        }

        wallSlide();
        wallJump();
        if (!isWallJumping)
        {
            Flip();
        }
    }

    private bool isGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    private bool isWalled()
    {   
        return Physics2D.OverlapCircle(wallCheck.position,0.2f,wallLayer);
    }

    private void wallSlide()
    {
        if (isWalled() && !isGrounded() && horizontal != 0f)
        {
            isWallSliding = true;
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y,-wallSlidingSpeed,float.MaxValue));
        }
        else
        {
            isWallSliding = false;
        }
    }

    private void wallJump()
    {
        if (isWallSliding)
        {
            isWallJumping = false;
            wallJumpingDirection = -transform.localScale.x;
            wallJumpingCounter = wallJumpingTime;

            CancelInvoke(nameof(stopWallJumping));
        }
        else
        {
            wallJumpingCounter -= Time.deltaTime;
        }
        if (Input.GetButtonDown("Jump") && wallJumpingCounter > 0f)
        {
            isWallJumping = true;
            rb.velocity = new Vector2(wallJumpingDirection * wallJumpingPower.x, wallJumpingPower.y);
            wallJumpingCounter = 0f; 
            if(transform.localScale.x != wallJumpingDirection)
            {
                isFacingRight = !isFacingRight;
                Vector3 localScale = transform.localScale;
                localScale.x *= -1f;
                transform.localScale = localScale;
            }
            Invoke(nameof(stopWallJumping), wallJumpingDuration);
        }
    }

    private void stopWallJumping()
    {
        isWallJumping = false;
    }

    private void FixedUpdate()
    {
        if (isDashing)
        {
            return;
        }
        if (!isWallJumping)
        {
            rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
        }
    }

  

    private void Flip()
    {
        if(isFacingRight && horizontal <0 || !isFacingRight && horizontal > 0)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1;
            transform.localScale= localScale;
        }
        
    }

    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0f;
        rb.velocity = new Vector2(transform.localScale.x * dashingPower, 0f);
        tr.emitting = true;
        yield return new WaitForSeconds(dashingTime);
        tr.emitting = false;
        rb.gravityScale = originalGravity;
        isDashing= false;
        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
    }



}
