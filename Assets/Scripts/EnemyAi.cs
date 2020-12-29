using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAi : MonoBehaviour
{
    [SerializeField] private LayerMask islandLayer;
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private Vector3 walkPoint;
    bool walkPointSet;
    [SerializeField] private float walkPointRange;
    [SerializeField] private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        agent = GetComponent<NavMeshAgent>();
        agent.updatePosition = false;
        agent.updateRotation = false;
    }


    // Update is called once per frame
    void Update()
    {
        if (!walkPointSet) SearchWalkPoint();
        if (walkPointSet)
        {
            agent.SetDestination(walkPoint);
            //Debug.Log(agent.desiredVelocity);
            rb.AddForce(agent.desiredVelocity, ForceMode.Impulse);
            
        }
        Vector3 distanceToWalkPoint = transform.position - walkPoint;
        //Debug.Log(distanceToWalkPoint);

        if(distanceToWalkPoint.magnitude < 1f)
        {
            walkPointSet = false;
        }

        agent.nextPosition = rb.position;
    }

    private void SearchWalkPoint()
    {
        //Calculate random point in rango
        float randomX = Random.Range(-walkPointRange, walkPointRange);
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        //Check if point is on the map range and not an island.
        RaycastHit hit;
        if (Physics.Raycast(walkPoint, -transform.up, out hit, 10f))
            //Debug.Log("raycast worked");
            if (!Physics.Raycast(walkPoint, -transform.up, 100f, islandLayer))
        {
            //Debug.Log("raycast worked on water");
            walkPointSet = true;
            }
    }


}
