using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    [SerializeField]
    private float lookRadius = 10f;

    private NavMeshAgent agent;
    private Transform playerTargetTransform;
    [SerializeField]
    private Transform bunkerTransform;


    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        playerTargetTransform = PlayerManager.instance.player.transform;
    }

    private void Update()
    {
        float distance = Vector3.Distance(playerTargetTransform.position,transform.position);
        if(distance > lookRadius)
        {
            agent.SetDestination(bunkerTransform.position);
        }
        else 
        {
            agent.SetDestination(playerTargetTransform.position);

            if(distance <= agent.stoppingDistance)
            {
                //Attack the target

                //Face the target
                FaceTarget();
            }
        }
        
    }
    private void FaceTarget()
    {
        Vector3 direction = (playerTargetTransform.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x,0f,direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime *5f);
    }
}
