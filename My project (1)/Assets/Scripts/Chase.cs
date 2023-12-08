using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chase : MonoBehaviour
{
    public Transform player;
    public float chaseSpeed = 1f;
    private UnityEngine.AI.NavMeshAgent navMeshAgent;
    private Animator spiderAnimator;

    void Start()
    {
        navMeshAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        spiderAnimator = GetComponent<Animator>();

        if (player == null)
        {
            player = GameObject.Find("JohnLemon").transform;
        }

    }

    void Update()
    {
        if (player != null)
        {
            
            navMeshAgent.SetDestination(player.position);

            
            float speed = navMeshAgent.velocity.magnitude;
            

            navMeshAgent.speed = chaseSpeed;
        }
    }
}