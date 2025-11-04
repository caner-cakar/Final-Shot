using UnityEngine;
using UnityEngine.AI;

public class EnemyHealth : MonoBehaviour
{
    public float health;
    [HideInInspector] public bool isDead;
    Animator animator;
    NavMeshAgent agent;


    void Start()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
    }
    


    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            isDead = true;
            gameObject.layer = LayerMask.NameToLayer("DeadEnemy");
            agent.enabled = false;
            GetComponentInChildren<Collider>().enabled = false;
            animator.ResetTrigger("damage");
            animator.ResetTrigger("attack");
            animator.SetBool("isChasing", false);
            animator.SetBool("isAttacking", false);
            animator.SetTrigger("die");
            Destroy(gameObject, 5f);
        }
        else
        {
            animator.SetTrigger("damage");
            isDead = false;
        }

    }    
}
