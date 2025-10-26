using UnityEngine;
using UnityEngine.AI;

public class EnemyStateManager : MonoBehaviour
{
    [HideInInspector] public Transform player;
    [HideInInspector] public NavMeshAgent agent;
    [HideInInspector] public Animator animator;
    [HideInInspector] public EnemyHealth enemyHealth;

    public float chaseRange = 8f;
    public float attackRange = 2.5f;

    IEnemyState currentState;

    // Durumlar
    public EnemyIdleS idleState = new EnemyIdleS();
    public PatrolState patrolState = new PatrolState();
    public ChaseState chaseState = new ChaseState();
    public EnemyAState attackState = new EnemyAState();

    void Start()
    {
        enemyHealth = GetComponent<EnemyHealth>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        SwitchState(idleState);
    }

    void Update()
    {
        currentState.UpdateState(this);
    }

    public void SwitchState(IEnemyState newState)
    {
        if (currentState != null)
            currentState.ExitState(this);

        currentState = newState;
        currentState.EnterState(this);
    }
}
