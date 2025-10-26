using UnityEngine;
using UnityEngine.UI;

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
    [SerializeField] Slider healthbar;

    void Start()
    {
        actionStateManager = GetComponent<ActionStateManager>();
        aimStateManager = GetComponent<AimStateManager>();
        movementStateManager = GetComponent<MovementStateManager>();
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        currentHealth = maxHealth;
        UpdateHealthBar();
    }

    private void UpdateHealthBar()
    {
        healthbar.value = currentHealth;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        Debug.Log("Player Health: " + currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
        UpdateHealthBar();
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
