using UnityEngine;

public class PlayerCollider : MonoBehaviour
{
    private GameManager gameManager;
    void Awake()
    {
        gameManager = FindAnyObjectByType<GameManager>();
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("SpikedBall"))
        {
            Health.instance.TakeDamage(20);
        }
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Cup"))
        {
            UIManager.instance.WinGame();
        }

        if (collision.CompareTag("Trap"))
        {
            Health.instance.TakeDamage(5);
        }

        if (collision.CompareTag("Apple"))
        {
            FruitsController fruits = collision.GetComponent<FruitsController>();
            if (fruits != null)
            {
                fruits.TakeFruits();
                PlayerController.instance.UpgradeJump();
            }
        }

        if (collision.CompareTag("Bananas"))
        {
            FruitsController fruits = collision.GetComponent<FruitsController>();
            if (fruits != null)
            {
                fruits.TakeFruits();
                PlayerController.instance.UpgradeSpeed();
            }
        }

        if (collision.CompareTag("Cherries"))
        {
            FruitsController fruits = collision.GetComponent<FruitsController>();
            if (fruits != null)
            {
                fruits.TakeFruits();
                Health.instance.AdvancedHealing(10);
            }
        }

        if (collision.CompareTag("Strawberry"))
        {
            FruitsController fruits = collision.GetComponent<FruitsController>();
            if (fruits != null)
            {
                fruits.TakeFruits();
                Health.instance.UpgradeHp();
            }
        }

        if (collision.CompareTag("Melon"))
        {
            FruitsController fruits = collision.GetComponent<FruitsController>();
            if (fruits != null)
            {
                fruits.TakeFruits();
                Health.instance.StandardHealing(5);
            }
        }

        if (collision.CompareTag("Fruits"))
        {
            FruitsController fruits = collision.GetComponent<FruitsController>();
            if (fruits != null)
            {
                fruits.TakeFruits();
            }
            gameManager.AddScore(1);
        }

        if (collision.CompareTag("Checkpoint"))
        {
            GameManager.instance.isRespawn = false;
            Animator anim = collision.GetComponent<Animator>();
            if (anim != null)
            {
                anim.SetTrigger("appear");
            }
            GameManager.instance.SetRespawnPoint(collision.transform);
        }
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Checkpoint"))
        {
            GameManager.instance.isRespawn = false;
        }
    }
}
