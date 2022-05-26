using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    float horizontalInput = 0f;
    bool isTouchingTheGround = false;
    public float jumpForce = 10f;
    public float runningSpeed = 6f;
    Rigidbody2D rigidBody;
    Animator animator;
    SpriteRenderer spriteRenderer;

    bool isTouchingFront = false;
    public Transform frontCheck;
    bool wallSliding = false;
    public float wallSlidingSpeed = 1.5f;

    bool wallJumping;
    public Vector2 wallJumpingForce = new Vector2(5f, 5f);
    public float wallJumpTime = 1f;

    const string STATE_MOVING = "isMoving";
    const string STATE_ON_THE_GROUND = "isOnTheGround";
    const string VERTICAL_VELOCITY = "verticalVelocity";
    const string STATE_SLIDING = "isSliding";

    public LayerMask groundMask;

    void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        animator.SetBool(STATE_MOVING, false);
        animator.SetBool(STATE_ON_THE_GROUND, false);
        animator.SetBool(STATE_SLIDING, false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            Jump();
        }
        isTouchingTheGround = IsTouchingTheGround();
        animator.SetBool(STATE_ON_THE_GROUND, isTouchingTheGround);
        animator.SetFloat(VERTICAL_VELOCITY, rigidBody.velocity.y);
        animator.SetBool(STATE_MOVING, IsMoving());
        Debug.DrawRay(this.transform.position, Vector2.down * 1.2f, Color.red);

        isTouchingFront = Physics2D.OverlapCircle(frontCheck.position, 0.5f, groundMask);
        wallSliding = (isTouchingFront && !isTouchingTheGround && horizontalInput != 0);
        animator.SetBool(STATE_SLIDING, wallSliding);
        if (wallSliding)
        {
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, Mathf.Clamp(rigidBody.velocity.y, -wallSlidingSpeed, float.MaxValue));
        }

        if (Input.GetButtonDown("Jump") && wallSliding)
        {
            wallJumping = true;
            Vector2 wallJump = new Vector2(wallJumpingForce.x * -horizontalInput, wallJumpingForce.y);
            rigidBody.AddForce(wallJump, ForceMode2D.Impulse);
            Invoke("SetWallJumpingToFalse", wallJumpTime);
        }
    }

    private void FixedUpdate()
    {
        CaptureInput();
        Move();
    }

    void Jump()
    {
        if (isTouchingTheGround)
        {
            rigidBody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }

    void Move()
    {
        if (!wallSliding && !wallJumping)
        {
            rigidBody.velocity = new Vector2(horizontalInput * runningSpeed, rigidBody.velocity.y);
        }
        if(horizontalInput < 0)
        {
            spriteRenderer.flipX = true;
        }
        if(horizontalInput > 0)
        {
            spriteRenderer.flipX = false;
        }
    }

    void CaptureInput() => horizontalInput = Input.GetAxis("Horizontal");

    bool IsMoving() => rigidBody.velocity.x != 0;

    bool IsTouchingTheGround() => Physics2D.Raycast(this.transform.position, Vector2.down, 1.2f, groundMask);

    void SetWallJumpingToFalse() => wallJumping = false;
}
