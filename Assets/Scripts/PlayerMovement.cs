
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField] private float spd = 10;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] private float jumpForce = 7;
    [SerializeField] private float jumpTime = 0.35f;
    private Rigidbody2D rb;
    private Animator anim;
    private bool grounded;
    private BoxCollider2D bc;
    private float jumpTimeCounter;
    private float horizontalInput;
    private Health playerHealth;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        bc = GetComponent<BoxCollider2D>();
        playerHealth = GetComponent<Health>();
    }
    private void Update()
    {   if(!playerHealth.IsKnockbackActive())
        {
            horizontalInput = Input.GetAxis("Horizontal");
            rb.linearVelocity = new Vector2(horizontalInput * spd, rb.linearVelocity.y);

            //flips player
            if (horizontalInput > 0.01f)
            {
                transform.localScale = Vector3.one;

            }
            else if (horizontalInput < -0.01f)
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                jump();
            }
            if (Input.GetKey(KeyCode.Space) && !isGrounded())
            {
                if (jumpTimeCounter > 0)
                {
                    rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
                    jumpTimeCounter -= Time.deltaTime;
                }

            }
            if (Input.GetKeyUp("space"))
                jumpTimeCounter = 0;

            anim.SetBool("walk", horizontalInput != 0);
            anim.SetBool("grounded", isGrounded());
        }
       

    }
    private void jump()
    {
        if (isGrounded())
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            anim.SetTrigger("jump");
            jumpTimeCounter = jumpTime;
        }  
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        
    }
    private bool isGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(bc.bounds.center,bc.bounds.size,0,Vector2.down,.1f,groundLayer);
        return raycastHit.collider != null;
    }
    public bool canAttack()
    {
        return isGrounded() && horizontalInput == 0;
    }
}
