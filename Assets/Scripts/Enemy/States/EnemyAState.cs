using UnityEngine;

public class EnemyAState : IEnemyState
{
    public void EnterState(EnemyStateManager enemy)
    {
        enemy.agent.isStopped = true;
        enemy.animator.SetBool("isAttacking", true);
    }

    public void UpdateState(EnemyStateManager enemy)
    {
        
        enemy.transform.LookAt(enemy.player);
        float distance = Vector3.Distance(enemy.player.position, enemy.transform.position);
        if (distance > enemy.attackRange )
            enemy.SwitchState(enemy.chaseState);
    }

    public void ExitState(EnemyStateManager enemy)
    {
        enemy.agent.isStopped = false;
        enemy.animator.SetBool("isAttacking", false);
    }
}
