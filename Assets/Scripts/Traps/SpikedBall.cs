using UnityEngine;

public class SpikedBall : MonoBehaviour
{
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float moveSpeed;

    private CircleCollider2D circleCollider2D;
    private Rigidbody2D rb;
    private bool attacking;
    private float range = 50f;

    void Awake()
    {
        circleCollider2D = GetComponent<CircleCollider2D>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        Stop();
    }

    void Update()
    {
        if (attacking)
        {
            rb.gravityScale = 3;
        }
        else
        {
            CheckForPlayer();
        }

        transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
        if (IsGrounded())
        {
            moveSpeed += 0.3f * Time.deltaTime;
            rb.linearVelocity = new Vector2(moveSpeed, rb.linearVelocity.y);
        }
    }

    private void CheckForPlayer()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, range, LayerMask.GetMask("Player"));
        Debug.DrawRay(transform.position, Vector2.down * range, Color.red, 1f);

        if (hit.collider != null && !attacking)
        {
            attacking = true;
        }
    }

    private bool IsGrounded()
    {
        RaycastHit2D raycast = Physics2D.CircleCast(circleCollider2D.bounds.center, circleCollider2D.bounds.extents.x, Vector2.down, 0.1f,
        LayerMask.GetMask("Ground"));
        return raycast.collider != null;
    }

    private void Stop()
    {
        rb.gravityScale = 0;
        attacking = false;
    }
}
