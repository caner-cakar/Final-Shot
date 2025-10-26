using UnityEngine;

public class EnemyIdleS : IEnemyState
{
    float timer;

    public void EnterState(EnemyStateManager enemy)
    {
        timer = 0f;
    }

    public void UpdateState(EnemyStateManager enemy)
    {
        timer += Time.deltaTime;
    

        // Patrol'e geçiş
        if (timer > 5f)
            enemy.SwitchState(enemy.patrolState);

        // Chase'e geçiş
        float distance = Vector3.Distance(enemy.player.position, enemy.transform.position);
        if (distance < enemy.chaseRange)
            enemy.SwitchState(enemy.chaseState);
    }

    public void ExitState(EnemyStateManager enemy)
    {
        // Idle’dan çıkarken yapılacaklar
    }
}
