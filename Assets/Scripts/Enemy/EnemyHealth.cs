using UnityEngine;
using UnityEngine.AI;

public class EnemyHealth : MonoBehaviour
{
    public float health;
    [HideInInspector] public bool isDead;
    Animator animator;
    public Terrain terrain;


    void Start()
    {
        animator = GetComponent<Animator>();
    }
    

    void LateUpdate()
    {
        float terrainHeight = terrain.SampleHeight(transform.position);
        transform.position = new Vector3(transform.position.x, terrainHeight, transform.position.z);
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            animator.SetTrigger("die");
            GetComponent<CapsuleCollider>().enabled = false;
            isDead = true;
        }
        else
        {
            animator.SetTrigger("damage");
            isDead = false;
        }

    }    
}
