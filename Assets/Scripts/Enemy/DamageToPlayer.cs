using System.Collections;
using UnityEngine;

public class DamageToPlayer : MonoBehaviour
{
    public float damage = 20f; // Vurma başına hasar
    public float attackCooldown = 2f; // Hasar verme süresi aralığı (1 saniye)
    [HideInInspector] public bool canDamage = true;
    Animator animator;

    void Start()
    {
        animator = GetComponentInParent<Animator>();
    }

    void OnTriggerStay(Collider other)
    {
        if(other.CompareTag("Player") && canDamage && animator.GetBool("isAttacking"))
        {
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
            if(playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
                Debug.Log("Hit to player");
                StartCoroutine(DamageCooldown());
            }
        }
    }

    private IEnumerator DamageCooldown()
    {
        canDamage = false;
        yield return new WaitForSeconds(attackCooldown);
        canDamage = true;
    }
}
