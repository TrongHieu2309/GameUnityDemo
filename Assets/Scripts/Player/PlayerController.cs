using Unity.VisualScripting;
using UnityEngine;
public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    [Header("Movement")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpPower;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask wallLayer;
    [SerializeField] private BoxCollider2D boxCollider2D;

    private Rigidbody2D rb;
    private float horizontalInput;
    private Animator anim;
    private float isRunning;
    private float jumpCount = 0;
    private bool jumpPressed;
    private bool fall;
    private float defaultSpeed;
    private float defaultJump;

    private bool isWallSliding;
    private float wallSlidingSpeed = 0.5f;
    private bool isWallJumping;
    private float wallJumpingTime = 0.2f;
    private float wallJumpingCounter;
    private float wallJumpingDuration = 0.4f;

    #region MonoBehaviour Lifecycle Methods
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        instance = this;
    }

    void Start()
    {
        defaultSpeed = moveSpeed;
        defaultJump = jumpPower;
    }

    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        Movement();
        
        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
        {
            jumpPressed = true;
            jumpCount = 0;
            
        }
        if (Input.GetKeyUp(KeyCode.Space) && rb.linearVelocity.y > 0)
        {
            rb.linearVelocity = new Vector2(horizontalInput, rb.linearVelocity.y / 2);
        }
        if (Input.GetKeyDown(KeyCode.Space) && !IsGrounded() && jumpCount == 0)
        {
            rb.linearVelocity = new Vector2(horizontalInput, jumpPower);
            jumpCount++;
            anim.SetTrigger("doubleJump");
        }

        if (rb.linearVelocity.y < -0.5)
        {
            fall = true;
        }
        else
        {
            fall = false;
        }

        WallSlide();
        WallJump();

        anim.SetBool("onWall", isWallSliding);
        anim.SetBool("fall", fall);
        anim.SetFloat("run01", isRunning);
        anim.SetBool("grounded", IsGrounded());
    }

    void FixedUpdate()
    {
        if (jumpPressed)
        {
            Jump();
            jumpPressed = false;
        }
        if (!isWallJumping)
        {
            rb.linearVelocity = new Vector2(horizontalInput * moveSpeed, rb.linearVelocity.y);
        }
    }
    #endregion


    #region Movement
    private void Movement()
    {
        rb.linearVelocity = new Vector2(horizontalInput * moveSpeed, rb.linearVelocity.y);

        isRunning = Mathf.Abs(horizontalInput);

        if (horizontalInput > 0.01f)
        {
            transform.localScale = Vector3.one;
        }
        else if (horizontalInput < -0.01f)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    private void Jump()
    {
        rb.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
        anim.SetTrigger("jump");
    }

    private void WallSlide()
    {
        if (IsWalled() && !IsGrounded())
        {
            isWallSliding = true;
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, Mathf.Clamp(rb.linearVelocity.y, -wallSlidingSpeed, float.MaxValue));
        }
        else
        {
            isWallSliding = false;
        }
    }

    private void WallJump()
    {
        if (isWallSliding)
        {
            isWallJumping = false;
            wallJumpingCounter = wallJumpingTime;

            CancelInvoke(nameof(StopWallJumping));
        }
        else
        {
            wallJumpingCounter -= Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.Space) && wallJumpingCounter > 0f)
        {
            isWallJumping = true;
            rb.linearVelocity = new Vector2(0, 0);
            rb.AddForce(new Vector2(rb.linearVelocity.x, jumpPower * 1.5f), ForceMode2D.Impulse);
            wallJumpingCounter = 0f;

            Invoke(nameof(StopWallJumping), wallJumpingDuration);
        }
    }
    
    private void StopWallJumping()
    {
        isWallJumping = false;
    }

    private bool IsWalled()
    {
        RaycastHit2D raycast = Physics2D.BoxCast(boxCollider2D.bounds.center, boxCollider2D.bounds.size, 0, new Vector2(transform.localScale.x, 0), 0.1f, wallLayer);
        return raycast.collider != null;
    }

    private bool IsGrounded()
    {
        RaycastHit2D raycast = Physics2D.BoxCast(boxCollider2D.bounds.center, boxCollider2D.bounds.size, 0, Vector2.down, 0.05f, groundLayer);
        return raycast.collider != null;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(boxCollider2D.bounds.center, boxCollider2D.bounds.size);
    }
    #endregion


    #region Skill
    public void UpgradeJump()
    {
        if (moveSpeed > defaultSpeed || jumpPower == defaultJump)
        {
            moveSpeed = defaultSpeed;
            jumpPower += 5f;
        }
    }

    public void UpgradeSpeed()
    {
        if (jumpPower > defaultJump || moveSpeed == defaultSpeed)
        {
            jumpPower = defaultJump;
            moveSpeed += 3f;
        }
    }
    #endregion
}
