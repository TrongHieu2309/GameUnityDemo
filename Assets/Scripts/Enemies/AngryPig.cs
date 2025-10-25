using UnityEngine;

public class AngryPig : MonoBehaviour
{
    [Header ("Settings")]
    [SerializeField] private float walkSpeed;
    [SerializeField] private float runSpeed;
    [SerializeField] private Transform PosA;
    [SerializeField] private Transform PosB;

    [Header("Pig Dead")]
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private Transform PosDead;
    [SerializeField] private Rigidbody2D playerRb;

    private Vector3 target;
    private Animator anim;
    private Rigidbody2D rb;
    bool isWalking;
    bool isRunning;
    float rest = 2f;
    float timer;
    bool checkPlayer;
    bool isHit = false;
    bool checkDamage;
    bool takeDamage;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Start()
    {
        target = PosA.position;
    }

    void Update()
    {
        checkPlayer = Physics2D.OverlapBox(PosDead.position, new Vector2(1f, 0.2f), 0, playerLayer);
        checkDamage = Physics2D.OverlapBox(transform.position, new Vector2(1.3f, 0.8f), 0, playerLayer);
        PigDead();
        Hit();
        anim.SetBool("walking", isWalking);
        anim.SetBool("running", isRunning);
    }

    void FixedUpdate()
    {
        if (checkDamage) PigTakeDamage();
        PigWalking();
        if (isRunning) PigRunning();
    }

    private void PigWalking()
    {
        if (Vector2.Distance(transform.position, target) >= 0.5f)
        {
            isWalking = true;
            float direction = Mathf.Sign(target.x - transform.position.x);
            rb.linearVelocity = new Vector2(direction * walkSpeed, rb.linearVelocity.y);
        }
        else
        {
            rb.linearVelocity = Vector2.zero;
            isWalking = false;
            timer += Time.fixedDeltaTime;

            if (timer >= rest)
            {
                target = (target == PosA.position) ? PosB.position : PosA.position;
                transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
                timer = 0f;
            }
        }
    }

    private void PigRunning()
    {
        float direction = Mathf.Sign(target.x - transform.position.x);
        rb.linearVelocity = new Vector2(direction * runSpeed, rb.linearVelocity.y);

        if (Vector2.Distance(transform.position, target) <= 0.5f)
        {
            target = (target == PosA.position) ? PosB.position : PosA.position;

            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        }  
    }

    private void Hit()
    {
        if (checkPlayer && playerRb != null)
        {
            playerRb.linearVelocity = new Vector2(playerRb.linearVelocity.x, 0);
            playerRb.AddForce(Vector2.up * 10f, ForceMode2D.Impulse);
            isHit = true;
            if (isHit == true)
            {
                rb.linearVelocity = Vector2.zero;
                anim.SetTrigger("hit");
            }
        }
    }

    public void Running()
    {
        isHit = false;
        isRunning = true;
    }

    private void PigDead()
    {
        if (isRunning && checkPlayer)
        {
            Destroy(gameObject);
        }
    }

    private void PigTakeDamage()
    {
        Health.instance.TakeDamage(5);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerRb = collision.gameObject.GetComponent<Rigidbody2D>();
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(PosDead.position, new Vector2(1f, 0.2f));
        Gizmos.DrawWireCube(transform.position, new Vector2(1.3f, 0.8f));
    }
}

/******************************************************************************************************************/
    // void OnCollisionEnter2D(Collision2D collision)
    // {
    //     if (collision.gameObject.CompareTag("Player"))
    //     {
    //         playerRb = collision.gameObject.GetComponent<Rigidbody2D>();
    //     }
    // }

    // void Update()
    // {
    //     PigWalking();
    //     if (Vector2.Distance(transform.position, target) < 0.1f)
    //     {
    //         target = (target == PosA.position) ? PosB.position : PosA.position;
    //     }
    // }

    // private void PigWalking()
    // {
    //     transform.position = Vector2.MoveTowards(transform.position, target, walkSpeed * Time.deltaTime);

    //     float direction = Mathf.Sign(target.x - transform.position.x);

    //     if (isFacingLeftDefault)
    //     {
    //         transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x) * direction, transform.localScale.y, transform.localScale.z);
    //     }

    //     else
    //     {
    //         transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x) * direction, transform.localScale.y, transform.localScale.z);
    //     }
    // }
// }
/******************************************************************************************************************/