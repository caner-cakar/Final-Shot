using UnityEngine;
using UnityEngine.AI;

public class EnemyHealth : MonoBehaviour
{
    public float health;
    RagdollManager ragdollManager;
    [HideInInspector] public bool isDead;
    Animator animator;
    public Terrain terrain;


    void Start()
    {
        animator = GetComponent<Animator>();
        ragdollManager = GetComponent<RagdollManager>();
    }
    

    void LateUpdate()
    {
        float terrainHeight = terrain.SampleHeight(transform.position);
        transform.position = new Vector3(transform.position.x, terrainHeight, transform.position.z);
    }

    public void TakeDamage(float damage)
    {
        if (health > 0)
        {
            health -= damage;
            if (health <= 0) EnemyDeath();
            else
            {
                animator.SetTrigger("Damage");
                Debug.Log("Hit");
            }
        }

    }
    

    void EnemyDeath()
    {
        isDead = true;
        ragdollManager.TriggerRagdoll();
        Debug.Log("Death");
    }
    
}
