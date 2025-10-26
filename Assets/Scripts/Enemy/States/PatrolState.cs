using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;

public class PatrolState : IEnemyState
{
    List<Vector3> wayPoints = new List<Vector3>();
    int currentWaypointIndex;


    public void EnterState(EnemyStateManager enemy)
    {
        wayPoints.Clear();

        EnemyWaypoints waypointHolder = enemy.GetComponent<EnemyWaypoints>();
        if (waypointHolder != null)
        {
            foreach (GameObject wp in waypointHolder.Waypoints)
            {
                wayPoints.Add(wp.transform.position);
                wp.transform.parent = null;
            }
        }

        if (wayPoints.Count > 0)
        {
            currentWaypointIndex = Random.Range(0, wayPoints.Count);
            enemy.agent.speed = 0.2f;
            enemy.agent.SetDestination(wayPoints[currentWaypointIndex]);
            enemy.animator.SetBool("isPatrolling", true);
        }
    }

    public void UpdateState(EnemyStateManager enemy)
    {
        if(!enemy.enemyHealth.isDead)
        {
            if (!enemy.agent.pathPending && enemy.agent.remainingDistance <= enemy.agent.stoppingDistance)
            {
                int nextIndex;
                do
                {
                    nextIndex = Random.Range(0, wayPoints.Count);
                } while (nextIndex == currentWaypointIndex);

                currentWaypointIndex = nextIndex;
                enemy.agent.SetDestination(wayPoints[currentWaypointIndex]);
            }

            float distance = Vector3.Distance(enemy.player.position, enemy.transform.position);
            if (distance < enemy.chaseRange)
                enemy.SwitchState(enemy.chaseState);
        }
        
    }

    public void ExitState(EnemyStateManager enemy)
    {
        enemy.animator.SetBool("isPatrolling", false);
        enemy.agent.SetDestination(enemy.transform.position);
    }
}
