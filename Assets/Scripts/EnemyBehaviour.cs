using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{

    private Rigidbody2D rb;
    private Transform currentPoint;
    [SerializeField] private float speed;
    public GameObject pointA;
    public GameObject pointB;
    private float counter = .9f;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentPoint = pointB.transform;
        transform.position = currentPoint.position;
    }


    void Update()
    {
        if (counter<=.9f)
        {
            counter += Time.deltaTime;
            rb.linearVelocity = Vector2.zero;
            return;
        }
        Vector2 point = currentPoint.position - transform.position;
        if (currentPoint == pointB.transform)
        {
            rb.linearVelocity = new Vector2(speed, 0);
        }
        else
        {
            rb.linearVelocity = new Vector2(-speed, 0);
        }

        if(Vector2.Distance(currentPoint.position, transform.position) < .5f && currentPoint == pointB.transform)
        {
            currentPoint = pointA.transform;
        }
        if (Vector2.Distance(currentPoint.position, transform.position) < .5f && currentPoint == pointA.transform)
        {
            currentPoint = pointB.transform;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            counter = 0;
        }
    }
}
