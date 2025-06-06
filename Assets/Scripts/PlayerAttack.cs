using UnityEngine;

public class PlayerAttack : MonoBehaviour
{

    [SerializeField] private float attackCc;
    [SerializeField] private Transform fbPoint;
    [SerializeField] private GameObject[] fireballs;
    private Animator anim;
    private PlayerMovement pm;
    private float ccTimer = Mathf.Infinity;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        pm = GetComponent<PlayerMovement>();
    }
    private void Update()
    {
        if(Input.GetMouseButton(0) && ccTimer > attackCc && pm.canAttack())
        {
            attack();
        }
        ccTimer += Time.deltaTime;
    }
    private void attack()
    {
        anim.SetTrigger("attack");
        ccTimer = 0;

        fireballs[findFb()].transform.position = fbPoint.position;
        fireballs[findFb()].GetComponent<Projectile>().setDirection();
    }
    private int findFb()
    {
        for (int i = 0; i < fireballs.Length; i++)
        {
            if (!fireballs[i].activeInHierarchy)
                return i;
        }
        return 0;
    }


}
