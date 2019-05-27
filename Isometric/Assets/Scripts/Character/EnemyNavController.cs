using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityStandardAssets.Characters.ThirdPerson;

public class EnemyNavController : MonoBehaviour
{
    public NavMeshAgent agent;

    public ThirdPersonCharacter charactercontroller;

    public Transform target;
    private bool isReached;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        charactercontroller = GetComponent<ThirdPersonCharacter>();
    }

    private void Start()
    {
        agent.updateRotation = false;
        Invoke(nameof(FindPlayer), 0.1f);
    }

    private void FindPlayer()
    {
        target = GameObject.FindWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        //MouseClickToFindPlayer();
        if(target)
        {

            Debug.Log("Remaining: " + agent.remainingDistance);
            Debug.Log("stoppingDistance: " + agent.stoppingDistance);

            if (!isReached)
                agent.SetDestination(target.position);

            if (agent.remainingDistance > agent.stoppingDistance)
            {
                charactercontroller.Move(agent.desiredVelocity, false, false);
            }
            else
            {
                // We've reached the destination
                charactercontroller.Move(Vector3.zero, false, false);

                if(!agent.pathPending)
                {
                    Debug.Log("Reached Destination");
                    isReached = true;
                    agent.isStopped = true;
                }
            }
        }
    }



    private void MouseClickToFindPlayer()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                agent.SetDestination(target.position);
                Debug.Log("Moving To: " + agent.destination);
            }
            //Debug.Log("Remaining: " + agent.remainingDistance);
            //Debug.Log("stoppingDistance: " + agent.stoppingDistance);
        }
    }
}
