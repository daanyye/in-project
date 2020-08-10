using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public float attackRate = 2f;
    float nextAttackTime = 0f;

    public Animator animator;

    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask enemyLayer;
    public int attackDamage;

    private void Start()
    {
        attackDamage = PlayerPrefs.GetInt("attackDamagePoints");
    }

    void Update()
    {
        if (Time.time >= nextAttackTime)
        {
            if (Input.GetKeyDown(KeyCode.F) && animator.GetBool("Ground"))
            {
                attack();
                nextAttackTime = Time.time + 1f / attackRate;
            }
        }
    }

    private void attack()
    {
        FindObjectOfType<GameSoundEffectManager>().PlaySound("playerSwordSwing");
        animator.SetTrigger("Attack");

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayer);

        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponentInParent<Enemy>().TakeDamage(attackDamage);
        }
    }

    public void takeDamage(int incomingDamage)
    {
        float damageReduction = incomingDamage * (PlayerPrefs.GetFloat("damageReductionPercentage") / 100f);
        GetComponent<Player>().currentHealth -= Mathf.Floor(incomingDamage - damageReduction);
        animator.SetTrigger("GotHit");

        if (GetComponent<Player>().currentHealth <= 0)
            die();
    }

    public void die()
    {
        GetComponent<Player>().currentHealth = 0;
        if (animator.GetBool("Ground") == false)
        {
            animator.SetBool("Ground", true);
            disableComponentsAndSetDeadState();
        }
        else
            disableComponentsAndSetDeadState();
    }

    private void disableComponentsAndSetDeadState()
    {
        setDeadState();
        disableComponents();
        FindObjectOfType<LevelUIScript>().showDeathScreen();
        disableBoxCollidersAndGameObject();
    }

    private void setDeadState()
    {
        PlayerPrefs.SetString("PlayerDead", "true");
        animator.SetBool("Dead", true);
    }

    private void disableComponents()
    {
        GetComponent<Platformer2DUserControl>().enabled = false;
        GetComponent<PlatformerCharacter2D>().enabled = false;
        GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
    }

    private void disableBoxCollidersAndGameObject()
    {
        BoxCollider2D[] boxColliders = GetComponentsInChildren<BoxCollider2D>();

        foreach (BoxCollider2D collider in boxColliders)
        {
            collider.enabled = false;
        }
        this.enabled = false;
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
