using GLTFast.Schema;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAnimationController : MonoBehaviour
{
    private Animator animator;
    private NavMeshAgent agent;

    private MeleeEnemyAI enemyAI;

    private void Start()
    {
        animator = GetComponentInChildren<Animator>();
        agent = GetComponent<NavMeshAgent>();
        enemyAI = GetComponent<MeleeEnemyAI>(); 
    }
    private void Update()
    {
        UpdateAnimatorBools();
    }

    private void UpdateAnimatorBools()
    {
        if (enemyAI.IsPlayerInAttackRange)
        {
            animator.SetBool("isAttacking", true);
        }
        else
        {
            animator.SetBool("isAttacking", false);
        }

        if (agent.hasPath)
        {
            animator.SetBool("isMoving", true);
        }
        else
        {
            animator.SetBool("isMoving", false);
        }
    }
}
