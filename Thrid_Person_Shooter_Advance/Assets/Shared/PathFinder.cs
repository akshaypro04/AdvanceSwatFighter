using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class PathFinder : MonoBehaviour
{
    public NavMeshAgent agent;
    [SerializeField] float distanceRemainingTrashold;
    bool m_destinationReached;
    public bool destinationReached
    {
        set
        {
            m_destinationReached = value;
            if (m_destinationReached)
            {
                if (OndestinationReached != null)
                {
                    OndestinationReached();
                }
            }
                
        }

        get
        {
            return m_destinationReached;
        }
    }

    public event System.Action OndestinationReached;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    public void SetTarget(Vector3 target)
    {
        agent.SetDestination(target);
        destinationReached = false;
    }

    void Update()
    {
        if (destinationReached || agent.hasPath)
            return;

        if (agent.remainingDistance < distanceRemainingTrashold)
        {
            destinationReached = true;
            print("move to next waypoint :: pathfiner");
        }
    }
}
