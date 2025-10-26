using UnityEngine;
using UnityEngine.AI;

public class EnemyHealth : MonoBehaviour
{
    public float health;
    [HideInInspector] public bool isDead;
    Animator animator;


    void Start()
    {
        animator = GetComponent<Animator>();
    }
    


    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            gameObject.layer = LayerMask.NameToLayer("DeadEnemy");
            GetComponentInChildren<Collider>().enabled = false;
            animator.SetTrigger("die");

            isDead = true;
            Destroy(gameObject, 5f);
        }
        else
        {
            animator.SetTrigger("damage");
            isDead = false;
        }

    }    
}
