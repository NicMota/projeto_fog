using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Rendering;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float spd;
    private Camera mainCamera;
    private Vector3 mousePos;
    private bool hit;
    private BoxCollider2D bc;
    private float lifeTime;
    private Vector3 direction;
  
    private void Awake()
    {
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        bc = GetComponent<BoxCollider2D>();
       
    }
    private void Update()
    {
        if (hit) return;

        transform.position += direction * spd * Time.deltaTime;

        lifeTime += Time.deltaTime;
        if (lifeTime > 5) gameObject.SetActive(false);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag != "Player")
        {
            hit = true;
            bc.enabled = false;
            Deactivate();
        }
        if (collision.tag == "Enemy")
        {
            Destroy(collision.gameObject);
        }
        
    }
    public void setDirection()
    {
        lifeTime = 0f;
        hit = false;
        bc.enabled = true;
        mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;
        direction = (mousePos - transform.position).normalized;
       
       
        gameObject.SetActive(true);
       
        float localScaleX = transform.localScale.x;
      
    }
    private void Deactivate()
    {
        gameObject.SetActive(false);
    }
}
