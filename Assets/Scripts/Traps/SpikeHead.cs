using UnityEngine;

public class SpikeHead : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float range;
    [SerializeField] private float checkDelay;
    [SerializeField] private LayerMask playerLayer;

    private Vector3[] directions = new Vector3[4];
    private float checkTimer;
    private Vector3 destination;
    private bool attacking;
    private Animator anim;

    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    void OnEnable()
    {
        Stop();
    }

    void Update()
    {
        if (attacking)
        {
            transform.Translate(destination * speed * Time.deltaTime);
        }
        else
        {
            checkTimer += Time.deltaTime;
            if (checkTimer > checkDelay)
            {
                CheckForPlayer();
            }
        }
    }

    private void CheckForPlayer()
    {
        CalculateDirections();

        // check spikehead sees player in all 4 directions
        for (int i = 0; i < directions.Length; i++)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, directions[i], range, playerLayer);
            Debug.DrawRay(transform.position, directions[i], Color.red, 1f);

            if (hit.collider != null && !attacking)
            {
                if (i == 0)
                {
                    attacking = true;
                    anim.SetBool("rightHit", true);
                    destination = directions[i].normalized;
                    checkTimer = 0;
                }
                if (i == 1)
                {
                    attacking = true;
                    anim.SetBool("leftHit", true);
                    destination = directions[i].normalized;
                    checkTimer = 0;
                }
                if (i == 2)
                {
                    attacking = true;
                    anim.SetBool("topHit", true);
                    destination = directions[i].normalized;
                    checkTimer = 0;
                }
                if (i == 3)
                {
                    attacking = true;
                    anim.SetBool("bottomHit", true);
                    destination = directions[i].normalized;
                    checkTimer = 0;
                }
                return;
            }
        }
    }

    private void CalculateDirections()
    {
        directions[0] = transform.right * range;
        directions[1] = -transform.right * range;
        directions[2] = transform.up * range;
        directions[3] = -transform.up * range;
    }

    private void Stop()
    {
        destination = transform.position;
        attacking = false;

        anim.SetBool("rightHit", false);
        anim.SetBool("leftHit", false);
        anim.SetBool("topHit", false);
        anim.SetBool("bottomHit", false);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        attacking = false;
        Stop();
    }
}
