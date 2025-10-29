using System.Runtime.InteropServices.WindowsRuntime;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class Trampoline : MonoBehaviour
{
    [SerializeField] private float bouncePower;
    [SerializeField] private LayerMask playerLayer;

    private Animator anim;
    private Rigidbody2D playerRb;
    private BoxCollider2D boxCollider2D;

    void Awake()
    {
        anim = GetComponent<Animator>();
        boxCollider2D = GetComponent<BoxCollider2D>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerRb = collision.gameObject.GetComponent<Rigidbody2D>();
        }
    }

    void Update()
    {
        if (OnTrampoline())
        {
            anim.SetTrigger("activate");

            if (playerRb != null)
            {
                playerRb.linearVelocity = Vector2.zero;
                playerRb.AddForce(Vector2.up * bouncePower, ForceMode2D.Impulse);

                Animator playerAnim = playerRb.GetComponent<Animator>();
                playerAnim.SetTrigger("jump");
            }
        }
    }

    private bool OnTrampoline()
    {
        Vector2 catsSize = boxCollider2D.bounds.size;
        catsSize.x *= 0.8f;
        catsSize.y *= 1f;
        RaycastHit2D raycast = Physics2D.BoxCast(boxCollider2D.bounds.center, catsSize, 0, Vector2.up, 0.5f, playerLayer);
        return raycast.collider != null;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Vector2 catsSize = boxCollider2D.bounds.size;
        catsSize.x *= 0.8f;
        catsSize.y *= 1f;
        Gizmos.DrawWireCube(boxCollider2D.bounds.center, catsSize);
    }
}