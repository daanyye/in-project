using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    [Header("Health Settings")]
    public float maxHealth = 100f;

    [Header("Animator")]
    public Animator animator;

    [Header("Blood Effect")]
    public GameObject bloodEffect;
    public GameObject floatingPoint;

    [Header("Healthbar")]
    public Image healthBar;
    public Image healthBarBackground;

    [Header("Attack Settings")]
    public int attackDamage = 0;
    public float attackRate = 2f;
    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask playerLayer;

    float currentHealth;
    float nextAttackTime = 0f;
    private void Start()
    {
        currentHealth = maxHealth;
    }

    private void Update()
    {
        updateHealthBarAndFillOrigin();
    }

    private void updateHealthBarAndFillOrigin()
    {
        healthBar.fillAmount = currentHealth / maxHealth;
        if (transform.localScale.x < 0)
            healthBar.fillOrigin = 1;
        else
            healthBar.fillOrigin = 0;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        instantiateBloodEffect(damage);
        FindObjectOfType<GameSoundEffectManager>().PlaySound("enemyHit");
        animator.SetTrigger("GotHit");

        if (currentHealth <= 0)
            Die();
    }

    private void instantiateBloodEffect(int damage)
    {
        GameObject damageTaken = Instantiate(floatingPoint, transform.position, Quaternion.identity);
        damageTaken.transform.GetChild(0).GetComponent<TextMesh>().text = "-" + damage;
        Instantiate(bloodEffect, transform.position, Quaternion.identity);
    }

    public void Die()
    {
        currentHealth = 0;
        disableEnemyHealthBar();
        animator.SetBool("Dead", true);
        GetComponent<EnemyChaseScript>().enabled = false;
        disableObjectAndColliders();
    }

    private void disableEnemyHealthBar()
    {
        healthBar.enabled = false;
        healthBarBackground.enabled = false;
    }

    private void disableObjectAndColliders()
    {
        BoxCollider2D[] colliders = GetComponentsInChildren<BoxCollider2D>();
        foreach (BoxCollider2D collider in colliders)
        {
            collider.enabled = false;
        }

        this.enabled = false;
    }

    public void Attack()
    {
        if (Time.time >= nextAttackTime)
        {
            animator.SetTrigger("Attack");

            Collider2D[] hitPlayers = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, playerLayer);

            if (hitPlayers == null)
                return;
            else
            {
                foreach (Collider2D players in hitPlayers)
                {
                    players.GetComponentInParent<PlayerCombat>().takeDamage(attackDamage);
                }
                nextAttackTime = Time.time + 1f / attackRate;
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
