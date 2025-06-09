using UnityEngine;
using System.Collections;
public class EnemyBehaviour : MonoBehaviour
{

    private Rigidbody2D rb;
    private Transform currentPoint;
    [SerializeField] private float speed;
    public GameObject pointA;
    public GameObject pointB;
    private float counter = .9f;
    [SerializeField] private int health = 5;
    [SerializeField] private float visionRange = 5f;
    [SerializeField] LayerMask playerLayer;
    private Vector2 moveDirection;
    private bool detectedPlayer = false;
    [SerializeField] Transform player;
    private Animator anim;

    private FlashScript flash;
    
   
    public void loseHealth(int amount)
    {
        this.health -= amount;
        flash.Flash();
        if (this.health <= 0)
            Destroy(gameObject);
       
    }

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        currentPoint = pointB.transform;
        rb.position = currentPoint.position;
        anim = GetComponent<Animator>();
        flash = GetComponent<FlashScript>();
        
    }

    void sawPlayer()
    {
        if (detectedPlayer)
            return;

        moveDirection = (currentPoint.position - transform.position).normalized;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, moveDirection, visionRange, playerLayer);

        if (hit.collider != null && hit.collider.CompareTag("Player"))
        {
            Debug.Log("Player detected!");
            this.detectedPlayer = true;
            counter = .6f;
        }
    }
    private void Update()
    {
        if (counter <= .9f)
        {
            counter += Time.deltaTime;
            rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
            anim.SetBool("moving", false);
            return;
        }
    }
    void FixedUpdate()
    {
        if (counter <= .9f)
            return;
        if (pointA.transform.position.x > player.position.x || player.position.x > pointB.transform.position.x)
        {
            detectedPlayer = false;
        }
            if (!detectedPlayer)
        {
            Debug.Log("haha");
            patrol();
        }else 
        {   

            chasePlayer();
        }
        if (rb.linearVelocity.x < -0.01f)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        if(rb.linearVelocity.x > .01f)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        sawPlayer();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            counter = 0;
        }

    }
    private void patrol()
    {
        anim.SetBool("moving", true);
        Vector2 point = currentPoint.position - transform.position;
        if (currentPoint == pointB.transform)
        {
            rb.linearVelocity = new Vector2(speed, rb.linearVelocity.y);
        }
        else
        {
            rb.linearVelocity = new Vector2(-speed, rb.linearVelocity.y);
        }

        if (Vector2.Distance(currentPoint.position, transform.position) < .5f)
        {
            currentPoint = (currentPoint == pointB.transform) ? pointA.transform : pointB.transform;
        }
    }
   
    private void chasePlayer()
    {
        if (player == null) return;
        anim.SetBool("moving", true);

        Vector2 direction = (player.position - transform.position).normalized;
        direction = new Vector2(direction.x, 0);
        rb.linearVelocity = new Vector2(direction.x * speed,rb.linearVelocity.y);
    }
    private void OnDrawGizmosSelected()
    {
        if (Application.isPlaying)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, (Vector2)transform.position + moveDirection * visionRange);
        }
    }
}
