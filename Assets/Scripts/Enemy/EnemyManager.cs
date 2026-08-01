using UnityEngine;
using UnityEngine.AI;

namespace Enemy
{
  public class EnemyManager : MonoBehaviour
  {
    EnemyAnimatorHandler enemyAnimatorHandler;
    EnemyStats enemyStats;
    public Rigidbody enemyRigidbody;
    public NavMeshAgent navMeshAgent;

    public EnemyAttackAction[] enemyAttacks;
    public EnemyAttackAction currentAttack;

    public State currentState;
    public bool isInteracting;

    public float distanceFromTarget;
    public float rotationSpeed = 10;
    public float maximumAttackRange = 5.5f;

    public GameObject player;

    public float currentRecoveryTime = 0;

    private void Awake()
    {
      enemyAnimatorHandler = GetComponentInChildren<EnemyAnimatorHandler>();
      enemyStats = GetComponent<EnemyStats>();
      navMeshAgent = GetComponent<NavMeshAgent>();
      enemyRigidbody = GetComponent<Rigidbody>();
    }

    private void Start()
    {
      navMeshAgent.enabled = false;
      enemyRigidbody.isKinematic = false;
    }

    private void Update() {
      HandleRecoveryTime();
    }

    private void FixedUpdate() {
      HandleStateMachine();
    }

    private void HandleStateMachine()
    {
      if (currentState != null)
      {
        State nextState = currentState.Tick(this, enemyStats, enemyAnimatorHandler);

        if (nextState != null)
        {
          SwitchToNextState(nextState);
        }
      }
    }

    private void SwitchToNextState(State state)
    {
      currentState = state;
    }

    private void HandleRecoveryTime()
    {
      if (currentRecoveryTime > 0)
      {
        currentRecoveryTime -= Time.deltaTime;
      }
      if (isInteracting)
      {
        if (currentRecoveryTime <= 0)
        {
          isInteracting = false;
          currentAttack = null;
        }
      }
    }
  }
}
