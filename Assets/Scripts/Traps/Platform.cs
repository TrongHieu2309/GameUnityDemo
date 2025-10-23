using System.Diagnostics;
using UnityEngine;

public class Platform : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private GameObject platform;
    [SerializeField] private Transform posA;
    [SerializeField] private Transform posB;

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
}
