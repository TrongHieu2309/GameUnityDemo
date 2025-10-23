using UnityEngine;

public class SpikedBall : MonoBehaviour
{
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float moveSpeed;

    private CircleCollider2D circleCollider2D;
    private Rigidbody2D rb;

    void Awake()
    {
        circleCollider2D = GetComponent<CircleCollider2D>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
        if (IsGrounded())
        {
            rb.linearVelocity = new Vector2(moveSpeed, rb.linearVelocity.y);
        }
    }

    private bool IsGrounded()
    {
        RaycastHit2D raycast = Physics2D.CircleCast(circleCollider2D.bounds.center, circleCollider2D.bounds.extents.x, Vector2.down, 0.1f,
        LayerMask.GetMask("Ground"));
        return raycast.collider != null;
    }
}
