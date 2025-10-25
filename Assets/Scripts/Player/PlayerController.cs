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
    [SerializeField] private Transform wallCheck;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private Vector2 wallJumpForce;

    private Rigidbody2D rb;
    private float horizontalInput;
    private Animator anim;
    private float isRunning;
    private float jumpCount = 0;
    private bool jumpPressed;
    private bool fall;
    private float defaultSpeed;
    private float defaultJump;

    // Wall Jumping
    private bool isWallSliding;
    private bool isWallJumping;
    private float wallJumpingDuration = 0.1f;
    private bool isSliding;


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
        Jump();
        WallJump();

        anim.SetBool("onWall", isWallSliding);
        anim.SetBool("fall", fall);
        anim.SetFloat("run01", isRunning);
        anim.SetBool("grounded", IsGrounded());
        anim.SetBool("onWall", isSliding);
    }

    void FixedUpdate()
    {
        if (jumpPressed)
        {
            rb.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            anim.SetTrigger("jump");
            jumpPressed = false;
        }
        if (isWallJumping)
        {
            rb.linearVelocity = new Vector2(-horizontalInput * wallJumpForce.x, wallJumpForce.y);
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
    }

    private void WallJump()
    {
        isSliding = (IsWalled() && !IsGrounded() && horizontalInput != 0) ? true : false;
        if (isSliding)
        {
            rb.linearVelocity = new Vector2(horizontalInput, -1f);
            if (Input.GetKeyDown(KeyCode.Space))
            {
                isWallJumping = true;
                Invoke(nameof(StopWallJumping), wallJumpingDuration);
            }
        }
    }
    
    private void StopWallJumping()
    {
        isWallJumping = false;
    }

    private bool IsWalled()
    {
        RaycastHit2D raycast = Physics2D.Raycast(wallCheck.position, new Vector2(horizontalInput, 0), 0.05f, wallLayer);
        return raycast.collider != null;
    }

    private bool IsGrounded()
    {
        RaycastHit2D raycast = Physics2D.Raycast(groundCheck.position, Vector2.down, 0.05f, groundLayer);
        return raycast.collider != null;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(groundCheck.position, groundCheck.position + Vector3.down * 0.05f);
        Gizmos.DrawLine(wallCheck.position, wallCheck.position + new Vector3(horizontalInput, 0, 0) * 0.05f);
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
