using UnityEngine;

public class Danger : MonoBehaviour
{
    [SerializeField] private Transform PosDanger;
    [SerializeField] private LayerMask playerLayer;
    private float damageCooldown = 1f;
    private float timer;

    void Update()
    {
        if (DamagePlayer() && Time.time >= timer + damageCooldown)
        {
            Health.instance.TakeDamage(5);
            timer = Time.time;
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
