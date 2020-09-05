using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[RequireComponent(typeof(PathFinder))]
[RequireComponent(typeof(EnemyPlayer))]
public class EnemyPatrol : MonoBehaviour
{
    [SerializeField]
    WayPointController WayPointController;

    [SerializeField]
    float waitTimeMin;

    [SerializeField]
    float waitTimeMax;

    PathFinder pathFinder;

    private EnemyPlayer m_enemyPlayer;
    public EnemyPlayer enemyPlayer
    {
        get
        {
            if (m_enemyPlayer == null)
                m_enemyPlayer = GetComponent<EnemyPlayer>();
            return m_enemyPlayer;
        }
    }

    void Start()
    {
        WayPointController.SetNextWayPoint();
    }

    void Awake()
    {
        pathFinder = GetComponent<PathFinder>();
        enemyPlayer.enemyHealth.OnDeath += EnemyHealth_OnDeath;
        enemyPlayer.OnTergetSelected += EnemyPlayer_OnTergetSelected;
    }

    [System.Obsolete]
    private void EnemyPlayer_OnTergetSelected(Player obj)
    {
        pathFinder.agent.Stop();

    }

    [System.Obsolete]
    private void EnemyHealth_OnDeath()
    {
        pathFinder.agent.Stop();
    }

    void OnEnable()
    {
        pathFinder.OndestinationReached += PathFinder_OndestinationReached;
        WayPointController.OnWayPointChange += WayPointController_OnWayPointChange;
    }

    private void WayPointController_OnWayPointChange(WayPoint wayPoint)
    {
        pathFinder.SetTarget(wayPoint.transform.position);
        pathFinder.destinationReached = false;
    }

    private void PathFinder_OndestinationReached()
    {
        // patrolling
        GameManager.Instance.Timer.add(WayPointController.SetNextWayPoint, Random.Range(waitTimeMax, waitTimeMin));
    }





}
