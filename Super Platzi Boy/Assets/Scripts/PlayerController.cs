using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float jumpForce = 10f;
    public float runningSpeed = 2f;
    Rigidbody2D rigidBody;
    Animator animator;
    SpriteRenderer spriteRenderer;

    const string STATE_MOVING = "isMoving";
    const string STATE_ON_THE_GROUND = "isOnTheGround";
    const string VERTICAL_VELOCITY = "verticalVelocity";

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
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            Jump();
        }
        animator.SetBool(STATE_ON_THE_GROUND, IsTouchingTheGround());
        animator.SetFloat(VERTICAL_VELOCITY, rigidBody.velocity.y);
        animator.SetBool(STATE_MOVING, IsMoving());
        Debug.DrawRay(this.transform.position, Vector2.down * 1.2f, Color.red);
    }

    private void FixedUpdate()
    {
        Move();
    }

    void Jump()
    {
        if (IsTouchingTheGround())
        {
            rigidBody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }

    void Move()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        rigidBody.velocity = new Vector2(horizontalInput * runningSpeed, rigidBody.velocity.y);
        if(horizontalInput < 0)
        {
            spriteRenderer.flipX = true;
        }
        if(horizontalInput > 0)
        {
            spriteRenderer.flipX = false;
        }
    }

    bool IsMoving() => rigidBody.velocity.x != 0;

    bool IsTouchingTheGround() => Physics2D.Raycast(this.transform.position, Vector2.down, 1.2f, groundMask);
}
