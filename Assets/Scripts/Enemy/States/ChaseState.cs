using UnityEngine;

public class ChaseState : IEnemyState
{
    EnemyHealth enemyHealth;
    public void EnterState(EnemyStateManager enemy)
    {
        enemy.agent.speed = 1f;
        enemy.animator.SetBool("isChasing", true);
    }

    public void UpdateState(EnemyStateManager enemy)
    {
        if(!enemy.enemyHealth.isDead)
        {
            enemy.agent.SetDestination(enemy.player.position);

            float distance = Vector3.Distance(enemy.player.position, enemy.transform.position);

            if (distance > enemy.chaseRange)
                enemy.SwitchState(enemy.idleState);

            if (distance < enemy.attackRange)
                enemy.SwitchState(enemy.attackState);
        }
        
    }

    public void ExitState(EnemyStateManager enemy)
    {
        enemy.animator.SetBool("isChasing", false);
        enemy.agent.SetDestination(enemy.transform.position);
    }
}
