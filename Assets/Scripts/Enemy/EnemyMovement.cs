using UnityEngine;
using UnityEngine.AI;

namespace Enemy
{
  public class EnemyMovement : MonoBehaviour
  {
    EnemyManager enemyManager;
    EnemyAnimatorHandler enemyAnimatorHandler;
    public Rigidbody enemyRigidbody;

    NavMeshAgent navMeshAgent;
    public GameObject player;

    public float distanceFromTarget;
    public float stoppingDistance = 1f;
    public float rotationSpeed = 15;

    int vertical;
    int horizontal;

    private void Awake()
    {
      enemyManager = GetComponent<EnemyManager>();
      navMeshAgent = GetComponentInChildren<NavMeshAgent>();
      vertical = Animator.StringToHash("Vertical");
      horizontal = Animator.StringToHash("Horizontal");
      enemyRigidbody = GetComponent<Rigidbody>();
      enemyAnimatorHandler = GetComponentInChildren<EnemyAnimatorHandler>();
    }


    private void Start()
    {
      player = GameObject.Find("Player");
      navMeshAgent.enabled = false;
      enemyRigidbody.isKinematic = false;
    }

    public void HandleMoveToTarget()
    {
      Vector3 targetDirection = player.transform.position - transform.position;
      distanceFromTarget = Vector3.Distance(player.transform.position, transform.position);
      float viewableAngle = Vector3.Angle(targetDirection, transform.forward);

      if (enemyManager.isPerformingAction)
      {
        enemyAnimatorHandler.animator.SetFloat("Vertical", 0, 0.1f, Time.deltaTime);
        navMeshAgent.enabled = false;
      }
      else
      {
        if (distanceFromTarget > stoppingDistance)
        {
          enemyAnimatorHandler.animator.SetFloat("Vertical", 1, 0.1f, Time.deltaTime);
          navMeshAgent.enabled = true;
        }
        else if (distanceFromTarget <= stoppingDistance)
        {
          enemyAnimatorHandler.animator.SetFloat("Vertical", 0, 0.1f, Time.deltaTime);
        }
      }

      HandleRotateToTarget();
      navMeshAgent.transform.localPosition = Vector3.zero;
      navMeshAgent.transform.localRotation = Quaternion.identity;
    }
    
    private void HandleRotateToTarget()
    {
      if (enemyManager.isPerformingAction)
      {
        Vector3 direction = player.transform.position - transform.position;
        direction.y = 0;
        direction.Normalize();

        if (direction == Vector3.zero)
        {
          direction = transform.forward;
        }

        Quaternion targetRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed / Time.deltaTime);
      }
      else
      {
        Vector3 relativeDirection = transform.InverseTransformDirection(navMeshAgent.desiredVelocity);
        Vector3 targetVelocity = enemyRigidbody.linearVelocity;

        navMeshAgent.enabled = true;
        navMeshAgent.SetDestination(player.transform.position);
        enemyRigidbody.linearVelocity = targetVelocity;
        transform.rotation = Quaternion.Slerp(transform.rotation, navMeshAgent.transform.rotation, rotationSpeed / Time.deltaTime);
      }
    }
  }
}