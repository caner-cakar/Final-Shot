using UnityEngine;

public class EnemyAudioCheck : MonoBehaviour
{
    EnemyAudioController audioController;
    Animator animator;
    EnemyHealth enemyHealth;

    void Start()
    {
        audioController = GetComponent<EnemyAudioController>();
        enemyHealth = GetComponent<EnemyHealth>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // Örnek state kontrolü — senin FSM’ine göre düzenlenir:
        if (enemyHealth.isDead)
        {
            audioController.StopSound();
            return;
        }

        if (animator.GetBool("isChasing"))
            audioController.PlayChaseSound();
        else if (animator.GetBool("isAttacking"))
            return;
        else if (animator.GetBool("isPatrolling"))
            audioController.PlayWalkSound();
        else
            audioController.StopSound();
    }
}
