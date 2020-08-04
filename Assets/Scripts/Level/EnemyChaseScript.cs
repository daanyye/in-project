using UnityEngine;

public class EnemyChaseScript : MonoBehaviour
{
    [SerializeField]
    Transform player;
    [SerializeField]
    float aggroRange = 0f;
    [SerializeField]
    float moveSpeed = 0f;
    [SerializeField]
    float attackDistance = 0f;

    Rigidbody2D rb2d;
    Enemy enemyScript;

    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        enemyScript = GetComponent<Enemy>();
    }

    private void Update()
    {
        if (PlayerPrefs.GetString("PlayerDead") == "false")
        {
            float distToPlayer = Vector2.Distance(transform.position, player.position);

            if (distToPlayer < attackDistance)
                attackPlayer();
            else if (distToPlayer < aggroRange)
                chasePlayer();
            else
                stopChasingPlayer();
        }
        else
            stopChasingPlayer();
    }

    private void attackPlayer()
    {
        if (transform.position.x < player.position.x)
        {
            enemyScript.Attack();
            transform.localScale = new Vector2(1.2f, 1.2f);
            enemyScript.animator.SetFloat("Speed", 0);
        }
        else if (transform.position.x > player.position.x)
        {
            enemyScript.Attack();
            transform.localScale = new Vector2(-1.2f, 1.2f);
            enemyScript.animator.SetFloat("Speed", 0);
        }

    }

    private void chasePlayer()
    {
        if (transform.position.x < player.position.x)
        {
            //enemy is to the left of the player so move right
            rb2d.velocity = new Vector2(moveSpeed, 0);
            transform.localScale = new Vector2(1.2f, 1.2f);
            enemyScript.animator.SetFloat("Speed", moveSpeed);
        }
        else if (transform.position.x > player.position.x)
        {
            //enemy is to the right of the player so move left
            rb2d.velocity = new Vector2(-moveSpeed, 0);
            transform.localScale = new Vector2(-1.2f, 1.2f);
            enemyScript.animator.SetFloat("Speed", moveSpeed);
        }
    }

    private void stopChasingPlayer()
    {
        rb2d.velocity = new Vector2(0, 0);
        enemyScript.animator.SetFloat("Speed", 0);
    }
}
