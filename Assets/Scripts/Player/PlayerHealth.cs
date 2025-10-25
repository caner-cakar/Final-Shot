using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth = 100f;
    private float currentHealth;
    Animator animator;
    public GameObject weapon;
    CharacterController characterController;
    MovementStateManager movementStateManager;
    AimStateManager aimStateManager;
    ActionStateManager actionStateManager;

    void Start()
    {
        actionStateManager = GetComponent<ActionStateManager>();
        aimStateManager = GetComponent<AimStateManager>();
        movementStateManager = GetComponent<MovementStateManager>();
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        currentHealth = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        Debug.Log("Player Health: " + currentHealth);

        if(currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {

        animator.SetTrigger("isDead");
        Debug.Log("Player Died!");
        characterController.enabled = false;
        movementStateManager.enabled = false;
        aimStateManager.enabled = false;
        
        
        if (weapon != null)
            weapon.SetActive(false); // Silah gizle
    }
}
