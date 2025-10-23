using UnityEngine;

public class Platform : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private GameObject platform;
    [SerializeField] private Transform posA;
    [SerializeField] private Transform posB;
    [Header ("collision")]
    [SerializeField] private Transform PosDanger;
    [SerializeField] private LayerMask playerLayer;
    private float damageCooldown = 1f;
    private float timer;

    private Vector3 target;

    void Start()
    {
        target = posA.position;
    }

    void Update()
    {
        platform.transform.position = Vector3.MoveTowards(platform.transform.position, target, speed * Time.deltaTime);

        if (Vector3.Distance(platform.transform.position, target) < 0.1f)
        {
            if (target == posA.position)
            {
                target = posB.position;
            }
            else
            {
                target = posA.position;
            }
        }

        if (DamagePlayer() && Time.time >= timer + damageCooldown)
        {
            Health.instance.TakeDamage(5);
            timer = Time.time;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.transform.SetParent(transform);
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.transform.SetParent(null);
        }
    }

    private bool DamagePlayer()
    {
        RaycastHit2D raycast = Physics2D.Raycast(PosDanger.position, Vector2.down, 0.1f, playerLayer);
        return raycast.collider != null;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(PosDanger.position, PosDanger.position + Vector3.down * 0.1f);
    }
}
