using UnityEngine;

public class Saw : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Transform posA;
    [SerializeField] private Transform posB;

    private Vector3 target;

    void Start()
    {
        target = posA.position;
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
        
        if (Vector3.Distance(transform.position, target) < 0.1f)
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
}
