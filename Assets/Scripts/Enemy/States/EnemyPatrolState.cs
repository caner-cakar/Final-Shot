using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyPatrolState : StateMachineBehaviour
{
    List<Vector3> wayPoints = new List<Vector3>();
    NavMeshAgent agent;
    Transform player;
    float chaseRange = 8f;
    int currentWaypointIndex = 0;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        agent = animator.GetComponent<NavMeshAgent>();
        agent.speed = 0.2f;
        agent.angularSpeed = 120f;
        agent.acceleration = 8f;

        wayPoints.Clear();

        // Enemy üzerinde EnemyWaypoints scriptini bul
        EnemyWaypoints waypointHolder = animator.GetComponent<EnemyWaypoints>();
        if (waypointHolder != null)
        {
            foreach (GameObject wp in waypointHolder.Waypoints)
            {
                wayPoints.Add(wp.transform.position); // sadece world pozisyonunu alıyoruz
                wp.transform.parent = null;
            }
        }

        if (wayPoints.Count > 0)
        {
            currentWaypointIndex = Random.Range(0, wayPoints.Count);
            agent.SetDestination(wayPoints[currentWaypointIndex]);
        }
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
        {
            int nextIndex;
            do
            {
                nextIndex = Random.Range(0, wayPoints.Count);
            } while (nextIndex == currentWaypointIndex);

            currentWaypointIndex = nextIndex;
            agent.SetDestination(wayPoints[currentWaypointIndex]);
        }

        float distance = Vector3.Distance(player.position, animator.transform.position);
        if (distance < chaseRange)
        {
            animator.SetBool("isChasing", true);
        }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agent.SetDestination(agent.transform.position);
    }
}
