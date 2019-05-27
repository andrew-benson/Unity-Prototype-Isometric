using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyNavController : MonoBehaviour
{
    public NavMeshAgent agent;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        //agent.updateRotation = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                agent.SetDestination(hit.point);
                Debug.Log("Moving To: " + hit.point);
            }

            Debug.Log("Remaining: " + agent.remainingDistance);
            Debug.Log("stoppingDistance: " + agent.stoppingDistance);

            //if (agent.remainingDistance > agent.stoppingDistance)
            //{
            //    characterController.Move(agent.desiredVelocity, false, false);
            //}
            //else
            //{
            //    characterController.Move(Vector3.zero, false, false);
            //}
        }
    }

    private void FixedUpdate()
    {
        
    }
}
