using UnityEngine;

public class Health : MonoBehaviour
{
    public int health;

    [SerializeField] private int maxHealth = 5;
    private Rigidbody2D rb;
    private bool knockbackActive = false;
    [SerializeField] private float knockbackDuration = 0.3f;
    [SerializeField] private float knockbackForce = 20f;

    void Start()
    {
        health = maxHealth;
        
        rb = GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            Debug.LogError("Rigidbody2D not found on " + gameObject.name);
        }
    }
    public bool IsKnockbackActive()
    {
        return knockbackActive;
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Enemy collision detected");

            Vector2 knockbackDir = (transform.position - collision.transform.position).normalized;
            knockbackDir += new Vector2(0f, .3f);
               
            rb.AddForce(knockbackDir * knockbackForce, ForceMode2D.Impulse);
            knockbackActive = true;

            Debug.Log("Knockback force applied: " + knockbackDir * knockbackForce);
            Invoke("EndKnockback", knockbackDuration);
            health -= 1;
            Debug.Log("Health: " + health);

            if (health <= 0)
            {
                Destroy(gameObject);
            }
        }
    }

    private void EndKnockback()
    {
        knockbackActive = false;
    }
}