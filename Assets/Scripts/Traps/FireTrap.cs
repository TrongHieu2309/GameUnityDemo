using System.Collections;
using UnityEngine;

public class FireTrap : MonoBehaviour
{
    [SerializeField] private float damage;

    private float damageDelay = 1;
    private float activeTime = 2;
    private Animator anim;
    private bool isActive = false;
    private float damageCooldown = 1;
    private float timer;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            StartCoroutine(ActivateTrap());
        }
    }

    private IEnumerator ActivateTrap()
    {
        anim.SetTrigger("hit");
        yield return new WaitForSeconds(damageDelay);
        anim.SetTrigger("fireOn");

        isActive = true;
        yield return new WaitForSeconds(activeTime);

        isActive = false;
        anim.SetTrigger("idle");
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (isActive == true && Time.time >= timer + damageCooldown)
            {
                Health heal = collision.GetComponent<Health>();
                if (heal != null)
                {
                    heal.TakeDamage(damage);
                    timer = Time.time;
                }
            }
        }
    }
}