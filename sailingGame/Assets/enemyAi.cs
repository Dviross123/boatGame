using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class enemyAi : MonoBehaviour
{
    [SerializeField] NavMeshAgent agent;
    [SerializeField] Transform player;
    [SerializeField] LayerMask groundLM, playerLM;

    // patroling
    [SerializeField] Vector3 walkPoint;
    bool walkPointSet;
    [SerializeField] float walkPointRange;

    //attacking
    [SerializeField] float attackCd;
    bool alreadyAttacked;

    //states
    [SerializeField] float seeRange, attackRange;
    [SerializeField] bool playerInSeeRange, playerInAttackRange;

    private void Update()
    {
        playerInSeeRange = Physics.CheckSphere(transform.position, seeRange, playerLM);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, playerLM);

         if (!playerInSeeRange && !playerInAttackRange) Patrolling();
         if ( playerInSeeRange && !playerInAttackRange) ChasePlayer();
         if (!playerInSeeRange &&  playerInAttackRange) AttackPlayer();
    }

    private void Awake()
    {
        player = GameObject.FindWithTag("Player").transform;
    }

    void Patrolling() 
    {
        if (!walkPointSet) findWalkPoint();

        else
            agent.SetDestination(walkPoint);

        Vector3 disToWalkPoint = transform.position - walkPoint;

        if (disToWalkPoint.magnitude < 1f) walkPointSet = false;
    }

    void findWalkPoint()
    {
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, groundLM))
            walkPointSet = true;
    }

    void ChasePlayer()
    {
        agent.SetDestination(player.position);
    }

    void AttackPlayer()
    {
    }
}
