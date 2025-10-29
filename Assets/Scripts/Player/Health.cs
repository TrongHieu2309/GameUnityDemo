using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public static Health instance;

    [Header("Health")]
    [SerializeField] private float maxHealth;
    [SerializeField] private Image hpBar;
    [SerializeField] private float currentHealth;

    private Animator anim;
    private float defaultMaxHealth;
    private bool isDamage = true;
    private float hurtCooldown = 2;
    private SpriteRenderer spriteRenderer;

    void Awake()
    {
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        instance = this;
    }

    void Start()
    {
        defaultMaxHealth = maxHealth;
        currentHealth = defaultMaxHealth;
        UpdateHpBar();
    }

    public void TakeDamage(float damage)
    {
        if (!isDamage) return;

        currentHealth = Mathf.Clamp(currentHealth - damage, 0f, maxHealth);
        if (currentHealth > 0)
        {
            anim.SetTrigger("hurt");
        }
        else
        {
            if (GameManager.instance.HasRespawnPoint() && GameManager.instance != null)
            {
                GameManager.instance.Respawn();
                currentHealth = maxHealth;
            }
            else
            {
                Debug.Log("Game Over");
                Time.timeScale = 0f;
            }
        }
        UpdateHpBar();
        StartCoroutine(Hurt());
    }

    private IEnumerator Hurt()
    {
        isDamage = false;
        float elapsed = 0;

        while (elapsed < hurtCooldown)
        {
            Color c = spriteRenderer.color;
            c.a = 0.3f;
            spriteRenderer.color = c;
            yield return new WaitForSeconds(0.2f);

            c.a = 1;
            spriteRenderer.color = c;
            yield return new WaitForSeconds(0.2f);

            elapsed += 0.4f;
        }

        spriteRenderer.color = new Color(1, 1, 1, 1);
        isDamage = true;
    }

    public void AdvancedHealing(float healthValue)
    {
        if (currentHealth < maxHealth)
        {
            currentHealth = Mathf.Clamp(currentHealth + healthValue, 5f, maxHealth);
        }
        UpdateHpBar();
    }

    public void StandardHealing(float healthValue)
    {
        if (currentHealth < maxHealth)
        {
            currentHealth = Mathf.Clamp(currentHealth + healthValue, 5f, maxHealth);
        }
        UpdateHpBar();
    }

    public void UpgradeHp()
    {
        if (maxHealth == defaultMaxHealth)
        {
            maxHealth += 10f;
            currentHealth = maxHealth;
            hpBar.color = Color.magenta;
            UpdateHpBar();
        }
    }

    private void UpdateHpBar()
    {
        if (hpBar != null)
        {
            hpBar.fillAmount = currentHealth / maxHealth;
        }
    }
}
