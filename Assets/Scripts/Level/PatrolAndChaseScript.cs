using UnityEngine;

public class PatrolAndChaseScript : MonoBehaviour
{
    Transform player;
    Enemy enemyScript;

    public Transform groundDetection;
    public float aggroRange;
    public float moveSpeed;
    public float distance;
    public float attackDistance;


    private bool movingRight = true;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        enemyScript = GetComponent<Enemy>();
    }

    void Update()
    {
        RaycastHit2D groundInfo = Physics2D.Raycast(groundDetection.position, Vector2.down, distance);
        float distToPlayer = Vector2.Distance(transform.position, player.position);

        if (distToPlayer < aggroRange)
        {
            if (distToPlayer < attackDistance)
                attackPlayer();
            else
                chasePlayer();
        }
        else
        {
            if(movingRight == true)
                transform.Translate(Vector2.right * moveSpeed * Time.deltaTime);
            else if (movingRight == false)
                transform.Translate(Vector2.left * moveSpeed * Time.deltaTime);
        }

        if (groundInfo.collider == false)
        {
            if (movingRight == true)
            {
                transform.eulerAngles = new Vector3(0, -180, 0);
                movingRight = false;
            }
            else
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
                movingRight = true;
            }
        }
    }

    private void chasePlayer()
    {
        if (transform.position.x < player.position.x)
        {
            //enemy is to the left of the player so move right
            transform.Translate(Vector2.right * moveSpeed * Time.deltaTime);
            transform.localScale = new Vector2(1, 1);
            movingRight = true;
        }
        else if (transform.position.x > player.position.x)
        {
            //enemy is to the right of the player so move left
            transform.Translate(Vector2.left * moveSpeed * Time.deltaTime);
            transform.localScale = new Vector2(-1, 1);
            movingRight = false;
        }
    }

    private void attackPlayer()
    {
        enemyScript.Attack();
    }
}
