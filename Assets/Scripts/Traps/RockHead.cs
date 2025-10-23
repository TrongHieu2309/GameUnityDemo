using System;
using Unity.VisualScripting;
using UnityEngine;

public class RockHead : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float rangeDown;
    [SerializeField] private float rangeUp;
    [SerializeField] private float checkDelay;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private GameObject spikes;
    [SerializeField] private Transform PosDanger;

    private Vector3[] directions = new Vector3[2];
    private float checkTimer;
    private Vector3 destination;
    private bool attacking;
    private Animator anim;
    private BoxCollider2D boxCollider2D;
    private Vector2 boxSize = new Vector2(1, 1);
    private Rigidbody2D rb;

    void Awake()
    {
        anim = GetComponent<Animator>();
        boxCollider2D = GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();
    }

    void OnEnable()
    {
        spikes.SetActive(false);
        Stop();
    }

    void Update()
    {
        TakeDamagePlayer();
        if (attacking)
        {
            RaycastHit2D hit = Physics2D.BoxCast(
                boxCollider2D.bounds.center,
                boxCollider2D.bounds.size,
                0,
                Vector2.down,
                0.1f,
                LayerMask.GetMask("Ground")
            );

            if (hit.collider == null)
            {
                transform.Translate(destination * speed * Time.deltaTime);
            }
            else
            {
                Stop();
            }
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
        CalculateDirection();

        for (int i = 0; i < directions.Length; i++)
        {
            if (i == 0)
            {
                RaycastHit2D raycast = Physics2D.Raycast(transform.position, directions[i], rangeUp, playerLayer);
                Debug.DrawRay(transform.position, directions[i], Color.red, 1f);
                if (raycast.collider != null)
                {
                    spikes.SetActive(true);
                }
            }
            if (i == 1)
            {
                RaycastHit2D raycast = Physics2D.Raycast(transform.position, directions[i], rangeDown, playerLayer);
                Debug.DrawRay(transform.position, directions[i], Color.red, 1f);
                if (raycast.collider != null)
                {
                    attacking = true;
                    checkTimer = 0;
                    destination = directions[i].normalized;
                    anim.SetBool("bottomHit", true);
                }
            }
        }
    }
    
    private void CalculateDirection()
    {
        directions[0] = transform.up * rangeUp;
        directions[1] = -transform.up * rangeDown;
    }

    private void Stop()
    {
        attacking = false;
        rb.linearVelocity = Vector2.zero;

        anim.SetBool("topHit", false);
        anim.SetBool("bottomHit", false);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer != LayerMask.NameToLayer("Player"))
        {
            Stop();
        }
    }

    private bool CheckCollider()
    {
        RaycastHit2D hit = Physics2D.BoxCast(PosDanger.position, boxSize, 0, Vector2.down, 0.02f, playerLayer);
        return hit.collider != null;
    }

    private void TakeDamagePlayer()
    {
        if (CheckCollider())
        {
            Health.instance.TakeDamage(5);
        }
    }
}
